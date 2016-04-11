using System;
using System.Collections.Generic;
using HM.DataModels;
using MongoDB.Driver;
using MongoDB.Bson;
using HM.DataModels.Utils;

namespace HM.API
{
    public class DBContext<T>
    {
        private static string mongoServerUrl = DbUtils.MongoServerUrl;
        protected static IMongoClient client = new MongoClient(mongoServerUrl);
        protected IMongoDatabase _MongoDatabase = client.GetDatabase(DbUtils.MongoDatabaseName);
        protected string _CollectionName = string.Empty;
        public int IDCurrent = 0;

        #region Constructors
        public DBContext(string collectionName)
        {
            _CollectionName = collectionName;
        }
        #endregion

        #region Insert - Replace - Update - Delete

        /// <summary>
        /// Hàm thêm một object
        /// </summary>
        /// <param name="entity">Object cần thêm</param>
        /// <returns>Object nếu thêm thành công</returns>
        public virtual Result<T> Insert(T entity)
        {
            var result = new Result<T>();
            try
            {
                var idproperty = entity.GetType().GetProperty("Id");
                var idValue = BsonValue.Create(idproperty.GetValue(entity));
                if (idproperty.CanRead && idproperty.CanWrite)
                {

                    idproperty.SetValue(entity, IDCurrent = (int)GetNextId(_MongoDatabase, _CollectionName), null);
                    _MongoDatabase.GetCollection<T>(_CollectionName).InsertOne(entity);
                    result.Data = entity;
                }
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Hàm thay thế một object
        /// </summary>
        /// <param name="entity">Object cần thay thế</param>
        /// <returns>Object nếu thay thế thành công</returns>
        public virtual Result<T> Replace(T entity)
        {
            var result = new Result<T>();
            try
            {
                var idproperty = entity.GetType().GetProperty("Id");
                var idValue = BsonValue.Create(idproperty.GetValue(entity));
                _MongoDatabase.GetCollection<T>(_CollectionName).ReplaceOne(Builders<T>.Filter.Eq("_id", idValue), entity);
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Hàm update các field của một object
        /// </summary>
        /// <param name="id">Id của object cần update</param>
        /// <param name="fieldValues">Danh sách các field của object</param>
        /// <returns>Object nếu update thành công</returns>
        public virtual Result<T> Update(BsonValue id, Dictionary<string, BsonValue> fieldValues)
        {
            var result = new Result<T>();
            try
            {
                if (fieldValues == null || fieldValues.Count == 0)
                    result.Code = MessageUtils.ERR_NO_FIELD_TO_UPDATE;

                UpdateDefinition<T> update = null;
                foreach (var key in fieldValues.Keys)
                {
                    if (update == null) update = Builders<T>.Update.Set(key, fieldValues[key]);
                    update = update.Set(key, fieldValues[key]);
                }

                _MongoDatabase.GetCollection<T>(_CollectionName).UpdateOne(Builders<T>.Filter.Eq("_id", id), update);
                result.Data = this.GetObject(id).Data;
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Hàm xóa một object (Thay đổi giá trị Inactive)
        /// </summary>
        /// <param name="id">Id của object cần xóa</param>
        /// <returns>Object nếu xóa thành công</returns>
        public virtual Result<T> Delete(BsonValue id)
        {
            var result = new Result<T>();
            try
            {
                UpdateDefinition<T> update = null;
                update = update.Set("Inactive", true);

                _MongoDatabase.GetCollection<T>(_CollectionName).UpdateOne(Builders<T>.Filter.Eq("_id", id), update);
                result.Data = this.GetObject(id).Data;
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Hàm lấy danh sách tất cả object 
        /// </summary>
        /// <returns>Danh sách tất cả object</returns>
        public virtual Result<IEnumerable<T>> GetObjects()
        {
            var result = new Result<IEnumerable<T>>();
            try
            {
                result.Data = _MongoDatabase.GetCollection<T>(_CollectionName).Find(Builders<T>.Filter.Empty).ToList();
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Hàm lấy object theo Id
        /// </summary>
        /// <param name="id">Id của object cần lấy</param>
        /// <returns>Object nếu tồn tại, null nếu không tồn tại</returns>
        public virtual Result<T> GetObject(BsonValue id)
        {
            var result = new Result<T>();
            try
            {
                result.Data = _MongoDatabase.GetCollection<T>(_CollectionName).Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Hàm lấy một Object theo một key (field)
        /// </summary>
        /// <param name="fieldName">Tên field cần tìm</param>
        /// <param name="fieldValue">Giá trị của field cần tìm</param>
        /// <returns>Danh sách object nếu tồn tại, null nếu không tồn tại</returns>
        public virtual Result<IEnumerable<T>> GetObjectsByIndex(string fieldName, BsonValue fieldValue)
        {
            var result = new Result<IEnumerable<T>>();
            try
            {
                result.Data = _MongoDatabase.GetCollection<T>(_CollectionName).Find(Builders<T>.Filter.Eq(fieldName, fieldValue)).ToList();
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }
            return result;
        }

        #endregion

        #region Utilities
        /// <summary>
        /// Hàm lấy Id tiếp theo của object (Id tự động tăng)
        /// </summary>
        /// <param name="mongodb">database</param>
        /// <param name="collectionName">tên collection</param>
        /// <returns></returns>
        private static long GetNextId(IMongoDatabase mongodb, string collectionName)
        {
            var col = mongodb.GetCollection<BsonDocument>("_counters");
            var result = col.FindOneAndUpdate(
                Builders<BsonDocument>.Filter.Eq("_id", 1),
                Builders<BsonDocument>.Update.Inc(collectionName, 1),
                new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = true, ReturnDocument = ReturnDocument.After });
            return result.GetValue(collectionName).ToInt64();
        }
        #endregion
    }
}
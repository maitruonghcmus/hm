using System;
using System.Collections.Generic;
using HM.API.Models;
using HM.DataModels;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Reflection;
using Newtonsoft.Json;
using FluentAssertions;
using MongoDB.Driver.Core;
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
        /// <param name="entity"></param>
        /// <returns></returns>
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
        /// Replace a document
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Result<T> Replace(T entity)
        {
            var result = new Result<T>();
            try
            {
                var idproperty = entity.GetType().GetProperty("Id");
                var idValue = BsonValue.Create(idproperty.GetValue(entity));
                var re = _MongoDatabase.GetCollection<T>(_CollectionName).ReplaceOne(Builders<T>.Filter.Eq("_id", idValue), entity);
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// update document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update">for example </param>
        /// <returns></returns>
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
                result.Data = GetObject(id).Data;
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// set an entity inactive
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Result<T> Delete(BsonValue id)
        {
            var result = new Result<T>();
            try
            {
                UpdateDefinition<T> update = null;
                update = update.Set("Inactive", true);

                _MongoDatabase.GetCollection<T>(_CollectionName).UpdateOne(Builders<T>.Filter.Eq("_id", id), update);
                result.Data = GetObject(id).Data;
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Get all entity from a collection
        /// </summary>
        /// <returns></returns>
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
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// Get entity by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
        /// get nextid by collection name
        /// </summary>
        /// <param name="db"></param>
        /// <param name="collectionName"></param>
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
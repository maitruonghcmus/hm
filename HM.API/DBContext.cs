using System;
using System.Collections.Generic;
using HM.API.Models;
using HM.DataModels;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Reflection;
using Newtonsoft.Json;

namespace HM.API
{
    public class DBContext<DocumentT> where DocumentT : class
    {
        public static DBContext<DocumentT> Instance = new DBContext<DocumentT>();

        protected static IMongoClient client = new MongoClient(DbUtils.MongoServerUrl);
        protected static IMongoDatabase db = client.GetDatabase(DbUtils.MongoDatabaseName);

        /// <summary>
        /// Hàm thêm một Document vào db
        /// </summary>
        /// <param name="doc">document bất kỳ</param>
        /// <returns></returns>
        public Result<DocumentT> Create(DocumentT doc)
        {
            var result = new Result<DocumentT>();
            try
            {
                var collection = db.GetCollection<BsonDocument>(doc.GetType().ToString().Remove(0, 14));
                collection.InsertOne(doc.ToBsonDocument());

                result.IsSuccess = true;
                result.Data = doc;
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Hàm lấy danh sách Document của một collection
        /// </summary>
        /// <param name="doc">document rỗng bất kỳ (chỉ dùng để lấy kiểu dữ liệu)</param>
        /// <returns>Danh sách document</returns>
        public Result<IEnumerable<DocumentT>> Read(DocumentT doc)
        {
            var result = new Result<IEnumerable<DocumentT>>();
            try
            {
                var collection = db.GetCollection<DocumentT>(doc.GetType().ToString().Remove(0, 14));
                var documents = collection.Find(new BsonDocument()).ToList();
                result.IsSuccess = true;
                result.Data = documents;
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Hàm tìm một Document theo id
        /// </summary>
        /// <param name="id">id của document cần tìm</param>
        /// <param name="doc">document rỗng bất kỳ (chỉ dùng để lấy kiểu dữ liệu)</param>
        /// <returns>Document cần tìm</returns>
        public Result<DocumentT> Read(ObjectId id, DocumentT doc)
        {
            var result = new Result<DocumentT>();
            try
            {
                var collection = db.GetCollection<DocumentT>(doc.GetType().ToString().Remove(0, 14));
                var filter = Builders<DocumentT>.Filter.Eq("_id", id);
                var documents = collection.Find(filter).FirstOrDefault();

                result.IsSuccess = true;
                result.Data = documents;
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Hàm update một Document bất kỳ
        /// </summary>
        /// <param name="oldDoc">document cần update</param>
        /// <param name="newDoc">document mới, chứa giá trị mới cần update cho document cũ</param>
        /// <returns>Document đã thay đổi giá trị</returns>
        public Result<DocumentT> Update(DocumentT oldDoc, DocumentT newDoc)
        {
            var result = new Result<DocumentT>();
            try
            {
                var collection = db.GetCollection<DocumentT>(oldDoc.GetType().ToString().Remove(0, 14));
                var oldDocId = oldDoc.GetType().GetProperty("Id");
                var filter = Builders<DocumentT>.Filter.Eq("_id", oldDocId);
                var documents = collection.ReplaceOne(filter, newDoc);

                result.IsSuccess = true;
                result.Data = newDoc;
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Hàm xóa một Document bất kỳ (update Inactive = true)
        /// </summary>
        /// <param name="id">id của document cần update</param>
        /// <returns></returns>
        public Result<DocumentT> Detele(ObjectId id, DocumentT doc)
        {
            var result = new Result<DocumentT>();
            try
            {
                var collection = db.GetCollection<DocumentT>(doc.GetType().ToString().Remove(0, 14));

                var filter = Builders<DocumentT>.Filter.Eq("_id", id);

                var update = Builders<DocumentT>.Update
                    .Set("Inactive", true)
                    .CurrentDate("ModifiedOn");

                collection.UpdateOne(filter, update);

                result.IsSuccess = true;
                result.Data = null;
            }
            catch (Exception ex)
            {
                result.Code = ex.Message;
            }

            return result;
        }
    }

    //public class DBContext<T> where T : class
    //{
    //    public static DBContext<T> Instance = new DBContext<T>();

    //    internal HotelManagementEntities db = new HotelManagementEntities();

    //    /// <summary>
    //    /// Thêm mới một object, object có thể là bất kỳ record nào trong các bảng có trong DB
    //    /// </summary>
    //    /// <param name="entity">object cần thêm</param>
    //    /// <returns>true nếu thêm thành công, false nếu thêm thất bại</returns>
    //    public bool Create(T entity)
    //    {
    //        var success = false;
    //        try
    //        {
    //            db.Set<T>().Add(entity);
    //            db.SaveChanges();
    //            success = true;
    //        }
    //        catch (Exception e)
    //        {
    //        }
    //        return success;
    //    }

    //    /// <summary>
    //    /// Lấy tất cả các object, object có thể là bất kỳ record nào trong các bảng có trong DB
    //    /// </summary>
    //    /// <returns>danh sách các object</returns>
    //    public IEnumerable<T> Read()
    //    {
    //        return db.Set<T>();
    //    }

    //    /// <summary>
    //    /// Lấy một object theo id, object có thể là bất kỳ record nào trong các bảng có trong DB
    //    /// </summary>
    //    /// <param name="id">id của object</param>
    //    /// <returns>object, null nếu không tìm thấy</returns>
    //    public T Read(int id)
    //    {
    //        return db.Set<T>().Find(id);
    //    }

    //    /// <summary>
    //    /// Update thông tin một object, object có thể là bất kỳ record nào trong các bảng có trong DB
    //    /// </summary>
    //    /// <param name="id">id của object cần update</param>
    //    /// <param name="obj">object lưu trữ thông tin cần update</param>
    //    /// <returns>true nếu thành công, false nếu thất bại</returns>
    //    public bool Update(int id, T obj)
    //    {
    //        var result = false;
    //        var objToUpdate = db.Set<T>().Find(id);
    //        if (objToUpdate != null)
    //        {
    //            try
    //            {
    //                db.Entry(objToUpdate).CurrentValues.SetValues(obj);
    //                db.SaveChanges();
    //                result = true;
    //            }
    //            catch (Exception)
    //            {
    //            }
    //        }

    //        return result;
    //    }

    //    /// <summary>
    //    /// Xóa một object theo id(không xóa thực, chỉ set inactive = true)
    //    /// </summary>
    //    /// <param name="id">id của object cần xóa</param>
    //    /// <returns>true nếu thành công, false nếu thất bại</returns>
    //    public bool Delete(int id)
    //    {
    //        var result = false;
    //        var objToDelete = db.Set<T>().Find(id);
    //        if (objToDelete != null)
    //        {
    //            try
    //            {
    //                var type = objToDelete.GetType();
    //                var prop = type.GetProperty("Inactive");
    //                prop.SetValue(objToDelete, true, null);

    //                db.Entry(objToDelete).CurrentValues.SetValues(objToDelete);
    //                db.SaveChanges();
    //                result = true;
    //            }
    //            catch (Exception)
    //            {
    //            }
    //        }

    //        return result;
    //    }
    //}
}
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace HM.DataModels
{
    public class Role
    {
        [BsonId]
        public Guid Id { get; set; }

        /// <summary>
        /// Tên role, ví dụ: administrator, manager, quản lý, thu ngân
        /// </summary>
        public string Name { get; set; }
        public bool? Inactive { get; set; }
    }
}

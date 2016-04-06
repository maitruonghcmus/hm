using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace HM.DataModels
{
    public class RoomType
    {
        public ObjectId Id { get; set; }

        /// <summary>
        /// Tên loại phòng
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Số khách tối đa được ở trong phòng
        /// </summary>
        public int MaxCustomer { get; set; }

        /// <summary>
        /// Danh sách giá của một phòng
        /// int: số giờ
        /// long: giá tương ứng
        /// Ví dụ: 1h giá 70k, 2h giá 90k, 1 đêm giá 150k
        /// </summary>
        public Dictionary<int, long> Price { get; set; }

        public ObjectId HotelId { get; set; }
        public bool? Inactive { get; set; }
        public ObjectId CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public ObjectId ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

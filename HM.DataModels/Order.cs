using MongoDB.Bson;
using System;

namespace HM.DataModels
{
    public class Order
    {
        public ObjectId Id { get; set; }

        /// <summary>
        /// Khách hàng
        /// </summary>
        public ObjectId CustomerId { get; set; }

        /// <summary>
        /// Phòng
        /// </summary>
        public ObjectId RoomId { get; set; }

        /// <summary>
        /// Giờ vào lấy phòng (giờ tạo order)
        /// </summary>
        public DateTime CheckinDate { get; set; }

        public ObjectId HotelId { get; set; }
        public bool? Inactive { get; set; }
        public ObjectId CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public ObjectId ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

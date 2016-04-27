using System;

namespace HM.DataModels
{
    public class Order
    {
        public int Id { get; set; }

        /// <summary>
        /// Khách hàng
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Phòng
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Giờ vào lấy phòng (giờ tạo order)
        /// </summary>
        public DateTime CheckInDate { get; set; }

        public int? HotelId { get; set; }
        public bool Inactive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

using System;

namespace HM.DataModels
{
    public class Order
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Khách hàng
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Phòng
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// Giờ vào lấy phòng (giờ tạo order)
        /// </summary>
        public DateTime CheckinDate { get; set; }

        public Guid HotelId { get; set; }
        public bool? Inactive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

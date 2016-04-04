using System;

namespace HM.DataModels
{
    public class Order
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Khách hàng
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Phòng
        /// </summary>
        public Room Room { get; set; }

        /// <summary>
        /// Giờ vào lấy phòng (giờ tạo order)
        /// </summary>
        public DateTime CheckinDate { get; set; }

        public Hotel Hotel { get; set; }
        public bool? Inactive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

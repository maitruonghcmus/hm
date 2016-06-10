using System;
using System.ComponentModel.DataAnnotations;

namespace HM.DataModels
{
    public class Room
    {
        public int Id { get; set; }

        /// <summary>
        /// Loại phòng
        /// </summary>
        [Required]
        public int RoomTypeId { get; set; }

        /// <summary>
        /// Tên hoặc mã phòng
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Ghi chú của phòng
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Trạng thái phòng
        /// 0. Trống
        /// 1. Có khách
        /// -1. Cần dọn dẹp
        /// </summary>
        public int? Status { get; set; } = 0;
        public int? CurrentCustomerId { get; set; }
        public int? CurrentOrderId { get; set; }

        public DateTime? CheckInDate { get; set; }

        [Required]
        public int HotelId { get; set; }
        public bool Inactive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
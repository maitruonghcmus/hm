using System;
namespace HM.DataModels
{
    public class Room
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Loại phòng
        /// </summary>
        public Guid RoomTypeId { get; set; }

        /// <summary>
        /// Tên hoặc mã phòng
        /// </summary>
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
        public int Status { get; set; }

        public Guid HotelId { get; set; }
        public bool? Inactive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

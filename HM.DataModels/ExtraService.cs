
using System;
using System.ComponentModel.DataAnnotations;

namespace HM.DataModels
{
    /// <summary>
    /// Các dịch vụ cộng thêm của KS
    /// </summary>
    public class ExtraService
    {
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Tên dịch vụ, ví dụ: cocacola, pepsi, mì bò, giặt ủi, dọn phòng, nước suối
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Đơn vị tính của dịch vụ, ví dụ: chai, lon, lần
        /// </summary>
        [Required]
        public string Unit { get; set; }

        [Required]
        public string Price { get; set; }

        [Required]
        public int HotelId { get; set; }
        public bool Inactive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

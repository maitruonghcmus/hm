using System;
using System.ComponentModel.DataAnnotations;

namespace HM.DataModels
{
    /// <summary>
    /// Loại khách hàng (trong nước, ngoài nước, cá nhân, tổ chức cty)
    /// </summary>
    public class CustomerType
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Hệ số (hệ số nhân giá phòng của khách hàng)
        /// VD: trong nước 1
        /// ngoài nước 1.5 hoặc khác, chém cao hơn khách trong nước
        /// cty đặt nhiều phòng thì giảm giá 20% => hệ số = 0.8
        /// </summary>
        [Required]
        public float Coefficient { get; set; }

        [Required]
        public int HotelId { get; set; }
        public bool Inactive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

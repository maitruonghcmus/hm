using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HM.DataModels
{
    public class OrderDetail
    {
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Order mà tương ứng mà khách đã đặt (1 order - n order detail)
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// Danh sách những dịch vụ trong một lần khách mua hàng
        /// Ví dụ: coca 3,khăn lạnh 2, mỳ bò 3
        /// int: ExtraServiceId
        /// int: số lượng
        /// </summary>
        public Dictionary<int, int> ExtraServices { get; set; }

        /// <summary>
        /// Tổng tiền cho tất cả các dịch vụ trên
        /// </summary>
        [Required]
        public long Total { get; set; }

        [Required]
        public int HotelId { get; set; }
        public bool Inactive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
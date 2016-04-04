using System;
using System.Collections.Generic;

namespace HM.DataModels
{
    public class OrderDetail
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Order mà tương ứng mà khách đã đặt (1 order - n order detail)
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Danh sách những dịch vụ trong một lần khách mua hàng
        /// Ví dụ: coca 3,khăn lạnh 2, mỳ bò 3
        /// Guid: ExtraServiceId
        /// int: số lượng
        /// </summary>
        public Dictionary<Guid, int> ExtraServices { get; set; }

        /// <summary>
        /// Tổng tiền cho tất cả các dịch vụ trên
        /// </summary>
        public long Total { get; set; }

        public Guid HotelId { get; set; }
        public bool? Inactive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
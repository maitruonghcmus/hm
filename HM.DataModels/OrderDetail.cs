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
        public Order Order { get; set; }

        /// <summary>
        /// Danh sách những dịch vụ trong một lần khách mua hàng
        /// Ví dụ: coca 3,khăn lạnh 2, mỳ bò 3
        /// </summary>
        public Dictionary<ExtraService, int> ExtraServices { get; set; }

        /// <summary>
        /// Tổng tiền cho tất cả các dịch vụ trên
        /// </summary>
        public long Total { get; set; }
    }
}
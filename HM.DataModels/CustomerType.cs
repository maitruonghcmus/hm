﻿using System;

namespace HM.DataModels
{
    /// <summary>
    /// Loại khách hàng (trong nước, ngoài nước, cá nhân, tổ chức cty)
    /// </summary>
    public class CustomerType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Hệ số (hệ số nhân giá phòng của khách hàng)
        /// VD: trong nước 1
        /// ngoài nước 1.5 hoặc khác, chém cao hơn khách trong nước
        /// cty đặt nhiều phòng thì giảm giá 20% => hệ số = 0.8
        /// </summary>
        public double Coefficient { get; set; }

        public Guid HotelId { get; set; }
        public bool? Inactive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

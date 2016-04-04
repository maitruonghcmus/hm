﻿using System;

namespace HM.DataModels
{
    /// <summary>
    /// Các dịch vụ cộng thêm của KS
    /// </summary>
    public class ExtraService
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Tên dịch vụ, ví dụ: cocacola, pepsi, mì bò, giặt ủi, dọn phòng, nước suối
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Đơn vị tính của dịch vụ, ví dụ: chai, lon, lần
        /// </summary>
        public string Unit { get; set; }
        public string Price { get; set; }

        public Hotel Hotel { get; set; }
        public bool? Inactive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

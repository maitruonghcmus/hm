﻿using System;

namespace HM.DataModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }

        /// <summary>
        /// Password cần được mã hóa trước khi lưu
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Phân quyền
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Khách sạn của user
        /// </summary>
        public Guid HotelId { get; set; }

        /// <summary>
        /// Bị xóa ? True = bị xóa, false hoặc null là chưa bị xóa
        /// </summary>
        public bool? Inactive { get; set; }
        
        /// <summary>
        /// Tạo bởi user nào
        /// </summary>
        public Guid CreatedBy { get; set; }
        
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedOn { get; set; }
        
        /// <summary>
        /// Sửa bởi user nào
        /// </summary>
        public Guid ModifiedBy { get; set; }
        
        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
    }
}

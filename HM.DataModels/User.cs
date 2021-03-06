﻿using System;
using System.ComponentModel.DataAnnotations;

namespace HM.DataModels
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        
        public string Fullname { get; set; }

        /// <summary>
        /// Password cần được mã hóa trước khi lưu
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Phân quyền
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Khách sạn của user
        /// </summary>
        [Required]
        public int HotelId { get; set; }

        /// <summary>
        /// Bị xóa ? True = bị xóa, false hoặc null là chưa bị xóa
        /// </summary>
        public bool Inactive { get; set; }
        
        /// <summary>
        /// Tạo bởi user nào
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedOn { get; set; }
        
        /// <summary>
        /// Sửa bởi user nào
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
    }
}

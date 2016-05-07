using System;

namespace HM.DataModels
{
    /// <summary>
    /// Khách hàng của 1 khách sạn
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        /// <summary>
        /// Loại khách hàng
        /// </summary>
        public int CustomerTypeId { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// CMND, Hộ chiếu
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// KH thuộc về KS nào
        /// </summary>
        public int HotelId { get; set; }
        /// <summary>
        /// Trạng thái đang hoạt động/đã xóa
        /// </summary>
        public bool Inactive { get; set; }
        /// <summary>
        /// Được tạo bởi người dùng nào
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// Tạo vào lúc nào
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Sửa bởi người dùng nào
        /// </summary>
        public int? ModifiedBy { get; set; }
        /// <summary>
        /// Sửa vào lúc nào
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
    }
}
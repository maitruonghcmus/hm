using System;

namespace HM.DataModels
{
    /// <summary>
    /// Khách hàng của 1 khách sạn
    /// </summary>
    public class Customer
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Loại khách hàng
        /// </summary>
        public CustomerType CustomerType { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// CMND, Hộ chiếu
        /// </summary>
        public string CardId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Hotel Hotel { get; set; }
        public bool? Inactive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
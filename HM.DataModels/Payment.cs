using System;

namespace HM.DataModels
{
    public class Payment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        
        /// <summary>
        /// Số lượng (giờ)
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Tiền phòng
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Danh sách những chi tiết hóa đơn cho một lần thanh toán
        /// </summary>
        public Guid[] OrderDetailIds { get; set; }
        
        /// <summary>
        /// Tiền dịch vụ (của orderdetails)
        /// </summary>
        public long ServiceCharge { get; set; }
        /// <summary>
        /// Chi phí khác (nếu có)
        /// </summary>
        public long OtherCharge { get; set; }

        /// <summary>
        /// Giảm giá %
        /// </summary>
        public double DiscountPercent { get; set; }

        /// <summary>
        /// Tổng tiền giảm giá
        /// </summary>
        public long DiscountTotal { get; set; }

        /// <summary>
        /// Nếu có xuất VAT thì tính thêm 10% tổng giá đơn hàng
        /// </summary>
        public bool? VAT { get; set; }

        /// <summary>
        /// Tổng tiền = Amount + ServiceCharge + OtherCharge + DiscountTotal + VAT
        /// </summary>
        public long Total { get; set; }

        public int HotelId { get; set; }
        public bool? Inactive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
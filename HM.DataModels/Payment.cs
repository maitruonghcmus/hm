using System;

namespace HM.DataModels
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        public Order Order { get; set; }
        
        /// <summary>
        /// Số lượng (giờ)
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Tiền phòng
        /// </summary>
        public long Amount { get; set; }

        public OrderDetail[] OrderDetails { get; set; }
        
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
    }
}
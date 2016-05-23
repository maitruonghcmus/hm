using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HM.WebApp.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }

        public long Total { get; set; }

        public PaymentModel() { }

        public PaymentModel(Payment p)
        {
            this.Id = p.Id;
            this.OrderId = p.OrderId;
            this.RoomType = AppContext.Instance.GetRoomTypeFromPayment(p.Id);
            this.RoomTypeId = this.RoomType?.Id ?? 0;
            this.Total = p.Total;
        }
    }
}
using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Models
{
    public class OrderDetailModel
    {

        public int Id { get; set; }

 
        public int OrderId { get; set; }


        public int ServiceId { get; set; }
        public string ServicePrice { get; set; }
        public int Number { get; set; }

        public long Total { get; set; }
        public string ServiceName { get; set; }

        public OrderDetailModel() { }
        public OrderDetailModel(OrderDetail o)
        {
            this.Id = o.Id;
            this.OrderId = o.OrderId;
            this.ServiceId = o.ServiceId;
            this.Number = o.Number;
            
            ServiceName = DataContext.Instance.GetExtraService(o.ServiceId)?.Name;
            ServicePrice = DataContext.Instance.GetExtraService(o.ServiceId)?.Price;
            Total = long.Parse(ServicePrice) * Number;
        }
    }
}
using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Models
{
    public class PayModel
    {
      

        public int OrderId { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public int CustomerId { get; set; }
        public double Coe { get; set; }
        public long SvTotal { get; set; }
        public string CustomerName { get; set; }
        public string strCheckIn { get; set; }
        public string strCheckOut { get; set; }
        public PayModel() { }
        public double Day { get; set; }
        public double Hour { get; set; }
        public PayModel(int roomId, int customerId, int ordId)
        {

            this.OrderId = ordId;
            RoomTypeId = DataContext.Instance.GetRoom(roomId).RoomTypeId;
            RoomType = DataContext.Instance.GetRoomType(RoomTypeId);
            CustomerId = DataContext.Instance.GetCustomer(customerId).Id;
            int CusType = DataContext.Instance.GetCustomer(customerId).CustomerTypeId;

            Coe = DataContext.Instance.GetCustomerType(CusType).Coefficient;

            CustomerName = DataContext.Instance.GetCustomer(customerId).Name ?? "null";
            var orderdetails = DataContext.Instance.GetOrderDetails().Where(o => o.OrderId == ordId)?.Select(o => o.Total??0);
            if(orderdetails != null)
            {
                SvTotal = 0;
                foreach (var item in orderdetails)
                {
                    SvTotal += item;
                }
            }

            var CheckIndate = DataContext.Instance.GetRoom(roomId)?.CheckInDate;

          

            if (CheckIndate!=null)
            {
                DateTime CheckOutDate = DateTime.Now;
                Day = Convert.ToInt32((CheckOutDate - CheckIndate.Value).TotalDays);
                Hour = Convert.ToInt32((CheckOutDate - CheckIndate.Value).TotalHours);

                strCheckIn = CheckIndate.Value.Day.ToString()+"/"+CheckIndate.Value.Month.ToString() ;
                strCheckOut = CheckOutDate.Day.ToString()+"/"+CheckOutDate.Month.ToString();
            }
            
            
        }
    }
}
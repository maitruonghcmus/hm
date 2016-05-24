using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HM.WebApp.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public string CheckInDate { get; set; }

        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public string RoomTypeName { get; set; }
        public Customer Customer { get; set; }

        public string Price { get; set; }

        public RoomModel() { }
        public RoomModel(Room r)
        {
            this.Id = r.Id;
            this.Name = r.Name;
            this.Status = r.Status;
            this.RoomTypeId = r.RoomTypeId;
            this.RoomType = DataContext.Instance.GetRoomType(r.RoomTypeId);
            this.RoomTypeName = RoomType?.Name ?? "Chưa rõ";
            this.Price = RoomType?.Price[3].ToString(NumberUtils.NumberFormatByComma) ?? "0";
            Customer = DataContext.Instance.GetCustomer(r.CurrentCustomerId??-1);
            this.CheckInDate = r.CheckInDate?.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM) ?? "Phòng trống";
            
        }
    }
}
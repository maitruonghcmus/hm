using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HM.WebApp.Models
{
    public class RoomTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxCustomer { get; set; }
        public long[] Price { get; set; }
        public int RoomCount { get; set; }

        public RoomTypeModel() { }
        public RoomTypeModel(RoomType rt)
        {
            this.Id = rt.Id;
            this.Name = rt.Name;
            this.MaxCustomer = rt.MaxCustomer;
            this.Price = rt.Price;
            this.RoomCount = DataContext.Instance.GetRooms()?.Where(a => a.RoomTypeId == rt.Id)?.Count() ?? 0;
        }
    }
}
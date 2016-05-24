using HM.DataModels;
using HM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            var rooms = DataContext.Instance.GetRooms();
            var roomModels = rooms?.Select(a => new RoomModel(a));
            var customers = DataContext.Instance.GetCustomers();
            var services = DataContext.Instance.GetExtraServices();
            ViewBag.Customers = customers;
            ViewBag.Rooms = roomModels;
            ViewBag.Services = services;
            

            return View();
        }

        public ActionResult LoadRooms()
        {
            var rooms = DataContext.Instance.GetRooms();
            var roomModels = rooms?.Select(a => new RoomModel(a));
            var customers = DataContext.Instance.GetCustomers();
            var services = DataContext.Instance.GetExtraServices();
            ViewBag.Customers = customers;
            ViewBag.Rooms = roomModels;
            ViewBag.Services = services;


            return PartialView("_ShowRooms");
        }

        public ActionResult GetRoomInfo(int rId)
        {
            var room = DataContext.Instance.GetRoom(rId);
            var roommodel = new RoomModel(room);
            return Json(roommodel,JsonRequestBehavior.AllowGet);
        }

        public ActionResult BookNewRoom(Order ord)
        {
            ord.CheckInDate = DateTime.Now;
            
            var r = DataContext.Instance.GetRoom(ord.RoomId);
            if(r != null) { r.Status = 1; r.CurrentCustomerId = ord.CustomerId; r.CheckInDate = ord.CheckInDate; }
            else { return Json(false, JsonRequestBehavior.AllowGet); }

            var createsuccess = DataContext.Instance.CreateOrder(ord);
            if (createsuccess)
            {
                var updatesuccess = DataContext.Instance.UpdateRoom(r);
                if (updatesuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}
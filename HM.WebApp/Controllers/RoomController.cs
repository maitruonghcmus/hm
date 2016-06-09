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
            //var roomModels = rooms?.Select(a => new RoomModel(a))?.Where(a=>a.Customer != null && a.Order != null)?.Select(a=>a).ToList();
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
            if(r != null) {
                r.Status = 1;
                r.CurrentCustomerId = ord.CustomerId;
                r.CheckInDate = ord.CheckInDate;
            }
            else { return Json(false, JsonRequestBehavior.AllowGet); }

            var ordId = DataContext.Instance.CreateOrderReturnOrderId(ord);
            if (ordId != -1)
            {
                r.CurrentOrderId = ordId;

                var updatesuccess = DataContext.Instance.UpdateRoom(r);
                if (updatesuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetOrderDetails(int ordId)
        {
            var detail = DataContext.Instance.GetOrderDetails()?.Where(a => a.OrderId == ordId).Select(a => a).ToList();
            var OrderDetailModel = detail?.Select(a => new OrderDetailModel(a));
            //var selected = rooms != null ? rooms.Where(a => a.RoomTypeId == rtId).Select(a => a) : null;
            ViewBag.OrderDetailList = OrderDetailModel;
            return PartialView("_ShowOrderDetail");
        }

        public ActionResult Calculate(int roomId, int customerId, int ordId)
        {
            var topay = AppContext.Instance.Calculate(roomId, customerId, ordId);
            if (topay != null) {
                var obj = new
                {
                    topay.OrderId,
                    topay.RoomTypeId,
                    topay.CustomerId,
                    topay.CustomerName,
                    topay.SvTotal,
                    topay.Day,
                    topay.Hour,
                    topay.strCheckIn,
                    topay.strCheckOut,
                    topay.RoomType,
                    topay.Coe
                 };
                return Json(obj, JsonRequestBehavior.AllowGet);

            };
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateRoomToClean(int roomId)
        {
            var r = DataContext.Instance.GetRoom(roomId);
            r.Status = -1;
            r.CurrentCustomerId = null;
            r.CurrentOrderId = null;
            r.CheckInDate = null;

            var updatesuccess = DataContext.Instance.UpdateRoom(r);
            if(updatesuccess == true)
            { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckRoomAvailable(int roomId)
        {
            var r = DataContext.Instance.GetRoom(roomId);
            r.Status = 0;
      

            var updatesuccess = DataContext.Instance.UpdateRoom(r);
            if (updatesuccess == true)
            { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

    }
}
using HM.DataModels;
using HM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    public class OrderDetailController : Controller
    {
        // GET: OrderDetail
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetOrderDetails(int ordId)
        {
            var detail = DataContext.Instance.GetOrderDetails()?.Where(a => a.OrderId == ordId).Select(a => a).ToList();
            var OrderDetailModel = detail?.Select(a => new OrderDetailModel(a));
            //var selected = rooms != null ? rooms.Where(a => a.RoomTypeId == rtId).Select(a => a) : null;
            ViewBag.OrderDetailList = OrderDetailModel;
            return PartialView("_ShowOrderDetail");
        }



      

        public ActionResult CreateOrderDetail(OrderDetail orderDetail)
        {
            var createsuccess = DataContext.Instance.CreateOderDetail(orderDetail);
            if (createsuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

       
    }
}
using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            if (AppContext.Instance.GetDayRemains() <= 0)
                return RedirectToAction("Expired", "Error");

            return View();
        }

        public ActionResult CreateOrder(Order ord)
        {
            var createsuccess = DataContext.Instance.CreateOrder(ord);
            if (createsuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteOrder(int ordId)
        {
            var deletesuccess = DataContext.Instance.DeleteOrder(ordId);
            if (deletesuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);

        }

        public ActionResult UpdataOrder(Order ord)
        {
            var updatesuccess = DataContext.Instance.UpdateOrder(ord);
            if (updatesuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrderInfo(int ordId)
        {
            var order = DataContext.Instance.GetOrder(ordId);
            if(order != null)
            {
                return Json(order, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}
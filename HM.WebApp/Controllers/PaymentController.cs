using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        [HttpPost]
        public ActionResult CreatePayment(Payment pm)
        {
            pm.CheckOutDate = DateTime.Now;

            var createsuccess = DataContext.Instance.CreatePayment(pm);
            if(createsuccess==true)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}
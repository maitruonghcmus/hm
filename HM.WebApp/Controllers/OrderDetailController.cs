using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    [Authorize]
    public class OrderDetailController : Controller
    {
        // GET: OrderDetail
        public ActionResult Index()
        {
            if (AppContext.Instance.GetDayRemains() <= 0)
                return RedirectToAction("Expired", "Error");

            return View();
        }
    }
}
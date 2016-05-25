using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HM.DataModels;

namespace HM.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (AppContext.Instance.GetDayRemains() <= 0)
                return RedirectToAction("Expired", "Error");

            var revByDay = AppContext.Instance.GetRevenueByDay();
            ViewBag.RevByDay = revByDay;

            var revByWeek = AppContext.Instance.GetRevenueByWeek();
            ViewBag.RevByWeek = revByWeek;

            var revByMonth = AppContext.Instance.GetRevenueByMonth();
            ViewBag.RevByMonth = revByMonth;

            var revByYear = AppContext.Instance.GetRevenueByYear();
            ViewBag.RevByYear = revByYear;

            var newCustomer = AppContext.Instance.Get5NewestCustomers();
            ViewBag.NewCustomer = newCustomer;

            var customerCheckoutRecent = AppContext.Instance.Get5CusomerCheckedOutRecent();
            ViewBag.CheckOutRecent = customerCheckoutRecent;

            var result = AppContext.Instance.GetRevenueByRoomType(DateTime.Today.AddMonths(-1), DateTime.Today.AddDays(1));
            var revByRoomType = new Dictionary<string, double>();
            if (result != null)
            {
                foreach (var item in result)
                {
                    revByRoomType.Add(item.Key?.Name ?? "Chưa rõ", item.Value);
                }
            }

            ViewBag.RevenueByRoomType = revByRoomType;

            return View();
        }
    }
}
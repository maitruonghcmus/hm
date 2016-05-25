﻿using System;
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
            var revByWeek = AppContext.Instance.GetRevenueByWeek();
            var revByMonth = AppContext.Instance.GetRevenueByMonth();
            var revByYear = AppContext.Instance.GetRevenueByYear();

            ViewBag.RevByDay = revByDay;
            ViewBag.RevByWeek = revByWeek;
            ViewBag.RevByMonth = revByMonth;
            ViewBag.RevByYear = revByYear;

            return View();
        }
    }
}
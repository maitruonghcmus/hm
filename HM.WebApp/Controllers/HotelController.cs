using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    public class HotelController : Controller
    {
        public ActionResult Index()
        {
            var hotels = DataContext.Instance.GetHotels();
            ViewBag.Hotels = hotels;

            return View();
        }

        [HttpGet]
        public JsonResult GetHotel(int id)
        {
            var hotel = DataContext.Instance.GetHotel(id);
            return Json(hotel, JsonRequestBehavior.AllowGet);
        }
    }
}
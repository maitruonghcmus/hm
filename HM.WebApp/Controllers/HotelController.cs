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


        public ActionResult LoadHotelsPartial()
        {
            var hotels = DataContext.Instance.GetHotels();
            ViewBag.Hotels = hotels;

            return PartialView("_ShowHotelsPartial");
        }



        [HttpGet]
        public JsonResult GetHotelInfo(int htId)
        {
            var hotel = DataContext.Instance.GetHotel(htId);
            return Json(hotel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateHotel(Hotel ht)
        {
            var createSuccess = DataContext.Instance.CreateHotel(ht);

            if (createSuccess)
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteHotel(int htId)
        {
            var deleteSuccess = DataContext.Instance.DeleteHotel(htId);

            if (deleteSuccess)
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateHotel(Hotel ht)
        {
            var updateSuccess = DataContext.Instance.UpdateHotel(ht);
            if (updateSuccess) return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}
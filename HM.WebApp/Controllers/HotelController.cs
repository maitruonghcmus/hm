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
            var hotels = DataService.Instance.GetObjects<IEnumerable<Hotel>>(ApiUtils.HOTEL, ApiUtils.GETALL);
            ViewBag.Hotels = hotels;

            return View();
        }

        [HttpGet]
        public JsonResult GetHotel(int id)
        {
            var hotel = DataService.Instance.GetObject<Hotel>(ApiUtils.HOTEL, ApiUtils.GETBYID, id);
            if (hotel != null)
            {
                return Json(hotel, JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }

        [HttpPost]
        public ActionResult Create(Hotel h)
        {
            if (ModelState.IsValid)
            {
                var r = DataService.Instance.Post(ApiUtils.HOTEL, ApiUtils.CREATE, h);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(Hotel h)
        {
            if (ModelState.IsValid)
            {
                var r = DataService.Instance.Post(ApiUtils.HOTEL, ApiUtils.UPDATE, h);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var r = DataService.Instance.Delete<Hotel>(ApiUtils.HOTEL, ApiUtils.DELETE, id);
            return RedirectToAction("Index");
        }
    }
}
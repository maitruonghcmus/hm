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
            var hotels = DataService.Instance.GetObjects<IEnumerable<Hotel>>("Hotel", "GetAll");
            ViewBag.Hotels = hotels;

            return View();
        }

        [HttpGet]
        public JsonResult GetHotel(int id)
        {
            var hotel = DataService.Instance.GetObject<Hotel>("Hotel", "GetById", id);
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
                var r = DataService.Instance.Post("Hotel", "Create", h);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(Hotel h)
        {
            if (ModelState.IsValid)
            {
                var r = DataService.Instance.Post("Hotel", "Update", h);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var r = DataService.Instance.Delete<Hotel>("Hotel", "Delete", id);
            return RedirectToAction("Index");
        }
    }
}
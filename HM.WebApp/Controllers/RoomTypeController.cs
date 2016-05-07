using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    public class RoomTypeController : Controller
    {
        // GET: RoomType
        public ActionResult Index()
        {
            var hotelId = 1;

            var roomtypes = DataService.Instance.GetObjects<IEnumerable<RoomType>>(ApiUtils.ROOMTYPE, ApiUtils.GETALL)
                .Where(a=>a.HotelId == hotelId)
                .Select(a => a);
            ViewBag.RoomTypes = roomtypes;

            return View();
        }

        public ActionResult CreateRoomType(RoomType r)
        {
            var result = DataService.Instance.Post<RoomType>(ApiUtils.ROOMTYPE, ApiUtils.CREATE, r);
            if (result.IsSuccess())
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
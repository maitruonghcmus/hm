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
                .Where(a => a.HotelId == hotelId)
                .Select(a => a);
            ViewBag.RoomTypes = roomtypes;

            return View();
        }

        public ActionResult CreateRoomType(int id, string name, int maxCustomer, long price1, long price2, long price3, long price24)
        {
            var roomType = new RoomType
            {
                Id = id,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                HotelId = 1,
                MaxCustomer = maxCustomer,
                Name = name,
                Price = new Dictionary<int, long> { { 1, price1 }, { 2, price2 }, { 3, price3 }, { 24, price24 } }
            };

            var result = DataService.Instance.Post<RoomType>(ApiUtils.ROOMTYPE, ApiUtils.CREATE, roomType);
            if (result.IsSuccess())
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RemoveRoomType(RoomType r)
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
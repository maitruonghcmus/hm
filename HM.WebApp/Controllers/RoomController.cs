using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRoom(Room r)
        {
            var createSuccess = DataContext.Instance.CreateRoom(r);
            if(createSuccess)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoomList(int rtId)
        {
            var rooms = DataContext.Instance.GetRooms();
            var selected = rooms != null ? rooms.Where(a=>a.RoomTypeId == rtId).Select(a=>a) : null;

            ViewBag.RoomList = selected;
            return PartialView("_RoomListPartial");
        }
    }
}
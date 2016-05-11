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
        public ActionResult Index()
        {
            var roomtypes = DataContext.Instance.GetRoomTypes();
            ViewBag.RoomTypes = roomtypes;

            return View();
        }

        [HttpPost]
        public ActionResult CreateRoomType(RoomType roomType)
        {
            var createSuccess = DataContext.Instance.CreateRoomType(roomType);
            if (createSuccess) return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetRoomType(int id)
        {
            var room = DataContext.Instance.GetRoomType(id);
            return Json(room, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateRoomType(RoomType editRoomType)
        {
            var updateSuccess = DataContext.Instance.UpdateRoomType(editRoomType);
            if (updateSuccess) return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteRoomType(int id)
        {
            var deleteSuccess = DataContext.Instance.DeleteRoomType(id);
            if (deleteSuccess) return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}
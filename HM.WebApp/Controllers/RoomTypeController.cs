using HM.DataModels;
using HM.WebApp.Models;
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
            var roomtypes = DataContext.Instance.GetRoomTypes()?.Select(a => new RoomTypeModel(a));
            ViewBag.RoomTypes = roomtypes;
            return View();
        }

        public ActionResult LoadRoomTypePartial()
        {
            var roomtypes = DataContext.Instance.GetRoomTypes()?.Select(a => new RoomTypeModel(a));
            ViewBag.RoomTypes = roomtypes;

            return PartialView("_RoomTypePartial");
        }

        [HttpPost]
        public ActionResult CreateRoomType(RoomType roomType)
        {
            var createSuccess = DataContext.Instance.CreateRoomType(roomType);

            if (createSuccess)
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetRoomType(int id)
        {
            var rt = DataContext.Instance.GetRoomType(id);
            return Json(rt, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public ActionResult CreateRoom(Room r)
        {
            var createSuccess = DataContext.Instance.CreateRoom(r);
            if (createSuccess)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoomList(int rtId)
        {
            var rooms = DataContext.Instance.GetRooms()?.Where(a => a.RoomTypeId == rtId).Select(a => a).ToList();
            //var selected = rooms != null ? rooms.Where(a => a.RoomTypeId == rtId).Select(a => a) : null;

            ViewBag.RoomList = rooms;
            return PartialView("_RoomListPartial");
        }

        public ActionResult DeleteRoom(int rId)
        {
            var deleteSuccess = DataContext.Instance.DeleteRoom(rId);
            if (deleteSuccess)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoomInfo(int rId)
        {
            var room = DataContext.Instance.GetRoom(rId);

            return Json(room, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateRoom(Room r)
        {
            var updatesuccess = DataContext.Instance.UpdateRoom(r);
            if (updatesuccess)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}
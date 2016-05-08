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
            var hotelId = 1;

            var roomtypes = DataService.Instance.GetObjects<IEnumerable<RoomType>>(ApiUtils.ROOMTYPE, ApiUtils.GETALL)
                ?.Where(a => a.HotelId == hotelId && !a.Inactive)
                ?.Select(a => a);
            ViewBag.RoomTypes = roomtypes;

            return View();
        }

        [HttpPost]
        public ActionResult CreateRoomType(RoomType roomType)
        {
            //2 cái này trước mắt gán cứng, sau này có chức năng đăng nhập rồi gắn vô sau
            roomType.HotelId = 1;
            roomType.CreatedBy = 1;

            //2 cái này mặc định nên ko cần truyền từ trên, mà để dưới này gán giá trị cũng đc. Id mặc định = 0 để xuống dưới tự sinh
            roomType.Id = 0;
            roomType.CreatedOn = DateTime.Now;

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

        [HttpGet]
        public ActionResult GetRoomType(int id)
        {
            var room = DataService.Instance.GetObject<RoomType>(ApiUtils.ROOMTYPE, ApiUtils.GETBYID, id);
            if (room != null && !room.Inactive)
            {
                return Json(room, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateRoomType(RoomType editRoomType)
        {
            var result = DataService.Instance.Post<RoomType>(ApiUtils.ROOMTYPE, ApiUtils.UPDATE, editRoomType);
            if (result.IsSuccess())
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteRoomType(int id)
        {
            var result = DataService.Instance.Delete<RoomType>(ApiUtils.ROOMTYPE, ApiUtils.DELETE, id);
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
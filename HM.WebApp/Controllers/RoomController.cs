using HM.WebApp.Models;
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
            var rooms = DataContext.Instance.GetRooms();
            var roomModels = rooms?.Select(a => new RoomModel(a));
            ViewBag.Rooms = roomModels;

            return View();
        }
    }
}
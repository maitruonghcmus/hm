using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var users = DataContext.Instance.GetUsers();
            ViewBag.Users = users;
            return View();
        }


        public ActionResult GetUserInfo(int userId)
        {
            var user = DataContext.Instance.GetUser(userId);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateUser(User user)
        {
            var updateSuccess = DataContext.Instance.UpdateUser(user);
            if (updateSuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            var createSuccess = DataContext.Instance.CreateUser(user);
            if (createSuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Deleteuser(int userId)
        {
            var deleteSuccess = DataContext.Instance.DeleteCustomer(userId);
            if (deleteSuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadUsers()
        {
            var users = DataContext.Instance.GetUsers();
            ViewBag.Customers = users;
            return PartialView("_ShowUsers");
        }
    }
}
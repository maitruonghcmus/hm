using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    public class ExtraServiceController : Controller
    {
        // GET: ExtraService
        public ActionResult Index()
        {
            var services = DataContext.Instance.GetExtraServices();
            ViewBag.Services = services;
            return View();
        }


        public ActionResult GetExtraServiceInfo(int svId)
        {
            var service = DataContext.Instance.GetExtraService(svId);
            return Json(service, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateExtraService(ExtraService sv)
        {
            var updateSuccess = DataContext.Instance.UpdateExtraService(sv);
            if(updateSuccess){ return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateExtraService(ExtraService sv)
        {
            var createSuccess = DataContext.Instance.CreateExtraService(sv);
            if (createSuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteExtraService(int svId)
        {
            var deleteSuccess = DataContext.Instance.DeleteExtraService(svId);
            if (deleteSuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadExtraServices()
        {
            var services = DataContext.Instance.GetExtraServices();
            ViewBag.Services = services;
            return PartialView("_ShowExtraServices");
        }

    }
}
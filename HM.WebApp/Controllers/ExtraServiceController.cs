﻿using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    [Authorize]
    public class ExtraServiceController : Controller
    {
        // GET: ExtraService
        public ActionResult Index()
        {
            if (AppContext.Instance.GetDayRemains() <= 0)
                return RedirectToAction("Expired", "Error");

            var services = DataContext.Instance.GetExtraServices();
            ViewBag.Services = services;
            return View();
        }


        public ActionResult GetExtraServiceInfo(int svId)
        {
            var service = DataContext.Instance.GetExtraService(svId);

            if (service != null)
            {
                var obj = new
                {
                    service.Id,
                    service.HotelId,
                    service.Inactive,
                    service.ModifiedBy,
                    ModifiedOn = service.ModifiedOn?.ToLocalTime().ToString(DateTimeUtils.YYYY_MM_DD_HH_MM) ?? string.Empty,
                    service.CreatedBy,
                    CreatedOn = service.CreatedOn.ToLocalTime().ToString(DateTimeUtils.YYYY_MM_DD_HH_MM),
                    service.Price,
                    service.Name,
                    service.Unit,
                  
                };

                return Json(obj, JsonRequestBehavior.AllowGet);
            }


            return Json(null, JsonRequestBehavior.AllowGet);
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
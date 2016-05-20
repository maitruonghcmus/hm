using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    public class CustomerTypeController : Controller
    {

        public ActionResult Index()
        {
            var ctmTypes = DataContext.Instance.GetCustomerTypes();
            ViewBag.CustomerTypes = ctmTypes;
            return View();
        }


        public ActionResult GetCustomerTypeInfo(int ctmTypeId)
        {
            var ctmType = DataContext.Instance.GetCustomerType(ctmTypeId);
            return Json(ctmType, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult UpdateCustomerType(CustomerType ctmType) 
        {
            var updateSuccess = DataContext.Instance.UpdateCustomerType(ctmType);
            if (updateSuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateCustomerType(string name, string coe)
        {
            var ctype = new CustomerType
            {
                Coefficient = float.Parse(coe, CultureInfo.InvariantCulture),
                Name = name
            };

            var createSuccess = DataContext.Instance.CreateCustomerType(ctype);
            if (createSuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteCustomerType(int ctmTypeId)
        {
            var deleteSuccess = DataContext.Instance.DeleteCustomerType(ctmTypeId);
            if (deleteSuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadCustomerTypes()
        {
            var ctmTypes = DataContext.Instance.GetCustomerTypes();
            ViewBag.CustomerTypes = ctmTypes;
            return PartialView("_ShowCustomerTypes");
        }


    }
}
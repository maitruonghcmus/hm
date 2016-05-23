using HM.DataModels;
using HM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            var ctms = DataContext.Instance.GetCustomers();
            var ctmTypes = DataContext.Instance.GetCustomerTypes();
            ViewBag.Customers = ctms;
            ViewBag.CustomerTypes = ctmTypes;
            return View();
        }

        public ActionResult GetCustomerInfo(int ctmId)
        {
            var ctm = DataContext.Instance.GetCustomer(ctmId);
            if (ctm != null)
            {
                var obj = new
                {
                    ctm.Id,
                    ctm.HotelId,
                    ctm.Inactive,
                    ctm.ModifiedBy,
                    ModifiedOn = ctm.ModifiedOn?.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM) ?? string.Empty,
                    ctm.CreatedBy,
                    CreatedOn = ctm.CreatedOn.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM),
                    ctm.Address,
                    ctm.Name,
                    ctm.CardId,
                    ctm.Phone,
                    ctm.CustomerTypeId
                };

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateCustomer(Customer ctm)
        {
            var updateSuccess = DataContext.Instance.UpdateCustomer(ctm);
            if (updateSuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateCustomer(Customer ctm)
        {
            var createSuccess = DataContext.Instance.CreateCustomer(ctm);
            if (createSuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int ctmId)
        {
            var deleteSuccess = DataContext.Instance.DeleteCustomer(ctmId);
            if (deleteSuccess) { return Json(true, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadCustomers()
        {
            var ctms = DataContext.Instance.GetCustomers();
            ViewBag.Customers = ctms;
            return PartialView("_ShowCustomers");
        }

        [HttpPost]
        public ActionResult LoadCustomersTable(int? start, int? length)
        {

            var tableSearchText = Request.Params["search[value]"];

            var ctms = DataContext.Instance.GetCustomers()
                         ?.Where(c =>
                                (c.Name + c.CardId + c.Phone + c.Address).ToLower()
                                    .Contains(tableSearchText.ToLower())
                                )
                         .Select(c => c);

            var dataFiltered = ctms?.Skip(start ?? 0).Take(Math.Min(length ?? 0, ctms.Count() - start ?? 0))
                .Select(c => new
                {
                    c.Id,
                    Name = "<b>" + c.Name + "</b> <br/><i class='fa fa-info'></i> " + (DataContext.Instance.GetCustomerType(c.CustomerTypeId)?.Name ?? "Chưa rõ"),
                    c.CardId,
                    c.Phone,
                    c.Address,
                    CreatedBy = "<i class='fa fa-user'></i> " + (DataContext.Instance.GetUser(c.CreatedBy)?.Fullname ?? "Đang cập nhật") + "<br/><i class='fa fa-clock-o'></i> " + c.CreatedOn.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM),
                    ModifiedBy = "<i class='fa fa-user'></i> " + (DataContext.Instance.GetUser(c.ModifiedBy ?? 0)?.Fullname ?? "Chưa chỉnh sửa") + "<br/><i class='fa fa-clock-o'></i> " + c.ModifiedOn?.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM) ?? "Chưa chỉnh sửa",
                    Command = "<button type='button' onclick='editCustomer(" + c.Id + ")' class='btn btn-icon-toggle' data-toggle='modal' data-target='#modalAddCustomer'><i class='fa fa-pencil'></i></button>"
                            + "<button type='button' onclick='deleteCustomer(" + c.Id + ")' class='btn btn-icon-toggle'><i class='fa fa-trash-o'></i></button>"
                });

            var dataTable = new DataTable<object>
            {
                recordsTotal = ctms?.Count() ?? 0,
                recordsFiltered = ctms?.Count() ?? 0,
                data = dataFiltered
            };

            return Json(dataTable, JsonRequestBehavior.AllowGet);
        }
    }
}
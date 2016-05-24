using HM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    public class OrderedCustomerController : Controller
    {
        // GET: OrderedCustomer
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LoadOrderedCustomersTable(int? start, int? length)
        {

            var tableSearchText = Request.Params["search[value]"];

            var ordctms = DataContext.Instance.GetOrders()
                         ?.Where(o =>
                                (o.CheckInDate.ToString() + (DataContext.Instance.GetCustomer(o.CustomerId)?.Name ?? "")).ToLower()
                                    .Contains(tableSearchText.ToLower())
                                )
                         .Select(o => o);

            var dataFiltered = ordctms?.Skip(start ?? 0).Take(Math.Min(length ?? 0, ordctms.Count() - start ?? 0))
                .Select(o => new
                {
                    o.Id,
                    CustomerName = "<i class='fa fa-user'></i> " + (DataContext.Instance.GetCustomer(o.CustomerId)?.Name ?? "Không lấy được tên khách hàng"),
                    RoomName = "<i class='fa fa-bed'></i> " + (DataContext.Instance.GetRoom(o.RoomId)?.Name ?? "Không lấy được tên phòng"),
                    CheckInDate = "<i class='fa fa-calendar'></i> " + o.CheckInDate.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM),


                    CreatedBy = "<i class='fa fa-user'></i> " + (DataContext.Instance.GetUser(o.CreatedBy)?.Fullname ?? "Đang cập nhật") + "<br/><i class='fa fa-clock-o'></i> " + o.CreatedOn.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM)
                   
                });

            var dataTable = new DataTable<object>
            {
                recordsTotal = ordctms?.Count() ?? 0,
                recordsFiltered = ordctms?.Count() ?? 0,
                data = dataFiltered
            };

            return Json(dataTable, JsonRequestBehavior.AllowGet);
        }
    }
}
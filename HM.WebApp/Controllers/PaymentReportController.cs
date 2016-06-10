using HM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    public class PaymentReportController : Controller
    {
        // GET: PaymentReport
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadPaymentReport(int? start, int? length)
        {

            var tableSearchText = Request.Params["search[value]"];

            var pm = DataContext.Instance.GetPayments()
                         ?.Where(p =>
                                ((DataContext.Instance.GetRoom((DataContext.Instance.GetOrder(p.OrderId)?.RoomId ?? -1))?.Name ?? "") + (DataContext.Instance.GetCustomer(p.CustomerId)?.Name ?? "")).ToLower()
                                    .Contains(tableSearchText.ToLower())
                                )
                         .Select(p => p);

            var dataFiltered = pm?.Skip(start ?? 0).Take(Math.Min(length ?? 0, pm.Count() - start ?? 0))
                .Select(p => new
                {
                    p.Id,
                    CustomerName = "<i class='fa fa-user'></i> " + (DataContext.Instance.GetCustomer(p.CustomerId)?.Name ?? "Không lấy được tên khách hàng"),
                    RoomName = "<i class='fa fa-bed'></i> " + (DataContext.Instance.GetRoom((DataContext.Instance.GetOrder(p.OrderId)?.RoomId ?? -1))?.Name ?? "Không lấy được tên phòng"),
                    CheckOutDate = "<i class='fa fa-calendar'></i> " + p.CheckOutDate.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM),
                    Amount = "<i class='fa fa-hotel'></i> " + p.Amount.ToString("#,##0") + " đ <br><i class='fa fa-plus-square-o'></i> Phụ thu: " + p.OtherCharge.ToString("#,##0") + " đ <br><i class='fa fa-minus-square-o'></i> Giảm giá: " + p.DiscountPercent.ToString() + " %",
                    Total = p.Total.ToString("#,##0") + " đ",

                    CreatedBy = "<i class='fa fa-user'></i> " + (DataContext.Instance.GetUser(p.CreatedBy)?.Fullname ?? "Đang cập nhật") + "<br/><i class='fa fa-clock-o'></i> " + p.CreatedOn.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM)

                });

            var dataTable = new DataTable<object>
            {
                recordsTotal = pm?.Count() ?? 0,
                recordsFiltered = pm?.Count() ?? 0,
                data = dataFiltered
            };

            return Json(dataTable, JsonRequestBehavior.AllowGet);
        }
    }
}
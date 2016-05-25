using HM.DataModels;
using HM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    [Authorize]
    public class HotelController : Controller
    {
        public ActionResult Index()
        {
            if (!AppContext.Instance.IsAdministrator())
                return RedirectToAction("Index", "Error");

            var hotels = DataContext.Instance.GetHotels();
            ViewBag.Hotels = hotels;

            return View();
        }

        [HttpPost]
        public ActionResult LoadHotelsTable(int? start, int? length)
        {
            var tableSearchText = Request.Params["search[value]"];

            var hotels = DataContext.Instance.GetHotels()
                         ?.Where(h =>
                                (h.Name + h.Address + h.Contact + h.ContactMail + h.ContactPhone + h.CreatedBy + h.CreatedOn + h.TaxCode).ToLower()
                                    .Contains(tableSearchText.ToLower())
                                )
                         .Select(h => h);

            var dataFiltered = hotels?.Skip(start ?? 0).Take(Math.Min(length ?? 0, hotels.Count() - start ?? 0))
                    .Select(h => new
                    {
                        h.Id,
                        Name = h.Name + "<br/><i class='fa fa-map-marker'></i> " + h.Address,
                        h.TaxCode,
                        Contact = h.Contact + "<br/><i class='fa fa-phone'></i> " + h.ContactPhone + "<br/><i class='fa fa-envelope-o'></i>  " + h.ContactMail,
                        CreatedBy = DataContext.Instance.GetUser(h.CreatedBy)?.Fullname ?? "Đang cập nhật",
                        CreatedOn = h.CreatedOn.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM),
                        ModifiedBy = DataContext.Instance.GetUser(h.ModifiedBy ?? 0)?.Fullname ?? "",
                        ModifiedOn = h.ModifiedOn?.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM) ?? "",
                        Command = "<button type='button' onclick='editHotel(" + h.Id + ")' class='btn btn-icon-toggle' data-toggle='modal' data-target='#modalAddHotel'><i class='fa fa-pencil'></i></button>"
                                + "<button type='button' onclick='deleteHotel(" + h.Id + ")' class='btn btn-icon-toggle'><i class='fa fa-trash-o'></i></button>"
                    });

            var dataTable = new DataTable<object>
            {
                recordsTotal = hotels?.Count() ?? 0, //Tổng số record nè, nếu hotel null thì là 0 record nè
                recordsFiltered = hotels?.Count() ?? 0,
                data = dataFiltered
            };

            return Json(dataTable, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetHotelInfo(int htId)
        {
            var hotel = DataContext.Instance.GetHotel(htId);
            if (hotel != null)
            {
                var obj = new
                {
                    hotel.Id,
                    hotel.Address,
                    hotel.Contact,
                    hotel.ContactMail,
                    hotel.ContactPhone,
                    hotel.CreatedBy,
                    CreatedOn = hotel.CreatedOn.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM),
                    hotel.Inactive,
                    hotel.ModifiedBy,
                    ModifiedOn = hotel.ModifiedOn?.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM) ?? string.Empty,
                    hotel.Name,
                    hotel.TaxCode
                };

                return Json(obj, JsonRequestBehavior.AllowGet);
            }


            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateHotel(Hotel ht)
        {
            var createSuccess = DataContext.Instance.CreateHotel(ht);

            if (createSuccess)
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteHotel(int htId)
        {
            var deleteSuccess = DataContext.Instance.DeleteHotel(htId);

            if (deleteSuccess)
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateHotel(Hotel ht)
        {
            var updateSuccess = DataContext.Instance.UpdateHotel(ht);

            if (updateSuccess)
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}
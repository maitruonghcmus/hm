﻿using HM.DataModels;
using HM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.WebApp.Controllers
{
    public class HotelController : Controller
    {
        public ActionResult Index()
        {
            var hotels = DataContext.Instance.GetHotels();
            ViewBag.Hotels = hotels;

            return View();
        }

        [HttpPost]
        public ActionResult LoadHotelsTable(int? start, int? length)
        {
            var tableSearchText = Request.Params["search[value]"]; //lấy text từ khung search của table nè

            var hotels = DataContext.Instance.GetHotels() // lấy danh sách hotel nè
                         ?.Where(h => //check null rồi đem where để lọc dữ liệu nè
                                (h.Name + h.Address + h.Contact + h.ContactMail + h.ContactPhone + h.CreatedBy + h.CreatedOn + h.TaxCode).ToLower() // cộng chuỗi các field của hotel để đem so sánh với tableSearchText nè
                                    .Contains(tableSearchText.ToLower()) //so nếu chuỗi trên có chứa tableSearchText nè
                                )
                         .Select(h => h);

            var dataFiltered = hotels?.Skip(start ?? 0).Take(Math.Min(length ?? 0, hotels.Count() - start ?? 0)) //xong lấy 10 record từ start đến length để show lên trên table nè
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
            return Json(hotel, JsonRequestBehavior.AllowGet);
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
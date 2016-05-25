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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (!AppContext.Instance.IsAdministrator())
                return RedirectToAction("Index", "Error");

            var users = DataContext.Instance.GetUsers();
            var roles = DataContext.Instance.GetRoles();
            var hotels = DataContext.Instance.GetHotels();

            ViewBag.Roles = roles;
            ViewBag.Users = users;
            ViewBag.Hotels = hotels;
            return View();
        }


        public ActionResult GetUserInfo(int userId)
        {
            var user = DataContext.Instance.GetUser(userId);
            if (user != null)
            {
                var obj = new
                {
                    user.Id,
                    user.HotelId,
                    user.Inactive,
                    user.ModifiedBy,
                    ModifiedOn = user.ModifiedOn?.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM) ?? string.Empty,
                    user.CreatedBy,
                    CreatedOn = user.CreatedOn.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM),
                    user.Fullname,
                    user.Password,
                    user.Username,
                    user.RoleId,
                };

                return Json(obj, JsonRequestBehavior.AllowGet);
            }


            return Json(null, JsonRequestBehavior.AllowGet);
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
        public ActionResult DeleteUser(int userId)
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


        [HttpPost]
        public ActionResult LoadUsersTable(int? start, int? length)
        {

            var tableSearchText = Request.Params["search[value]"]; //lấy text từ khung search của table nè

            var ctms = DataContext.Instance.GetUsers() // lấy danh sách khach hang nè
                         ?.Where(u => //check null rồi đem where để lọc dữ liệu nè
                                (u.Username + u.Fullname).ToLower() // cộng chuỗi các field của hotel để đem so sánh với tableSearchText nè
                                    .Contains(tableSearchText.ToLower()) //so nếu chuỗi trên có chứa tableSearchText nè
                                )
                         .Select(u => u);

            var dataFiltered = ctms?.Skip(start ?? 0).Take(Math.Min(length ?? 0, ctms.Count() - start ?? 0)) //xong lấy 10 record từ start đến length để show lên trên table nè
                    .Select(u => new
                    {
                        u.Id,
                        Name = "<b>" + u.Fullname + "</b>",
                        u.Username,
                        Hotel = DataContext.Instance.GetHotel(u.HotelId)?.Name ?? "Chưa rõ",
                        Role = (DataContext.Instance.GetRole(u.RoleId)?.Name ?? "Chưa rõ"),
                        CreatedBy = "<i class='fa fa-user'></i> " + (DataContext.Instance.GetUser(u.CreatedBy)?.Fullname ?? "Đang cập nhật") + "<br/><i class='fa fa-clock-o'></i> " + u.CreatedOn.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM),
                        ModifiedBy = "<i class='fa fa-user'></i> " + (DataContext.Instance.GetUser(u.ModifiedBy ?? 0)?.Fullname ?? "Chưa chỉnh sửa") + "<br/><i class='fa fa-clock-o'></i> " + u.ModifiedOn?.ToString(DateTimeUtils.YYYY_MM_DD_HH_MM) ?? "Chưa chỉnh sửa",
                        Command = "<button type='button' onclick='editUser(" + u.Id + ")' class='btn btn-icon-toggle' data-toggle='modal' data-target='#modalAddUser'><i class='fa fa-pencil'></i></button>"
                                + "<button type='button' onclick='deleteUser(" + u.Id + ")' class='btn btn-icon-toggle'><i class='fa fa-trash-o'></i></button>"
                    });

            var dataTable = new DataTable<object>
            {
                recordsTotal = ctms?.Count() ?? 0, //Tổng số record nè, nếu hotel null thì là 0 record nè
                recordsFiltered = ctms?.Count() ?? 0,
                data = dataFiltered
            };

            return Json(dataTable, JsonRequestBehavior.AllowGet);
        }
    }
}
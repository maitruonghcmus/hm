using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HM.DataModels;

namespace HM.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var newCustomer = AppContext.Instance.Top5NewestCustomers(AppContext.Instance.GetHotelId(AppContext.Instance.GetLoginUserId()));

            var newCustomer = new List<Customer>();

#if DEBUG
            if (newCustomer == null || newCustomer.Count() == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    newCustomer.Add(new Customer { Id = i, Name = "Nhan" + i.ToString()  });
                }
            }
#endif


            ViewBag.NewCustomer = newCustomer;

            return View();
        }
    }
}
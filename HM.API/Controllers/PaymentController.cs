using HM.DataModels;
using HM.DataModels.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HM.API.Controllers
{
    public class PaymentController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<Payment>> GetAll()
        {
            return new DBContext<Payment>(DbUtils.PaymentCollection).GetObjects();
        }

        [HttpGet]
        public Result<Payment> GetById(int id)
        {
            return new DBContext<Payment>(DbUtils.PaymentCollection).GetObject(id);
        }

        [HttpPost]
        public Result<Payment> Create(Payment pm)
        {
            return new DBContext<Payment>(DbUtils.PaymentCollection).Insert(pm);
        }

        [HttpPost]
        public Result<Payment> Update(Payment pm)
        {
            return new DBContext<Payment>(DbUtils.PaymentCollection).Replace(pm);
        }

        [HttpDelete]
        public Result<Payment> Delete(int id)
        {
            return new DBContext<Payment>(DbUtils.PaymentCollection).Delete(id);
        }
    }
}

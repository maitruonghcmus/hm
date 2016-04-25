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
        public Result<IEnumerable<Payment>> GetAll(string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<IEnumerable<Payment>> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Payment>(DbUtils.PaymentCollection).GetObjects();
        }

        [HttpGet]
        public Result<Payment> GetById(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Payment> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Payment>(DbUtils.PaymentCollection).GetObject(id);
        }

        [HttpPost]
        public Result<Payment> Create(Payment pm, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Payment> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Payment>(DbUtils.PaymentCollection).Insert(pm);
        }

        [HttpPost]
        public Result<Payment> Update(Payment pm, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Payment> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Payment>(DbUtils.PaymentCollection).Replace(pm);
        }

        [HttpDelete]
        public Result<Payment> Delete(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Payment> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Payment>(DbUtils.PaymentCollection).Delete(id);
        }
    }
}

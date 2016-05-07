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
    public class CustomerController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<Customer>> GetAll()
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<IEnumerable<Customer>> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Customer>(DbUtils.CustomerCollection).GetObjects();
        }

        [HttpGet]
        public Result<Customer> GetById(int id)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<Customer> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Customer>(DbUtils.CustomerCollection).GetObject(id);
        }

        [HttpPost]
        public Result<Customer> Create(Customer cus)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<Customer> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Customer>(DbUtils.CustomerCollection).Insert(cus);
        }

        [HttpPost]
        public Result<Customer> Update(Customer cus)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<Customer> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Customer>(DbUtils.CustomerCollection).Replace(cus);
        }

        [HttpDelete]
        public Result<Customer> Delete(int id, string apiKey)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<Customer> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Customer>(DbUtils.CustomerCollection).Delete(id);
        }
    }
}

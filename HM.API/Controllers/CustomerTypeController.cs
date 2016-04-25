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
    public class CustomerTypeController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<CustomerType>> GetAll(string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<IEnumerable<CustomerType>> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<CustomerType>(DbUtils.CustomerTypeCollection).GetObjects();
        }

        [HttpGet]
        public Result<CustomerType> GetById(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<CustomerType> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<CustomerType>(DbUtils.CustomerTypeCollection).GetObject(id);
        }

        [HttpPost]
        public Result<CustomerType> Create(CustomerType type, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<CustomerType> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<CustomerType>(DbUtils.CustomerTypeCollection).Insert(type);
        }

        [HttpPost]
        public Result<CustomerType> Update(CustomerType type, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<CustomerType> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<CustomerType>(DbUtils.CustomerTypeCollection).Replace(type);
        }

        [HttpDelete]
        public Result<CustomerType> Delete(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<CustomerType> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<CustomerType>(DbUtils.CustomerTypeCollection).Delete(id);
        }
    }
}

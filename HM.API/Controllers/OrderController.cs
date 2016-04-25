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
    public class OrderController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<Order>> GetAll(string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<IEnumerable<Order>> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Order>(DbUtils.OrderCollection).GetObjects();
        }

        [HttpGet]
        public Result<Order> GetById(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Order> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Order>(DbUtils.OrderCollection).GetObject(id);
        }

        [HttpPost]
        public Result<Order> Create(Order ord, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Order> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Order>(DbUtils.OrderCollection).Insert(ord);
        }

        [HttpPost]
        public Result<Order> Update(Order ord, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Order> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Order>(DbUtils.OrderCollection).Replace(ord);
        }

        [HttpDelete]
        public Result<Order> Delete(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Order> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Order>(DbUtils.OrderCollection).Delete(id);
        }
    }
}

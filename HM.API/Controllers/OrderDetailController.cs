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
    public class OrderDetailController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<OrderDetail>> GetAll(string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<IEnumerable<OrderDetail>> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<OrderDetail>(DbUtils.OrderDetailCollection).GetObjects();
        }

        [HttpGet]
        public Result<OrderDetail> GetById(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<OrderDetail> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<OrderDetail>(DbUtils.OrderDetailCollection).GetObject(id);
        }

        [HttpPut]
        public Result<OrderDetail> Create(OrderDetail od, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<OrderDetail> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<OrderDetail>(DbUtils.OrderDetailCollection).Insert(od);
        }

        [HttpPost]
        public Result<OrderDetail> Update(OrderDetail od, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<OrderDetail> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<OrderDetail>(DbUtils.OrderDetailCollection).Replace(od);
        }

        [HttpDelete]
        public Result<OrderDetail> Delete(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<OrderDetail> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<OrderDetail>(DbUtils.OrderDetailCollection).Delete(id);
        }
    }
}

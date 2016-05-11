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
        public Result<IEnumerable<OrderDetail>> GetAll()
        {
            return new DBContext<OrderDetail>(DbUtils.OrderDetailCollection).GetObjects();
        }

        [HttpGet]
        public Result<OrderDetail> GetById(int id)
        {
            return new DBContext<OrderDetail>(DbUtils.OrderDetailCollection).GetObject(id);
        }

        [HttpPost]
        public Result<OrderDetail> Create(OrderDetail od)
        {
            return new DBContext<OrderDetail>(DbUtils.OrderDetailCollection).Insert(od);
        }

        [HttpPost]
        public Result<OrderDetail> Update(OrderDetail od)
        {
            return new DBContext<OrderDetail>(DbUtils.OrderDetailCollection).Replace(od);
        }

        [HttpDelete]
        public Result<OrderDetail> Delete(int id)
        {
            return new DBContext<OrderDetail>(DbUtils.OrderDetailCollection).Delete(id);
        }
    }
}

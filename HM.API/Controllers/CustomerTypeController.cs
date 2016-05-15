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
        public Result<IEnumerable<CustomerType>> GetAll()
        {
            return new DBContext<CustomerType>(DbUtils.CustomerTypeCollection).GetObjects();
        }

        [HttpGet]
        public Result<CustomerType> GetById(int id)
        {
            return new DBContext<CustomerType>(DbUtils.CustomerTypeCollection).GetObject(id);
        }

        [HttpPost]
        public Result<CustomerType> Create(CustomerType type)
        {
            return new DBContext<CustomerType>(DbUtils.CustomerTypeCollection).Insert(type);
        }

        [HttpPost]
        public Result<CustomerType> Update(CustomerType type)
        {
            return new DBContext<CustomerType>(DbUtils.CustomerTypeCollection).Replace(type);
        }

        [HttpDelete]
        public Result<CustomerType> Delete(int id)
        {
            return new DBContext<CustomerType>(DbUtils.CustomerTypeCollection).Delete(id);
        }
    }
}

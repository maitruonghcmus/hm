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
            return new DBContext<Customer>(DbUtils.CustomerCollection).GetObjects();
        }

        [HttpGet]
        public Result<Customer> GetById(int id)
        {
            return new DBContext<Customer>(DbUtils.CustomerCollection).GetObject(id);
        }

        [HttpPost]
        public Result<Customer> Create(Customer cus)
        {
            return new DBContext<Customer>(DbUtils.CustomerCollection).Insert(cus);
        }

        [HttpPost]
        public Result<Customer> Update(Customer cus)
        {
            return new DBContext<Customer>(DbUtils.CustomerCollection).Replace(cus);
        }

        [HttpDelete]
        public Result<Customer> Delete(int id)
        {
            return new DBContext<Customer>(DbUtils.CustomerCollection).Delete(id);
        }
    }
}

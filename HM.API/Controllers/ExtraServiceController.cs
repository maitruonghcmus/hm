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
    public class ExtraServiceController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<ExtraService>> GetAll(string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<IEnumerable<ExtraService>> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<ExtraService>(DbUtils.ExtraServiceCollection).GetObjects();
        }

        [HttpGet]
        public Result<ExtraService> GetById(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<ExtraService> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<ExtraService>(DbUtils.ExtraServiceCollection).GetObject(id);
        }

        [HttpPost]
        public Result<ExtraService> Create(ExtraService ex, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<ExtraService> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<ExtraService>(DbUtils.ExtraServiceCollection).Insert(ex);
        }

        [HttpPost]
        public Result<ExtraService> Update(ExtraService ex, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<ExtraService> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<ExtraService>(DbUtils.ExtraServiceCollection).Replace(ex);
        }

        [HttpDelete]
        public Result<ExtraService> Delete(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<ExtraService> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<ExtraService>(DbUtils.ExtraServiceCollection).Delete(id);
        }
    }
}

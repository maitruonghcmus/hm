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
    public class RoomTypeController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<RoomType>> GetAll(string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<IEnumerable<RoomType>> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<RoomType>(DbUtils.RoomTypeCollection).GetObjects();
        }

        [HttpGet]
        public Result<RoomType> GetById(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<RoomType> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<RoomType>(DbUtils.RoomTypeCollection).GetObject(id);
        }

        [HttpPut]
        public Result<RoomType> Create(RoomType type, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<RoomType> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<RoomType>(DbUtils.RoomTypeCollection).Insert(type);
        }

        [HttpPost]
        public Result<RoomType> Update(RoomType type, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<RoomType> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<RoomType>(DbUtils.RoomTypeCollection).Replace(type);
        }

        [HttpDelete]
        public Result<RoomType> Delete(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<RoomType> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<RoomType>(DbUtils.RoomTypeCollection).Delete(id);
        }
    }
}
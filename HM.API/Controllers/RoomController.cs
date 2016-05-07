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
    public class RoomController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<Room>> GetAll()
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<IEnumerable<Room>> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Room>(DbUtils.RoomCollection).GetObjects();
        }

        [HttpGet]
        public Result<Room> GetById(int id)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<Room> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Room>(DbUtils.RoomCollection).GetObject(id);
        }

        [HttpPost]
        public Result<Room> Create(Room room)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<Room> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Room>(DbUtils.RoomCollection).Insert(room);
        }

        [HttpPost]
        public Result<Room> Update(Room room)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<Room> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Room>(DbUtils.RoomCollection).Replace(room);
        }

        [HttpDelete]
        public Result<Room> Delete(int id)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<Room> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Room>(DbUtils.RoomCollection).Delete(id);
        }
    }
}

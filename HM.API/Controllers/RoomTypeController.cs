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
        public Result<IEnumerable<RoomType>> GetAll()
        {
            return new DBContext<RoomType>(DbUtils.RoomTypeCollection).GetObjects();
        }

        [HttpGet]
        public Result<RoomType> GetById(int id)
        {
            return new DBContext<RoomType>(DbUtils.RoomTypeCollection).GetObject(id);
        }

        [HttpPost]
        public Result<RoomType> Create(RoomType type)
        {
            return new DBContext<RoomType>(DbUtils.RoomTypeCollection).Insert(type);
        }

        [HttpPost]
        public Result<RoomType> Update(RoomType type)
        {
            return new DBContext<RoomType>(DbUtils.RoomTypeCollection).Replace(type);
        }

        [HttpDelete]
        public Result<RoomType> Delete(int id)
        {
            return new DBContext<RoomType>(DbUtils.RoomTypeCollection).Delete(id);
        }
    }
}
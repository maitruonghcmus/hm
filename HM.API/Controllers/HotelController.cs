﻿using HM.DataModels;
using HM.DataModels.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HM.API.Controllers
{
    public class HotelController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<Hotel>> GetAll(string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<IEnumerable<Hotel>> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Hotel>(DbUtils.HotelCollection).GetObjects();
        }

        [HttpGet]
        public Result<Hotel> GetById(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Hotel> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Hotel>(DbUtils.HotelCollection).GetObject(id);
        }

        [HttpPut]
        public Result<Hotel> Create(Hotel hotel, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Hotel> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Hotel>(DbUtils.HotelCollection).Insert(hotel);
        }

        [HttpPost]
        public Result<Hotel> Update(Hotel hotel, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Hotel> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Hotel>(DbUtils.HotelCollection).Replace(hotel);
        }

        [HttpDelete]
        public Result<Hotel> Delete(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Hotel> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Hotel>(DbUtils.HotelCollection).Delete(id);
        }
    }
}

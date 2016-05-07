using HM.DataModels;
using HM.DataModels.Utils;
using System.Collections.Generic;
using System.Web.Http;

namespace HM.API.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<User>> GetAll()
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<IEnumerable<User>> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<User>(DbUtils.UserCollection).GetObjects();
        }

        [HttpGet]
        public Result<User> GetById(int id)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<User> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<User>(DbUtils.UserCollection).GetObject(id);
        }

        [HttpPost]
        public Result<User> Create(User user)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<User> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<User>(DbUtils.UserCollection).Insert(user);
        }

        [HttpPost]
        public Result<User> Update(User user)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<User> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<User>(DbUtils.UserCollection).Replace(user);
        }

        [HttpDelete]
        public Result<User> Delete(int id)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<User> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<User>(DbUtils.UserCollection).Delete(id);
        }
    }
}
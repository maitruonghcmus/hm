using HM.DataModels;
using HM.DataModels.Utils;
using System.Collections.Generic;
using System.Web.Http;

namespace HM.API.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<User>> GetAll(string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<IEnumerable<User>> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<User>(DbUtils.UserCollection).GetObjects();
        }

        [HttpGet]
        public Result<User> GetById(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<User> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<User>(DbUtils.UserCollection).GetObject(id);
        }

        [HttpPut]
        public Result<User> Create(User user, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<User> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            //#if DEBUG
            //            if (user == null)
            //                user = new User
            //                {
            //                    Username = "debugmode",
            //                    Password = "1",
            //                    Fullname = "User Created In Debug Mode",
            //                    RoleId = 0,
            //                    CreatedBy = 1,
            //                    CreatedOn = DateTime.Now,
            //                };
            //#endif

            return new DBContext<User>(DbUtils.UserCollection).Insert(user);
        }

        [HttpPost]
        public Result<User> Update(User user, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<User> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<User>(DbUtils.UserCollection).Replace(user);
        }

        [HttpDelete]
        public Result<User> Delete(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<User> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<User>(DbUtils.UserCollection).Delete(id);
        }
    }
}
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
            return new DBContext<User>(DbUtils.UserCollection).GetObjects();
        }

        [HttpGet]
        public Result<User> GetById(int id)
        {
            return new DBContext<User>(DbUtils.UserCollection).GetObject(id);
        }

        [HttpPost]
        public Result<User> Create(User user)
        {
            return new DBContext<User>(DbUtils.UserCollection).Insert(user);
        }

        [HttpPost]
        public Result<User> Update(User user)
        {
            return new DBContext<User>(DbUtils.UserCollection).Replace(user);
        }

        [HttpDelete]
        public Result<User> Delete(int id)
        {
            return new DBContext<User>(DbUtils.UserCollection).Delete(id);
        }
    }
}
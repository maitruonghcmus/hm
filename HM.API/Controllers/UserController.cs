using HM.DataModels;
using HM.DataModels.Utils;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public Result<User> GetByUsername(string username)
        {
            var re = new DBContext<User>(DbUtils.UserCollection).GetObjectsByIndex("Username", username);
            var user = new User();
            if (re.IsSuccess())
            {
                user = re.Data?.FirstOrDefault();
            }

            return new Result<User> { Data = user, Code = re.Code };
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
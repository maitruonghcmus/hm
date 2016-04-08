using System.Collections.Generic;
using System.Web.Http;
using HM.DataModels;
using HM.DataModels.Utils;

namespace HM.API.Controllers
{
    public class RoleController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<Role>> GetAll(string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<IEnumerable<Role>> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Role>(DbUtils.RoleCollection).GetObjects();
        }

        [HttpGet]
        public Result<Role> GetById(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Role> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Role>(DbUtils.RoleCollection).GetObject(id);
        }

        [HttpPut]
        public Result<Role> Create(Role role, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Role> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

#if DEBUG
            if (role == null)
                role = new Role { Name = "TEST" };
#endif

            return new DBContext<Role>(DbUtils.RoleCollection).Insert(role);
        }

        [HttpPost]
        public Result<Role> Update(Role role, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Role> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

#if DEBUG
            role.Id = 2;
            role.Name = "Quản lý";
#endif

            return new DBContext<Role>(DbUtils.RoleCollection).Replace(role);
        }

        [HttpDelete]
        public Result<Role> Delete(int id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Role> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Role>(DbUtils.RoleCollection).Delete(id);
        }
    }
}

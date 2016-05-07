using System.Collections.Generic;
using System.Web.Http;
using HM.DataModels;
using HM.DataModels.Utils;

namespace HM.API.Controllers
{
    public class RoleController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<Role>> GetAll()
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<IEnumerable<Role>> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Role>(DbUtils.RoleCollection).GetObjects();
        }

        [HttpGet]
        public Result<Role> GetById(int id)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<Role> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Role>(DbUtils.RoleCollection).GetObject(id);
        }

        [HttpPost]
        public Result<Role> Create(Role role)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<Role> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Role>(DbUtils.RoleCollection).Insert(role);
        }

        [HttpPost]
        public Result<Role> Update(Role role)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<Role> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Role>(DbUtils.RoleCollection).Replace(role);
        }

        [HttpDelete]
        public Result<Role> Delete(int id)
        {
            //if (apiKey != DbUtils.ApiKey)
            //    return new Result<Role> { Code = MessageUtils.ERR_LOGIN_REQUIRED };

            return new DBContext<Role>(DbUtils.RoleCollection).Delete(id);
        }
    }
}

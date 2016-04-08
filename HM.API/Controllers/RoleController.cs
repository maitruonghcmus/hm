using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HM.DataModels;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using HM.DataModels.Utils;

namespace HM.API.Controllers
{
    public class RoleController : ApiController
    {
        [HttpGet]
        public Result<IEnumerable<Role>> GetAll(string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<IEnumerable<Role>> { Code = MessageUtils.ERR_PERMISSION };

            return new DBContext<Role>(DbUtils.RoleCollection).GetObjects();
        }

        [HttpGet]
        public Result<Role> GetById(string id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Role> { Code = MessageUtils.ERR_PERMISSION };

            return new DBContext<Role>(DbUtils.RoleCollection).GetObject(id);
        }

        [HttpPut]
        public Result<Role> Create(Role role, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Role> { Code = MessageUtils.ERR_PERMISSION };

#if DEBUG
            if (role == null)
            {
                role = new Role
                {
                    Id = new ObjectId(),
                    Name = "Admin"
                };
            }
#endif

            return new DBContext<Role>(DbUtils.RoleCollection).Insert(role);
        }

        [HttpPost]
        public Result<Role> Update(string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Role> { Code = MessageUtils.ERR_PERMISSION };

            var newRole = new Role();

#if DEBUG
            newRole.Id = new ObjectId("5704e9fa025e9738048354f9");
            newRole.Name = "Quản lý";
#endif

            return new DBContext<Role>(DbUtils.RoleCollection).Replace(newRole);
        }

        [HttpDelete]
        public Result<Role> Delete(string id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return new Result<Role> { Code = MessageUtils.ERR_PERMISSION };

            return new DBContext<Role>(DbUtils.RoleCollection).Delete(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HM.DataModels;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace HM.API.Controllers
{
    public class RoleController : ApiController
    {
        private readonly Result<Role> ErrorPermissionResult = new Result<Role>
        {
            Code = "ERR_PERMISSION",
            Data = null,
            IsSuccess = false
        };

        [HttpGet]
        public Result<IEnumerable<Role>> Get(string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return null;

            return DBContext<Role>.Instance.Read(new Role());
        }

        [HttpGet]
        public Result<Role> Get(ObjectId id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return null;

            return DBContext<Role>.Instance.Read(id, new Role());
        }

        [HttpPost]
        public async Task<Result<Role>> Post(string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return ErrorPermissionResult;

            var oldRole = new Role(); var newRole = new Role();

#if DEBUG

            //if (oldRole == null)
            //{
            oldRole = new Role
            {
                Id = new ObjectId(),
                Name = "Admin"
            };
            //}

            //if (newRole == null)
            //{
            newRole = new Role
            {
                Id = new ObjectId(),
                Name = "Quản lý"
            };
            //}

#endif

            return DBContext<Role>.Instance.Update(oldRole, newRole);
        }

        [HttpPut]
        public Result<Role> Put(Role role, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return ErrorPermissionResult;

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

            return DBContext<Role>.Instance.Create(role);
        }

        [HttpDelete]
        public Result<Role> Delete(ObjectId id, string apiKey)
        {
            if (apiKey != DbUtils.ApiKey)
                return ErrorPermissionResult;

            return DBContext<Role>.Instance.Detele(id, new Role());
        }
    }
}

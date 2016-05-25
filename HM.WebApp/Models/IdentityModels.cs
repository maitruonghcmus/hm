using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace HM.WebApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class HotelUser : IdentityUser
    {
    }

    public class HotelUserStore : IUserStore<HotelUser>, IUserPasswordStore<HotelUser>
    {
        public Task<HotelUser> FindByNameAsync(string username)
        {
            var user = DataContext.Instance.GetUsers()?.Where(a => a.Username.ToLower() == username.ToLower())?.FirstOrDefault();
            var hotelUser = new HotelUser();
            if (user != null)
            {
                hotelUser.Id = user.Id.ToString();
                hotelUser.UserName = user.Username;
                hotelUser.PasswordHash = user.Password;
                hotelUser.Email = user.Username;
            }
            return Task.FromResult<HotelUser>(hotelUser);
        }

        public Task<string> GetPasswordHashAsync(HotelUser user)
        {
            return Task.FromResult<string>(user.PasswordHash.ToString());
        }

        public Task SetPasswordHashAsync(HotelUser user, string passwordHash)
        {
            return Task.FromResult<string>(user.PasswordHash = passwordHash);
        }

        public Task CreateAsync(HotelUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(HotelUser user)
        {
            throw new NotImplementedException();
        }
        public Task<HotelUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(HotelUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(HotelUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        Task<HotelUser> IUserStore<HotelUser, string>.FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
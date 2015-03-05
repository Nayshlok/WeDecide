using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WeDecide.Models.Concrete
{
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
            : base("SecurityConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public enum UserRoles
    {
        User, Admin
    }


    public class IdentityManager
    {
        private ApplicationDbContext db;

        public IdentityManager()
        {
            this.db = ApplicationDbContext.Create();
        }


        public bool RoleExists(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            return rm.RoleExists(name);
        }

        public bool CreateRole(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var idResult = rm.Create(new IdentityRole(name));
            return idResult.Succeeded;

        }

        public bool AddUserToRole(string userId, UserRoles roleName)
        {

            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));
            string role = Enum.GetName(typeof(UserRoles), roleName);
            if (!RoleExists(role))
            {
                CreateRole(role);
            }
            var idResult = um.AddToRole(userId, role);

            return idResult.Succeeded;

        }

        public void ClearUserRoles(string userId)
        {
            var user = db.Users.SingleOrDefault(x => x.Id == userId);
            if (user != null)
            {
                var currentRoles = new List<IdentityUserRole>();
                currentRoles.AddRange(user.Roles);
                foreach (var role in currentRoles)
                {
                    user.Roles.Remove(role);
                }
                db.SaveChanges();
            }
        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using WeDecide.Controllers;
using WeDecide.Models.Concrete;

namespace WeDecide.ViewModels
{
    public class AdminViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<ApplicationUser> IdUsers { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Response> Responses { get; set; }
        public SelectList RoleList { get; set; }
        [Inject]
        public ApplicationUserManager userManager { get; set; }

        public AdminViewModel(ApplicationUserManager manager)
        {
            userManager = manager;
            RoleList = CreateRoleList();
            if(manager != null)
                IdUsers = manager.Users;
        }

        public SelectList CreateRoleList()
        {
            var roles = Enum.GetValues(typeof(UserRoles));
            return new SelectList(roles);
        }
    }
}
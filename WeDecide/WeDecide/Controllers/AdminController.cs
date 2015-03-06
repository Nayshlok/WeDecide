using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.DAL.Abstract;
using WeDecide.Models.Concrete;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WeDecide.ViewModels;

namespace WeDecide.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        private IMembershipDAL _membershipDAL;
        private ApplicationRoleManager _roleManager;


        public IMembershipDAL MembershipDAL
        {
            get { return _membershipDAL; }
            set { _membershipDAL = value; }
        }

        public AdminController(IMembershipDAL dal)
        {
            MembershipDAL = dal;
        }

        public AdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IMembershipDAL membership)
        {
            UserManager = userManager;
            MembershipDAL = membership;
            RoleManager = roleManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return this._roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set { this._roleManager = value; }
        }
        // GET: Admin
        [HttpGet]
        public ActionResult UserAdmin()
        {
            AdminViewModel model = new AdminViewModel(UserManager) { Users = MembershipDAL.GetUsers() };
            return View("~/Views/Account/AdminUsers.cshtml", model);
        }

        public ActionResult RemoveUser()
        {
            string id = Request.Form["userId"];
            //Remove User
            return new RedirectResult("UserAdmin");
        }

        public ActionResult ChangeRole()
        {
            string id = Request.Form["userId"];
            string roleName = Request.Form["Role"];
            RemoveRoles(id);
            if (!RoleManager.RoleExists(roleName))
            {
                RoleManager.Create(new IdentityRole(roleName));
            }
            UserManager.AddToRole(id, roleName);
            //MembershipDAL.SetRole(id, role);
            return new RedirectResult("UserAdmin");
        }

        public void RemoveRoles(string userId)
        {
            var user = UserManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                UserManager.RemoveFromRole(userId, RoleManager.Roles.Single(x => x.Id == role.RoleId).Name);
            }
        }
    }
}
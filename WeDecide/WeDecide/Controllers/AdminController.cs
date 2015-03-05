using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.DAL.Abstract;
using WeDecide.Models.Concrete;
using Microsoft.AspNet.Identity.Owin;
using WeDecide.ViewModels;

namespace WeDecide.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private IMembershipDAL Mdal;
        private ApplicationDbContext iddb;

        public AdminController(IMembershipDAL dal, ApplicationDbContext iddb)
        {
            Mdal = dal;
            this.iddb = iddb;
        }

        // GET: Admin
        [HttpGet]
        public ActionResult UserAdmin()
        {
            AdminViewModel model = new AdminViewModel(HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>()) { Users = Mdal.GetUsers(), IdUsers = iddb.Users };
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
            UserRoles role;
            Enum.TryParse<UserRoles>(Request.Form["Role"], out role);
            Mdal.SetRole(id, role);
            return new RedirectResult("UserAdmin");
        }

    }
}
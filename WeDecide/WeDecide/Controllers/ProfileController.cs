using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.ViewModels;

namespace WeDecide.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ProfileViewModel userProfile = new ProfileViewModel()
            {
                UserName = "Jim Bobby",
                UserFriends = null, //Not sure how to get this yet
                UserQuestions = null //Also not sure how to get this
            };

            return View("ProfileView", userProfile);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.DAL.Abstract;
using WeDecide.ViewModels;

namespace WeDecide.Controllers
{
    public class ProfileController : Controller
    {
        private IMembershipDAL memberDal;

        [HttpGet]
        public ActionResult Index()
        {
            //Need to be able to get the current logged in user
            //ProfileViewModel userProfile = new ProfileViewModel()
            //{
            //    UserName = memberDal.GetUser(1),
            //    UserFriends = memberDal.GetFriends(1), //Not sure how to get this yet
            //    UserQuestions = null //Also not sure how to get this
            //};

            ProfileViewModel userProfile = new ProfileViewModel()
            {
                UserName = "Jim Bobby",
                UserFriends = null, //Not sure how to get this yet
                UserQuestions = null //Also not sure how to get this
            };

            return View("ProfileView", userProfile);
        }

        public ActionResult UpdateProfile(ProfileViewModel pvm)
        {
            if(ModelState.IsValid)
            {
                //TODO: Update the profile of the user.
            }

            return View("ProfileView", pvm);
        }
    }
}
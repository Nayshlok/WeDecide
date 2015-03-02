using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.DAL.Abstract;
using Microsoft.AspNet.Identity;
using WeDecide.ViewModels;

namespace WeDecide.Controllers
{
    public class ProfileController : Controller
    {
        private IMembershipDAL memberDal;
        private IQuestionDAL questionDal;

        [HttpGet]
        public ActionResult Index()
        {
            //Models.Concrete.User currentUser = memberDal.GetUser(User.Identity.GetUserId());

            //ProfileViewModel userprofile = new ProfileViewModel()
            //{
            //    UserName = currentUser.Name,
            //    UserFriends = memberDal.GetFriends(currentUser.Id),
            //    UserQuestions = questionDal.GetAll(x => x.UserId == currentUser.Id)
            //};

            ProfileViewModel userProfile = new ProfileViewModel()
            {
                UserName = "Jim Bobby",
                UserFriends = null, //Not sure how to get this yet
                UserQuestions = null //Also not sure how to get this
            };

            //, userProfile
            return View("ProfileView");
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
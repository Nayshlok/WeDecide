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
    [Authorize]
    public class ProfileController : Controller
    {
        private IMembershipDAL memberDal;
        private IQuestionDAL questionDal;

        public ProfileController(IMembershipDAL memberDalParam, IQuestionDAL questionDalParam)
        {
            memberDal = memberDalParam;
            questionDal = questionDalParam;
        }

        [HttpGet]
        public ActionResult Index()
        {
            Models.Concrete.User currentUser = memberDal.GetUser(User.Identity.GetUserId());

            ProfileViewModel userProfile = new ProfileViewModel()
            {
                UserName = currentUser.Name,
                UserFriends = memberDal.GetFriends(currentUser.Id),
                UserQuestions = questionDal.GetAll(x => x.UserId == currentUser.Id),
                ImagePath = currentUser.ImagePath
            };

            return View("ProfileView", userProfile);
        }

        public ActionResult UpdateProfile(ProfileViewModel pvm)
        {
            Models.Concrete.User currentUser = memberDal.GetUser(User.Identity.GetUserId());
            if(ModelState.IsValid)
            {
                //Change the user's name
                currentUser.Name = pvm.UserName;  
            }

            ProfileViewModel userProfile = new ProfileViewModel()
            {
                UserName = currentUser.Name,
                UserFriends = memberDal.GetFriends(currentUser.Id),
                UserQuestions = questionDal.GetAll(x => x.UserId == currentUser.Id),
                ImagePath = currentUser.ImagePath
            };

            return View("ProfileView", userProfile);
        }
    }
}
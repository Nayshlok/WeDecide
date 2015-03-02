using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.DAL.Abstract;
using WeDecide.DAL.Concrete;
using WeDecide.Models.Concrete;
using WeDecide.ViewModels;
using Microsoft.AspNet.Identity;

namespace WeDecide.Controllers
{
    public class FriendsController : Controller
    {
        private IMembershipDAL memberDal;

        //View model for all pages
        private static FriendsViewModel fvm;

        public FriendsController()
        {
            memberDal = new CustomMembershipDAL();

            //---TESTING ONLY--//
            //Filled with temporary values to test page functionality
            fvm = new FriendsViewModel()
            {
                UserProfile = new ProfileViewModel()
                {
                    UserName = "Jim Bobby",
                    UserFriends = new List<User>()
                    {
                        new User{Name = "David Wright", Id = "01"},
                        new User{Name = "John Blake", Id = "02"},
                        new User{Name = "William Blake", Id = "03"}
                    }
                },
                PotentialFriends = null
            };
        }

        // GET: Friends
        public ActionResult Index()
        {
            return View("FriendsView", fvm);
        }

        [HttpGet]
        public ActionResult SearchFriends(string friendsQuery)
        {
            //Find users relevant to the users search
            List<User> potentialFriends = memberDal.Search(friendsQuery);

            //Get the current user and update the view model for the friends page.
            //Should be a better way to do this.
            //User currentUser = memberDal.GetUser(User.Identity.GetUserId());
            fvm.PotentialFriends = new List<User>()
            {
                new User{Name = "John Jake"}
            };

            //Working on Async Json returns
            //return Json(fvm, JsonRequestBehavior.AllowGet);

            return View("FriendsView", fvm);
        }

        public ActionResult AddFriend(string Id)
        {
            //User currentUser = memberDal.GetUser(User.Identity.GetUserId());
            //memberDal.GetFriends(currentUser.Id).Add(memberDal.GetUser(Id));

            //---TESTING ONLY--//
            //fvm.UserProfile.UserFriends.ToList().Add(fvm.PotentialFriends.Where(i => i.Id == Id).Single());
            //fvm.PotentialFriends.ToList().Remove(fvm.PotentialFriends.Where(i => i.Id == Id).Single());

            return View("FriendsView", fvm);
        }
    }
}
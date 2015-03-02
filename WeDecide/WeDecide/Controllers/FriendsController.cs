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

        public FriendsController()
        {
            memberDal = new CustomMembershipDAL();
        }

        // GET: Friends
        public ActionResult Index()
        {
            return View("FriendsView");
        }

        [HttpGet]
        public ActionResult SearchFriends(string friendsQuery)
        {
            //Find users relevant to the users search
            List<User> potentialFriends = memberDal.Search(friendsQuery);

            //Get the current user and create a new view model for the friends page.
            //Should be a better way to do this.
            User currentUser = memberDal.GetUser(User.Identity.GetUserId()); 
            FriendsViewModel fvm = new FriendsViewModel()
            {
                //UserName = currentUser.Name,
                //UserFriends = memberDal.GetFriends(currentUser.Id),
                PotentialFriends = potentialFriends
            };

            return View("FriendsView", fvm);
        }

        public ActionResult AddFriend()
        {
            return View();
        }
    }
}
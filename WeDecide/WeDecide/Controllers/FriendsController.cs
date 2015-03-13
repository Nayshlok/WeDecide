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
    [Authorize]
    public class FriendsController : Controller
    {
        private IMembershipDAL memberDal;

        //View model for all pages
        private static FriendsViewModel fvm;

        public FriendsController(IMembershipDAL dal)
        {
            memberDal = dal;
        }

        // GET: Friends
        public ActionResult Index()
        {
            User currentUser = memberDal.GetUser(User.Identity.GetUserId());
            fvm = new FriendsViewModel()
            {
                UserProfile = new ProfileViewModel()
                {
                    UserName = currentUser.Name,
                    UserFriends = memberDal.GetFriends(currentUser.Id),
                    ImagePath = currentUser.ImagePath
                },
                PotentialFriends = null
            };

            return View("FriendsView", fvm);
        }

        [HttpGet]
        public ActionResult SearchFriends(string friendsQuery)
        {
            User currentUser = memberDal.GetUser(User.Identity.GetUserId());
            //Find users relevant to the users search
            List<User> potentialFriends = memberDal.Search(currentUser, friendsQuery);

            //Get the current user and update the view model for the friends page.
            //Should be a better way to do this.
            fvm.PotentialFriends = potentialFriends;

            return View("FriendsView", fvm);
        }

        public void AddFriend(int nId, string UserID)
        {
            User currentUser = memberDal.GetUser(User.Identity.GetUserId());
            memberDal.AddFriend(currentUser.Id, UserID);
            memberDal.MarkNotPending(nId);

            //return RedirectToAction("Index", "Profile");
        }

        public void RejectFriend(int nId)
        {
            memberDal.MarkNotPending(nId);

            //return RedirectToAction("Index", "Profile");
        }
    }
}
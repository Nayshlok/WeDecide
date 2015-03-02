using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.DAL.Abstract;
using WeDecide.Models.Concrete;

namespace WeDecide.Controllers
{
    public class FriendsController : Controller
    {
        private IMembershipDAL memberDal;

        // GET: Friends
        public ActionResult Index()
        {
            return View("FriendsView");
        }

        [HttpGet]
        public ActionResult SearchFriends(string friendsQuery)
        {
            //List<User> possibleFriends = memberDal.Search(friendsQuery);
            //return View("FriendsView", possibleFriends);
            return View("FriendsView");
        }
    }
}
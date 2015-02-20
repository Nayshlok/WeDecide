using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.ViewModels;

namespace WeDecide.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            //If the user is logged in, take him to the home page with feed.  
            return View();
        }

        public ActionResult LandingPage()
        {
            //Take the user to the landing page if not logged in our if not yet registered.
            return View();
        }
    }
}
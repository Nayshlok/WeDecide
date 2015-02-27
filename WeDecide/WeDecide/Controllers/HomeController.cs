using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.Models.Concrete;
using WeDecide.ViewModels;
using WeDecide.DAL.Abstract;
using WeDecide.DAL.Concrete;

namespace WeDecide.Controllers
{
    public class HomeController : Controller
    {
        private QuestionDbContext _Context;
        public HomeController(QuestionDbContext context)
        {
            _Context = context;
        }

        //private IQuestionDAL _Context = new TestDal();

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

        public ActionResult Testpage()
        {
            // Use a viewmodel that holds Question, its responses, and those user responses
            TestViewModel testVm = new TestViewModel();

            testVm.TheQuestion = _Context.Questions.Find(1);
            testVm.Responses = _Context.Responses.ToList();
            testVm.UserResponses = _Context.UserResponses.AsEnumerable();

            return View(testVm);
        }
    }

    public class TestViewModel
    {
        public Question TheQuestion { get; set; }
        public IEnumerable<Response> Responses { get; set; }
        public IEnumerable<UserResponse> UserResponses { get; set; }
    }
}
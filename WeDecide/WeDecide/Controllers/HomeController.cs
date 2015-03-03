using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.Models.Concrete;
using WeDecide.ViewModels;
using WeDecide.DAL.Abstract;
using WeDecide.DAL.Concrete;
using Microsoft.AspNet.Identity;

namespace WeDecide.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private IMembershipDAL Mdal = new CustomMembershipDAL();
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
            User currentUser = Mdal.GetUser(User.Identity.GetUserId());

            Question quest = new Question { UserId = currentUser.Id, QuestionScope = Question.Scope.Friends, IsActive = true, Text = "Do I work?", EndDate = new DateTime(2015, 3, 1) };
            Response[] responses = new Response[]{
                new Response{ QuestionId = quest.Id, Text="Yes", Users = new List<User>{currentUser} },
                new Response{ QuestionId = quest.Id, Text="No", Users = new List<User>() },

            };

            testVm.TheQuestion = _Context.Questions.Find(1);
            testVm.Responses = _Context.Responses.ToList();

            return View(testVm);
        }
    }

    public class TestViewModel
    {
        public Question TheQuestion { get; set; }
        public IEnumerable<Response> Responses { get; set; }
    }
}
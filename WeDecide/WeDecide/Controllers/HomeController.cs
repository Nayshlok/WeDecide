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

            //Question question = _Context.Questions.First();
            //WeDecide.Models.Concrete.Response response1 = new WeDecide.Models.Concrete.Response { Question = question, Text="yes"};
            //WeDecide.Models.Concrete.Response response2 = new WeDecide.Models.Concrete.Response { Question = question, Text = "no" };
            //_Context.Responses.Add(response1);
            //_Context.Responses.Add(response2);
            //_Context.SaveChanges();

            testVm.TheQuestion = _Context.Questions.First();
            testVm.Responses = testVm.TheQuestion.Responses;

            //if (User != null)
            //{
            //    testVm.TheQuestion.Responses.First().Users.Add(Mdal.GetUser(User.Identity.GetUserId()));
            //}

            return View(testVm);
        }
    }

    public class TestViewModel
    {
        public Question TheQuestion { get; set; }
        public IEnumerable<Response> Responses { get; set; }
    }
}
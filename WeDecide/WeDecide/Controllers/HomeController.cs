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

        private QuestionDbContext _Context;
        private IMembershipDAL Mdal;

        public HomeController(QuestionDbContext context)
        {
            _Context = context;
            Mdal = new CustomMembershipDAL(context);
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

            //User currentUser = Mdal.GetUser(User.Identity.GetUserId());

            User user1 = Mdal.GetUserByName("test1@test.com");
            User user2 = Mdal.GetUserByName("test2@test.com");
            User user3 = Mdal.GetUserByName("test3@test.com");

            //Question question1 = new Question { User = user1, Text = "Question1", QuestionScope = Question.Scope.Friends, EndDate = new DateTime(2015, 3, 15) };
            //Response response1 = new Response { Question = question1, Text = "Response1Q1" };
            //Response response2 = new Response { Question = question1, Text = "Response2Q1" };
            //Response response3 = new Response { Question = question1, Text = "Response3Q1" };
            //Response response4 = new Response { Question = question1, Text = "Response4Q1" };

            //Question question2 = new Question { User = user1, Text = "question2", QuestionScope = Question.Scope.Friends, EndDate = new DateTime(2015, 3, 1) };
            //Response q2response1 = new Response { Question = question2, Text = "Response1Q2" };
            //Response q2response2 = new Response { Question = question2, Text = "Response2Q2" };
            //Response q2response3 = new Response { Question = question2, Text = "Response3Q2" };
            //Response q2response4 = new Response { Question = question2, Text = "Response4Q2" };

            //Question question3 = new Question { User = user1, Text = "question3", QuestionScope = Question.Scope.Friends, EndDate = new DateTime(2015, 3, 10), FreeResponseEnabled = true };
            //Response q3response1 = new Response { Question = question3, Text = "Response1Q3" };
            //Response q3response2 = new Response { Question = question3, Text = "Response2Q3" };
            //Response q3response3 = new Response { Question = question3, Text = "Response3Q3" };
            //Response q3response4 = new Response { Question = question3, Text = "Response4Q3" };

            //question1.Responses.Add(response1);
            //question1.Responses.Add(response2);
            //question1.Responses.Add(response3);
            //question1.Responses.Add(response4);
            //_Context.Questions.Add(question1);
            //question2.Responses.Add(q2response1);
            //question2.Responses.Add(q2response2);
            //question2.Responses.Add(q2response3);
            //question2.Responses.Add(q2response4);
            //question3.Responses.Add(q3response1);
            //question3.Responses.Add(q3response2);
            //question3.Responses.Add(q3response3);
            //question3.Responses.Add(q3response4);
            //_Context.Questions.Add(question2);
            //_Context.Questions.Add(question3);
            Question holder = _Context.Questions.FirstOrDefault(x => x.Text.Equals("question2"));

            holder.Responses.First().Users.Add(user1);
            holder.Responses.First().Users.Add(user2);

            _Context.SaveChanges();

            //User newUser = new User { Id = User.Identity.GetUserId(), Name = "david" };
            //_Context.Users.Add(newUser);
            //_Context.SaveChanges();

            //User current = Mdal.GetUser(User.Identity.GetUserId());
            //Question toEnter = new Question { User = current, Text = "Should I?", QuestionScope = Question.Scope.Friends };
            //_Context.Questions.Add(toEnter);
            //_Context.SaveChanges();

            //Question question = _Context.Questions.First();
            //WeDecide.Models.Concrete.Response response1 = new WeDecide.Models.Concrete.Response { Question = question, Text = "yes" };
            //WeDecide.Models.Concrete.Response response2 = new WeDecide.Models.Concrete.Response { Question = question, Text = "no" };
            //_Context.Responses.Add(response1);
            //_Context.Responses.Add(response2);
            //_Context.SaveChanges();



            testVm.TheQuestion = _Context.Questions.First();
            //currentUser.Responses.Add(testVm.TheQuestion.Responses.First());
            //testVm.TheQuestion.Responses.First().Users.Add(currentUser);
            //_Context.SaveChanges();

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
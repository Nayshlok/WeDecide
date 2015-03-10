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
        private QuestionDbContext _Context;
        private IMembershipDAL Mdal;

        public HomeController(IMembershipDAL dal)
        {
            Mdal = dal;
        }

        public HomeController(QuestionDbContext context, IMembershipDAL Mdal)
        {
            _Context = context;
            this.Mdal = Mdal;
        }

        //private IQuestionDAL _Context = new TestDal();

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            User currentUser = Mdal.GetUser(User.Identity.GetUserId());
            IndexViewModel ivm = new IndexViewModel()
            {
                User = new ProfileViewModel()
                {
                    UserName = currentUser.Name,
                    UserFriends = Mdal.GetFriends(currentUser.Id),
                    ImagePath = currentUser.ImagePath
                }
            };
  
            return View(ivm);
        }

        public ActionResult Testpage()
        {
            // Use a viewmodel that holds Question, its responses, and those user responses
            TestViewModel testVm = new TestViewModel();

            //User currentUser = Mdal.GetUser(User.Identity.GetUserId());

            //User user1 = Mdal.GetUserByName("Test1");
            //User user2 = Mdal.GetUserByName("Test2");
            //User user3 = Mdal.GetUserByName("Test3");
            //User user4 = Mdal.GetUserByName("Test4");

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
            //_Context.SaveChanges();
            //Question holder = _Context.Questions.FirstOrDefault(x => x.Text.Equals("question2"));

            //holder.Responses.First().Users.Add(user1);
            //holder.Responses.First().Users.Add(user2);

            //_Context.SaveChanges();

            //user1.UserFriends.Add(user2);
            //user1.UserFriends.Add(user4);
            //user3.UserFriends.Add(user2);
            //user2.UserFriends.Add(user1);
            //user2.UserFriends.Add(user4);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WeDecide.DAL.Abstract;
using WeDecide.DAL.Concrete;
using WeDecide.Models.Concrete;
using WeDecide.ViewModels;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using WeDecide.Hubs;

namespace WeDecide.Controllers
{
    [System.Web.Mvc.Authorize]
    public class QuestionController : Controller
    {

        private readonly static IHubConnectionContext<dynamic> FriendContext = GlobalHost.ConnectionManager.GetHubContext<FriendQuestionHub>().Clients;
        private readonly static IHubConnectionContext<dynamic> GlobalContext = GlobalHost.ConnectionManager.GetHubContext<GlobalQuestionHub>().Clients;

        //Until we have the DAL injection done
        private static IQuestionDAL Qdal;
        private static IMembershipDAL Mdal;//= new CustomMembershipDAL();

        public QuestionController(IMembershipDAL NewMdal, IQuestionDAL qdal) 
        {
            Mdal = NewMdal;
            Qdal = qdal;
        }

        [HttpGet]
        public ViewResult CreateQuestion()
        {
            //Get the Question creation view.
            return View();
        }

        private Func<Question, QuestionDTO> questionToDTO =
            q => new QuestionDTO()
            {
                Id = q.Id,
                QuestionText = q.Text,
                IsActive = q.IsActive,
                EndTime = q.EndDate,
                UserId = q.UserId,
                Scope = q.QuestionScope.ToString(),
                FreeResponseEnabled = q.FreeResponseEnabled,
                Responses = q.Responses.Where(r => !r.IsDeleted).Select(r =>
                {
                    return new { Text = r.Text, Id = r.Id, VoteCount = r.Users.Count };
                })
            };

        [HttpPost]
        public ActionResult CreateQuestion(MakeQuestionViewModel q)
        {
            if (ModelState.IsValid)
            {
                //Construct the Question
                Question NewQuestion = q.GetQuestion();
                //Add current user to question
                //NewQuestion.User = Mdal.GetUser(User.Identity.GetUserId());
                NewQuestion.UserId = (User.Identity.GetUserId());
                //Save the Question
                Qdal.Create(NewQuestion);
                //Don't know what to return yet, so returning response page
                //RespondToQuestionViewModel model = new RespondToQuestionViewModel(NewQuestion);
                //return RedirectToAction("QuestionResponse", "QuestionResponse", new { id = NewQuestion.Id });

                if (NewQuestion.QuestionScope == Question.Scope.Friends)
                {
                    string[] userConnections = FriendQuestionHub.userConnections.Where(x => x.Value == User.Identity.GetUserId()).Select(x => x.Key).ToArray();
                    if (userConnections.Length == 0)
                    {
                        FriendContext.All.addQuestion(questionToDTO.Invoke(NewQuestion));
                    }
                    else
                    {
                        FriendContext.AllExcept(userConnections).addQuestion(questionToDTO.Invoke(NewQuestion));
                    }
                }
                else
                {
                    string[] userConnections = GlobalQuestionHub.userConnections.Where(x => x.Value == User.Identity.GetUserId()).Select(x => x.Key).ToArray();
                    if (userConnections.Length == 0)
                    {
                        GlobalContext.All.addQuestion(questionToDTO.Invoke(NewQuestion));
                    }
                    else
                    {
                        GlobalContext.AllExcept(userConnections).addQuestion(questionToDTO.Invoke(NewQuestion));
                    }
                }

                return new EmptyResult();
            }
            return PartialView("~/Views/Shared/_MakeQuestionPartial.cshtml");
            //return Json(ModelState.ToDictionary(x => x.Key, x => x.Value.Errors.ToList().ConvertAll(y => y.ErrorMessage)));
        }
        
        [HttpGet]
        public ViewResult EditQuestion(int Id)
        {
            //Retrieve the question and allow user to change properties.
            EditQuestionViewModel vm = new EditQuestionViewModel(Qdal.Get(Id));
            return View(vm);
        }

        [HttpPost]
        public ActionResult EditQuestion(EditQuestionViewModel q)
        {
            if(ModelState.IsValid)
            {
                Question UpdatedQuestion = q.GetQuestion();
                //If current User and Question's User are the same
                if (IsUsersQuestion(q.QuestionId))
                {
                    //Update the question
                    UpdatedQuestion.User = Mdal.GetUser(User.Identity.GetUserId());
                    Qdal.Update(q.QuestionId, UpdatedQuestion);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult RemoveQuestion(int QuestionId)
        {
            //Retrieve the question and remove it from the database.
            if (IsUsersQuestion(QuestionId))
            {
                Qdal.Delete(QuestionId);
            }
            //Not sure what to return here
            return View();
        }
        
        private bool IsUsersQuestion(int QuestionId)
        {
            Question SavedQuestion = Qdal.Get(QuestionId);
            return User.Identity.GetUserId().Equals(SavedQuestion.UserId);
        }
    }
}
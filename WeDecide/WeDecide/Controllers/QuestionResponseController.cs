using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
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
    public class QuestionResponseController : Controller
    {

        private readonly static IHubConnectionContext<dynamic> FriendContext = GlobalHost.ConnectionManager.GetHubContext<FriendQuestionHub>().Clients;
        private readonly static IHubConnectionContext<dynamic> GlobalContext = GlobalHost.ConnectionManager.GetHubContext<GlobalQuestionHub>().Clients;

        //Until we have the DAL injection done
        private static IQuestionDAL Qdal;
        private static IMembershipDAL Mdal;// = new CustomMembershipDAL();

        public QuestionResponseController(IQuestionDAL NewQdal, IMembershipDAL NewMdal)
        {
            Mdal = NewMdal;
            Qdal = NewQdal;
        }

        // GET: QuestionResponse
        [HttpGet]
        public ActionResult QuestionResponse(int QuestionId)
        {
            Question question = Qdal.Get(QuestionId);

            RespondToQuestionViewModel vm = null;
            //Check if current user can respond to the Question
            if (UserCanRespondTo(question))
            {
                //Return the RespondToQuestionViewModel
                vm = new RespondToQuestionViewModel(question);
            }

            return View("~/Views/QuestionResponse/Response.cshtml", vm);
        }


        private bool UserCanRespondTo(Question question)
        {
            bool UserInScope = UserInQuestionScope(question);
            if (!UserInScope)
            {
                ViewBag.ErrorMessage = "You are not allowed to see this question";
            }
            bool IsSameUser = User.Identity.GetUserId().Equals(question.UserId);
            if (IsSameUser)
            {
                ViewBag.ErrorMessage = "You cannot respond to your own question";
            }

            bool IsActive = question.IsActive;
            if (!IsActive)
            {
                ViewBag.ErrorMessage = "This question has timed out";
            }
            return UserInScope && !IsSameUser && question.IsActive;
        }

        private bool UserInQuestionScope(Question question)
        {
            User QuestionAsker = Mdal.GetUser(question.UserId);
            bool InScope = false;
            var friends = Mdal.GetFriends(question.UserId);

            switch (question.QuestionScope)
            {
                case Question.Scope.Global: InScope = true; break;

                case Question.Scope.Friends: InScope = Mdal.GetFriends(question.UserId).Where(x => x.Id.Equals(User.Identity.GetUserId())).Count() > 0; break;

                //Don't know how to do local yet
                //case Question.Scope.Local: InScope;
                //Don't know how to do regional yet
                //case Question.Scope.Regional:
            }
            return InScope;
        }

        [HttpPost]
        public ActionResult QuestionResponsePost(string ChosenResponse, int QuestionId)
        {
            //Get the Question and Response chosen
            Question AffectedQuestion = Qdal.Get(QuestionId);
            if (UserCanRespondTo(AffectedQuestion))
            {
                //Add User Response to AffectedQuestion
                Response Resp = MakeUserResponse(AffectedQuestion, ChosenResponse);

                //Qdal.Update(QuestionId, AffectedQuestion);
                Response NewResp = AffectedQuestion.Responses.First(x => x.Text.Equals(Resp.Text));

                if (AffectedQuestion.QuestionScope == Question.Scope.Friends)
                {
                    FriendContext.All.RefreshResponse(NewResp.Id, Qdal.ResponseCount(NewResp.Id));
                }
                else
                {
                    GlobalContext.All.RefreshResponse(NewResp.Id, Qdal.ResponseCount(NewResp.Id));
                }
            }
            //Clients.User(UserId).receivedResponse(question.Id, question.Responses);
            //Would like to have this not actually return, as the Partial View will always be a part of something else
            //return RedirectToAction("QuestionResponse", new { id = QuestionId });
            return new EmptyResult();
        }

        private Response MakeUserResponse(Question question, string responseText)
        {
            User currentUser = Mdal.GetUser(User.Identity.GetUserId());
            Response response = null;

            //If Response is new and FreeResponse is enabled
            if (CanAddFreeResponse(question, responseText))
            {
                response = new Response() { Question = question, Text = responseText, Users = new List<User>()};
                //Add Response to Question
                Qdal.AddResponse(question.Id, response);
                question.Responses.Add(response);
                //If Has Responded Before
                //if (UserHasRespondedBefore(question))
                //{
                //    int oldResponseId = question.Responses.First(x => x.Users.Contains(currentUser, new UserComparer())).Id;
                //    //Swap the votes between the responses
                //    Qdal.SwitchUserResponse(oldResponseId, response.Id, currentUser.Id);
                //}
                ////Else
                //else
                //{
                //    //Add User to the Response
                //    Qdal.AddUserToResponse(response.Id, currentUser.Id);
                //}
            }
            //If User has responded to question before
            if(UserHasRespondedBefore(question))
            {
                Response oldResponse = question.Responses.First(x => x.Users.Contains(currentUser, new UserComparer()));
                response = question.Responses.First(x => x.Text.Equals(responseText));
                Qdal.SwitchUserResponse(oldResponse.Id, response.Id, currentUser.Id);
                
                if (question.QuestionScope == Question.Scope.Friends)
                {
                    FriendContext.All.RefreshResponse(oldResponse.Id, Qdal.ResponseCount(oldResponse.Id));
                }
                else
                {
                    GlobalContext.All.RefreshResponse(oldResponse.Id, Qdal.ResponseCount(oldResponse.Id));
                }

            }
            //Else the User has responded to this question for the first time
            else {
                //Make new UserResponse                
                //UserResponse NewUR = new UserResponse() { Question = question, QuestionId = question.Id, Response = response, ResponseId = response.Id };
                //AddUserResponse(question, response, NewUR);
                //response = question.Responses.First(x => x.Text.Equals(responseText));
                //response.Users.Add(currentUser);
                response = question.Responses.First(x => x.Text.Equals(responseText));
                Qdal.AddUserToResponse(response.Id, currentUser.Id);
            }
            return response;
        }

        //private UserResponse GetUserResponse(Response response)
        //{
        //    //return response.UserResponses.First(x => x.Id == UsersId)
        //    return null;
        //}

        //private void AddUserResponse(Question question, Response response, UserResponse userResponse)
        //{
        //    //Add new UserResponse to Question
        //    question.UserResponses.Add(userResponse);
        //    //Add new UserResponse to Respone
        //    response.UserResponses.Add(userResponse);
        //}

        //private void UpdateUserResponse(Question question, Response response, UserResponse userResponse)
        //{
        //    //Remove userResponse from its current response
        //    userResponse.Response.UserResponses.Remove(userResponse);
        //    //Change userResponse's response to the passed in response
        //    userResponse.Response = response;
        //    //Add userResponse to passed int response
        //    response.UserResponses.Add(userResponse);
        //}

        private bool CanAddFreeResponse(Question question, string response)
        {
            return question.FreeResponseEnabled && question.Responses.Count(x => x.Text.Equals(response)) < 1 && !string.IsNullOrWhiteSpace(response);
        }

        private bool UserHasRespondedBefore(Question question)
        {
            bool responded = false;
            User currentUser = Mdal.GetUser(User.Identity.GetUserId());
            foreach (Response r in question.Responses)
            {
                if (r.Users.Contains(currentUser, new UserComparer()))
                {
                    responded = true;
                    break;
                }
            }

            return responded;
        }
    }
}
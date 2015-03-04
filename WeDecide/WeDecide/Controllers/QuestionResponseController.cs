﻿using System;
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

        private readonly static IHubConnectionContext<dynamic> HubContext = GlobalHost.ConnectionManager.GetHubContext<questionHub>().Clients;
        //Until we have the DAL injection done
        private static IQuestionDAL Qdal = new MemoryQuestionDAL();
        private static IMembershipDAL Mdal;// = new CustomMembershipDAL();

        public QuestionResponseController(IMembershipDAL NewMdal)
        {
            Mdal = NewMdal;
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
            return UserInQuestionScope(question) && User.Identity.GetUserId().Equals(question.UserId);
        }

        private bool UserInQuestionScope(Question question)
        {
            User QuestionAsker = Mdal.GetUser(question.UserId);
            bool InScope = false;
            switch (question.QuestionScope)
            {
                case Question.Scope.Global: InScope = true; break;

                //case Question.Scope.Friends: InScope = Mdal.GetFriends(question.UserId).Where(x => x.Id.Equals(User.Identity.GetUserId())).Count() > 0; break;

                //Don't know how to do local yet
                //case Question.Scope.Local: InScope;
                //Don't know how to do regional yet
                //case Question.Scope.Regional:
            }
            return InScope;
        }

        [HttpPost]
        public ActionResult QuestionResponse(string ChosenResponse, int QuestionId)
        {
            //Get the Question and Response chosen
            Question AffectedQuestion = Qdal.Get(QuestionId);

            //Add User Response to AffectedQuestion
            Response Resp = MakeUserResponse(AffectedQuestion, ChosenResponse);

            Qdal.Update(QuestionId, AffectedQuestion);
            HubContext.User(User.Identity.GetUserId()).receivedResponse(QuestionId, Resp);
            //Clients.User(UserId).receivedResponse(question.Id, question.Responses);
            //Would like to have this not actually return, as the Partial View will always be a part of something else
            //return RedirectToAction("QuestionResponse", new { id = QuestionId });
            return new EmptyResult();
        }

        private Response MakeUserResponse(Question question, String responseText)
        {
            User currentUser = Mdal.GetUser(User.Identity.GetUserId());
            Response response = null;

            //If Response is new and FreeResponse is enabled
            if (CanAddFreeResponse(question, responseText))
            {
                response = new Response() { Question = question, Text = responseText, Users = new List<User>()};
                //Add Response to question
                question.Responses.Add(response);
                response.Users.Add(currentUser);
            }
            //If User has responded to question before
            if(UserHasRespondedBefore(question))
            {
                Response oldResponse = question.Responses.First(x => x.Users.Contains(currentUser));
                oldResponse.Users.Remove(currentUser);
                response = question.Responses.First(x => x.Text.Equals(responseText));
                response.Users.Add(currentUser);
            }
            //Else
            else {
                //Make new UserResponse                
                //UserResponse NewUR = new UserResponse() { Question = question, QuestionId = question.Id, Response = response, ResponseId = response.Id };
                //AddUserResponse(question, response, NewUR);
                response = question.Responses.First(x => x.Text.Equals(responseText));
                response.Users.Add(currentUser);
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

        private bool CanAddFreeResponse(Question question, String response)
        {
            return question.FreeResponseEnabled && question.Responses.Count(x => x.Text.Equals(response)) < 1;
        }

        private bool UserHasRespondedBefore(Question question)
        {
            bool responded = false;

            foreach (Response r in question.Responses)
            {
                if (r.Users.Contains(Mdal.GetUser(User.Identity.GetUserId())))
                {
                    responded = true;
                    break;
                }
            }

            return responded;
        }
    }
}
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

namespace WeDecide.Controllers
{
    public class QuestionResponseController : Controller
    {

        //Until we have the DAL injection done
        private static IQuestionDAL Qdal = new MemoryQuestionDAL();

        // GET: QuestionResponse
        [HttpGet]
        public ActionResult QuestionResponse(int id)
        {
            Question question = Qdal.Get(id);

            RespondToQuestionViewModel vm = null;
            //Check if current user can respond to the Question
            if (UserCanRespondTo(question))
            {
                //Return the RespondToQuestionViewModel
                vm = new RespondToQuestionViewModel(question);
            }
            return View("~/Views/QuestionResponse/Response.cshtml", vm);
        }

        //TODO: Change when we can get the users
        private bool UserCanRespondTo(Question question)
        {
            return true;
        }

        [HttpPost]
        public void QuestionResponse(string ChosenResponse, int QuestionId)
        {
            //Make New UserResponse with given string and question id
            Question AffectedQuestion = Qdal.Get(QuestionId);
            Response Resp = AffectedQuestion.Responses.First(x => x.Text.Equals(ChosenResponse));

            //Add User Response to AffectedQuestion
            MakeUserResponse(AffectedQuestion, Resp);

            Qdal.Update(QuestionId, AffectedQuestion);

            //Would like to have this not actually return, as the Partial View will always be a part of something else
            //return RedirectToAction("QuestionResponse", new { id = QuestionId });
        }

        private void MakeUserResponse(Question question, Response response)
        {
            //If Response is new and FreeResponse is enabled
            if (CanAddFreeResponse(question, response))
            {
                //Add Response to question
                question.Responses.Add(response);
            }
            //If User has responded to question before
            if(UserHasRespondedBefore(question, response))
            {
                //Adjust existing UserResponse to point to new Response
            }
            //Else
            else {
                //Make new UserResponse                
                UserResponse NewUR = new UserResponse() { Question = question, QuestionId = question.Id, Response = response, ResponseId = response.Id };
                AddUserResponse(question, response, NewUR);
            }
        }

        private UserResponse GetUserResponse(Response response)
        {
            //return response.UserResponses.First(x => x.Id == UsersId)
            return null;
        }

        private void AddUserResponse(Question question, Response response, UserResponse userResponse)
        {
            //Add new UserResponse to Question
            question.UserResponses.Add(userResponse);
            //Add new UserResponse to Respone
            response.UserResponses.Add(userResponse);
        }

        private void UpdateUserResponse(Question question, Response response, UserResponse userResponse)
        {
            //Remove userResponse from its current response
            userResponse.Response.UserResponses.Remove(userResponse);
            //Change userResponse's response to the passed in response
            userResponse.Response = response;
            //Add userResponse to passed int response
            response.UserResponses.Add(userResponse);
        }

        private bool CanAddFreeResponse(Question question, Response response)
        {
            return question.FreeResponseEnabled && question.Responses.Count(x => x.Text.Equals(response.Text)) > 0;
        }

        //TODO: Implement this method for realsies 
        private bool UserHasRespondedBefore(Question question, Response response)
        {
            return false;
        }
    }
}
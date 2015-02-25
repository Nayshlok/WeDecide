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
            RespondToQuestionViewModel vm = new RespondToQuestionViewModel(question);
            return View("~/Views/QuestionResponse/Response.cshtml", vm);
        }

        [HttpPost]
        public ActionResult QuestionResponse(string ChosenResponse, int QuestionId)
        {
            //Make New UserResponse with given string and question id
            Question AffectedQuestion = Qdal.Get(QuestionId);
            Response Resp = AffectedQuestion.Responses.First(x => x.Text.Equals(ChosenResponse));
            //UserResponse NewUR = new UserResponse() { Question = AffectedQuestion, QuestionId = AffectedQuestion.Id, Response = Resp, ResponseId = Resp.Id, RespontdantId = User.Identity.GetUserId() };
            //AffectedQuestion.UserResponses.Add(NewUR);
            Qdal.Update(QuestionId, AffectedQuestion);
            
            //Add UserResponse to Question
            //Would like to have this not actually return, as the Partial View will always be a part of something else
            return View(QuestionId);
        }
    }
}
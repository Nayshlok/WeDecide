using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.DAL.Abstract;
using WeDecide.DAL.Concrete;
using WeDecide.Models.Concrete;
using WeDecide.ViewModels;

namespace WeDecide.Controllers
{
    public class QuestionController : Controller
    {
        //Until we have the DAL injection done
        private static IQuestionDAL Qdal = new MemoryQuestionDAL();

        [HttpGet]
        public ViewResult CreateQuestion()
        {
            //Get the Question creation view.
            return View();
        }

        [HttpPost]
        public ActionResult CreateQuestion(QuestionViewModel q)
        {
            if (ModelState.IsValid)
            {
                //Construct the Question
                Question NewQuestion = q.GetQuestion();
                //Add current user to question
                //Save the Question
                Qdal.Create(NewQuestion);
                //Don't know what to return yet, so returning response page
                return View("~/Views/QuestionResponse/Response.cshtml", q);
            }
            return View();
        }
        
        [HttpGet]
        public ViewResult EditQuestion(int ID)
        {
            //Retrieve the question and allow user to change properties.
            return View();
        }

        [HttpPost]
        public ActionResult EditQuestion(QuestionViewModel q)
        {
            if(ModelState.IsValid)
            {
            //Take in the edited question from the user and change the original question.
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult RemoveQuestion(int ID)
        {
            //Retrieve the question and remove it from the database.
            //Send user back to some default view.
            return View();
        }
    }
}
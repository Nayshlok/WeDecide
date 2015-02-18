using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.ViewModels;

namespace WeDecide.Controllers
{
    public class QuestionController : Controller
    {
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
                //Do Stuff

                //Don't know what to return yet
                return View();
            }
            else
            {
                return View();
            }
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
            //Take in the edited question from the user and change the original question.
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
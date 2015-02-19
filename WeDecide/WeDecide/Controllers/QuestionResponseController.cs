using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.ViewModels;

namespace WeDecide.Controllers
{
    public class QuestionResponseController : Controller
    {
        // GET: QuestionResponse
        [HttpGet]
        public ActionResult QuestionResponse(QuestionViewModel question)
        {
            return View("~/View/Shared/_ResponseToQuestionPartial.cshtml", question);
        }

        [HttpPost]
        public ActionResult QuestionResponse(UserResponseViewModel UserResponse)
        {
            //Add or Update UserResponse to appropriate question
            
            //Would like to have this not actually return, as the Partial View will always be a part of something else
            return View();
        }
    }
}
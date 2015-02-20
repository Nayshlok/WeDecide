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
        public ActionResult ResponseToQuestion(QuestionViewModel question)
        {
            return View("~/View/Shared/_ResponseToQuestionPartial.cshtml", question);
        }

        [HttpPost]
        public void ResponseToQuestion(UserResponseViewModel UserResponse)
        {
            //Add or Update UserResponse to appropriate question
        }
    }
}
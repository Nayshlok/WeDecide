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

namespace WeDecide.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
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

        [HttpPost]
        public ActionResult CreateQuestion(MakeQuestionViewModel q)
        {
            if (ModelState.IsValid)
            {
                //Construct the Question
                Question NewQuestion = q.GetQuestion();
                //Add current user to question
                NewQuestion.User = Mdal.GetUser(User.Identity.GetUserId());
                //Save the Question
                Qdal.Create(NewQuestion);
                //Don't know what to return yet, so returning response page
                //RespondToQuestionViewModel model = new RespondToQuestionViewModel(NewQuestion);
                //return RedirectToAction("QuestionResponse", "QuestionResponse", new { id = NewQuestion.Id });
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
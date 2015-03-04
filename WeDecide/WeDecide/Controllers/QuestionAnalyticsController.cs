using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.DAL;
using WeDecide.DAL.Abstract;
using WeDecide.DAL.Concrete;
using WeDecide.Models.Concrete;
using WeDecide.ViewModels;

namespace WeDecide.Controllers
{
    [Authorize]
    public class QuestionAnalyticsController : Controller
    {
        IQuestionDAL Qdal = new MemoryQuestionDAL();

        // GET: QuestionAnalytics
        public ActionResult QuestionPoll(int QuestionId)
        {
            Question PolledQuestion = Qdal.Get(QuestionId);

            QuestionPollingViewModel vm = new QuestionPollingViewModel(PolledQuestion);

            return View(vm);
        }
    }
}
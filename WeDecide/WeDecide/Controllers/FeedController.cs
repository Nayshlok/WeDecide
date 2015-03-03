using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.DAL.Abstract;
using WeDecide.DAL.Concrete;

namespace WeDecide.Controllers
{
    [Authorize]
    public class FeedController : Controller
    {
        //private IQuestionDAL = new SqlQuestionDAL();
        
        // GET: Feed
        public ActionResult Index()
        {
            return View();
        }
    }
}
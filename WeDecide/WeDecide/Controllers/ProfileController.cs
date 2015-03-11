using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDecide.DAL.Abstract;
using Microsoft.AspNet.Identity;
using WeDecide.ViewModels;
using System.IO;
using WeDecide.Models.Concrete;

namespace WeDecide.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private IMembershipDAL memberDal;
        private IQuestionDAL questionDal;

        public ProfileController(IMembershipDAL memberDalParam, IQuestionDAL questionDalParam)
        {
            memberDal = memberDalParam;
            questionDal = questionDalParam;
        }

        [HttpGet]
        public ActionResult Index()
        {
            Models.Concrete.User currentUser = memberDal.GetUser(User.Identity.GetUserId());

            ProfileViewModel userProfile = new ProfileViewModel()
            {
                UserName = currentUser.Name,
                UserFriends = memberDal.GetFriends(currentUser.Id),
                UserQuestions = questionDal.GetAll(x => x.UserId == currentUser.Id),
                ImagePath = currentUser.ImagePath
            };

            return View("ProfileView", userProfile);
        }

        public ActionResult UpdateProfile(ProfileViewModel pvm)
        {
            Models.Concrete.User currentUser = memberDal.GetUser(User.Identity.GetUserId());
            if(ModelState.IsValid)
            {
                //Change the user's name
                currentUser.Name = pvm.UserName;  
            }

            ProfileViewModel userProfile = new ProfileViewModel()
            {
                UserName = currentUser.Name,
                UserFriends = memberDal.GetFriends(currentUser.Id),
                UserQuestions = questionDal.GetAll(x => x.UserId == currentUser.Id),
                ImagePath = currentUser.ImagePath
            };

            return View("ProfileView", userProfile);
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase image)
        {
            Models.Concrete.User currentUser = memberDal.GetUser(User.Identity.GetUserId());

            if(image != null)
            {
                string strLocation = HttpContext.Server.MapPath("~/Images/" + currentUser.Id + "/profile");

                //Create directory for user images if it does not exists
                if(!Directory.Exists(strLocation))
                {
                    Directory.CreateDirectory(strLocation);
                }
                else
                {
                    //Delete the old profile pic
                    string[] existingImage = Directory.GetFiles(strLocation);
                    if(existingImage.Length > 0)
                    {
                        System.IO.File.Delete(existingImage[0]);
                    }
                }

                image.SaveAs(strLocation + @"\" + image.FileName.Replace('+', '_'));
                memberDal.SaveImagePath(currentUser.Id, @"\Images\" + currentUser.Id + "\\profile\\" + image.FileName);
            }

            return RedirectToAction("Index");
        }

        public JsonResult getNotifications()
        {
            IEnumerable<Notification> notifications = memberDal.GetNotifications(User.Identity.GetUserId());

            //Create an object that can deliver the messages through json
            List<NotificationViewModel> nvms = new List<NotificationViewModel>();
            foreach(Notification n in notifications)
            {
                nvms.Add(new NotificationViewModel { SenderName = n.SendingUser.Name, SenderID = n.SenderId, Message = n.Message });
            }

            return Json(nvms, JsonRequestBehavior.AllowGet);
        }
    }
}
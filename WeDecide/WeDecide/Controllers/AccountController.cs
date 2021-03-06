﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WeDecide.ViewModels;
using WeDecide.Models.Concrete;
using WeDecide.DAL.Abstract;
using WeDecide.DAL.Concrete;
using Microsoft.AspNet.Identity.EntityFramework;
using Facebook;
using Microsoft.AspNet.Facebook;

namespace WeDecide.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IMembershipDAL _membershipDAL;
        private ApplicationRoleManager _roleManager;

        public IMembershipDAL MembershipDAL
        {
            get { return _membershipDAL; }
            set { _membershipDAL = value; }
        }

        public AccountController(IMembershipDAL dal)
        {
            MembershipDAL = dal;
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager, IMembershipDAL membership)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            MembershipDAL = membership;
            RoleManager = roleManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return this._roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set { this._roleManager = value; }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Username does not match the password");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    AddToRole(user, UserRoles.User);
                    MembershipDAL.AddUser(model.UserName, model.Email, user.Id);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        //[FacebookAuthorize("email", "user_friends")]
        public async Task<ActionResult> ExternalLoginCallback(FacebookContext context, string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {

                await SignInAsync(user, isPersistent: false);
                MergeFriends();
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;

                var UserClaim = await UserManager.GetClaimsAsync(User.Identity.GetUserId());
                var NewUser = new ApplicationUser() { UserName = loginInfo.Email, Email = loginInfo.Email };
                var result = await UserManager.CreateAsync(NewUser);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(NewUser.Id, loginInfo.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(NewUser, isPersistent: false);
                        AddUserFromExternalLogin(loginInfo, NewUser);
                        MergeFriends();
                        return RedirectToLocal(returnUrl);
                    }
                }
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[FacebookAuthorize("email", "user_friends")]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName};
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        AddUserFromExternalLogin(info, user);
                        await SignInAsync(user, isPersistent: false);
                        MergeFriends();
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        private async void MergeFriends()
        {
            var UserClaim = await UserManager.GetClaimsAsync(User.Identity.GetUserId());
            var token = UserClaim.FirstOrDefault(fb => fb.Type == "FacebookAccessToken").Value;
            var FbClient = new FacebookClient(token);

            dynamic FacebookFriends = FbClient.Get("/me/friends");
            foreach (dynamic MyFriend in FacebookFriends.data)
            {
                var friendId = MyFriend.Id;
                User currentUser = MembershipDAL.GetUser(User.Identity.GetUserId());
                if (!currentUser.MyFriends.Contains(MembershipDAL.GetUser(friendId)))
                {
                    if (MembershipDAL.GetUser(friendId) != null)
                    {
                        currentUser.MyFriends.Add(MembershipDAL.GetUser(friendId));
                    }
                }
            }
        }

        private async void AddUserFromExternalLogin(ExternalLoginInfo info, ApplicationUser user)
        {

            //If facebook
            if("facebook".Equals(info.Login.LoginProvider.ToLower()))
            {
                var UserClaim = await UserManager.GetClaimsAsync(User.Identity.GetUserId());
                var token = UserClaim.FirstOrDefault(fb => fb.Type == "FacebookAccessToken").Value;
                var FbClient = new FacebookClient(token);
                string Name = FbClient.Get("me/name/") as string;
                MembershipDAL.AddUser(Name, user.Email, user.Id);
                //I hope one of these works
                //FacebookClient fb = new FacebookClient("288b1b9a9d7de9198db8fd84a9ab93c8");
                //FacebookClient fb = new FacebookClient(Session["AccessToken"].ToString());'
            }
        }

        public ActionResult Permissions(FacebookRedirectContext context)
        {
            if (ModelState.IsValid)
            {
                return View(context);
            }

            return View("Error");
        }


        [HttpPost]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            //Get rid of dat cookie
            HttpCookie cookie = Request.Cookies[".AspNet.ApplicationCookie"];
            cookie.Expires = DateTime.Now.AddYears(-1000);
            Response.Cookies.Add(cookie);
            Session.Clear();

            return RedirectToAction("Login");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }


        private void AddToRole(ApplicationUser user, UserRoles role)
        {
            string roleName = Enum.GetName(typeof(UserRoles), role);
            if (!RoleManager.RoleExists(roleName))
            {
                RoleManager.Create(new IdentityRole(roleName));
            }
            UserManager.AddToRole(user.Id, roleName);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
        }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
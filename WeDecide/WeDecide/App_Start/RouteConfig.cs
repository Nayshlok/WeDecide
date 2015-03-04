using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WeDecide
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "New Question",
                url: "Question/New",
                defaults: new { controller = "Question", action = "CreateQuestion" });

            routes.MapRoute(
                name: "Profile",
                url: "Profile",
                defaults: new { controller = "Profile", action = "Index"});

            routes.MapRoute(
                name: "Friends",
                url: "Friends",
                defaults: new { controller = "Friends", action = "Index" });

            routes.MapRoute(
                name: "Poll",
                url:"Question/Poll/{QuestionId}",
                defaults: new { controller = "QuestionAnalyticsController", action = "QuestionPoll"});

            routes.MapRoute(
                name: "Response",
                url: "Respond/{QuestionId}",
                defaults: new { controller = "QuestionResponse", action = "QuestionResponse" });

            routes.MapRoute(
                name: "Delete Question",
                url: "Question/Delete/{QuestionId}",
                defaults: new { controller = "Question", action = "RemoveQuestion" });

            routes.MapRoute(
                name: "Update Profile",
                url: "Profile/Update",
                defaults: new { controller = "Profile", action = "UpdateProfile" });

            routes.MapRoute(
                name: "Add Friend",
                url: "Friend/Add/{UserId}",
                defaults: new { controller = "Friends", action = "AddFriend" });

            routes.MapRoute(
                name: "Search Friends",
                url: "SearchFriends",
                defaults: new { controller = "Friends", action = "SearchFriends" });

            routes.MapRoute(
                name: "Delete Friend",
                url: "Friend/Delete/{UserId}",
                defaults: new { controller = "Friends", action = "DeleteFriend" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;

namespace WeDecide
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/Content/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                "~/Scripts/jquery-1.11.2.min.js",
                "~/Scripts/jquery-ui-1.11.3.min.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/MakeQuestion").Include(
                "~/Scripts/app/MakeQuestion.js",
                "~/Scripts/app/EditProfile.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Angular").Include(
                "~/Scripts/angular.js",
                //"~/Scripts/angular.min.js",
                "~/Scripts/app/questionsViaAngular.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Globals").Include(
                "~/Scripts/app/functions.js",
                "~/Scripts/app/GlobalFeed.js"));
        }
    }
}

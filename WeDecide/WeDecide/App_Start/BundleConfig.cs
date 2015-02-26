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

            bundles.Add(new ScriptBundle("~/Scripts/MakeQuestion").Include(
                "~/Scripts/jquery-1.11.2.min.js",
                "~/Scripts/jquery-ui-1.11.3.min.js",
                "~/Scripts/MakeQuestion.js",
                "~/Scripts/EditProfile.js"));
        }
    }
}

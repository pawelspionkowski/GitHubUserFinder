using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace GitHubUserFinder.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/GitHubUpdater").Include(
                        "~/Scripts/Controllers/Home/Index/GitHubUpdater.js"));

            bundles.Add(new ScriptBundle("~/bundles/FormValidate").Include(
                        "~/Scripts/Controllers/Home/Index/FormValidate.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Table.css",
                      "~/Content/site.css"));
        }
    }
}
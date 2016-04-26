using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace InfoVideo
{
    public class BundleConfig
    {
   
        public static void RegisterBundles(BundleCollection bundles)
        {
           
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Libs/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Libs/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/Scripts")
                   .Include(
                     "~/Scripts/Libs/jquery.unobtrusive-ajax.js",
                "~/Scripts/FrontEnd/ninjaVideoPlugin.js",
                "~/Scripts/FrontEnd/ninja-slider.js",
                 "~/Scripts/FrontEnd/lab2.js",
                 "~/Scripts/FrontEnd/site.js",
                "~/Scripts/FrontEnd/ion.sound.js",
                "~/Scripts/main.js"
                ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.css",
                       "~/Content/material-design-iconic-font.css",
                         "~/Content/ninja-slider.css",
                      "~/Content/Site.css"));
        }
    }
}

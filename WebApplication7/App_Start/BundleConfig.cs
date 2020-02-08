using System.Web;
using System.Web.Optimization;

namespace WebApplication7
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/jquery-ui.css",
                "~/Content/jquery.tagsinput-revisited.css"
                ));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.tagsinput-revisited.js",
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/jquery.moment.js",
                        "~/Scripts/jquery.daterangepicker.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));



        }
    }
}

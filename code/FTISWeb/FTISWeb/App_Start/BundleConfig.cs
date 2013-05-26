using System.Web.Optimization;

namespace FTISWeb
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/JQuery").Include(
                "~/Scripts/jquery-1.9.1.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/TreeView").Include(
                "~/Scripts/jquery-1.9.1.js",
                "~/Scripts/jquery.treeview.js"));

            bundles.Add(new ScriptBundle("~/bundles/CKEditor").Include(
                "~/Scripts/ckeditor/ckeditor.js",
                "~/Scripts/ckfinder/ckfinder.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendoUI").Include(
                "~/Scripts/kendo/kendo.web.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/TreeView").Include(
                "~/Scripts/jquery-1.9.1.js",
                "~/Scripts/jquery.treeview.js"));

            bundles.Add(new ScriptBundle("~/bundles/SiteCommon").Include(
                "~/Scripts/site.common.js"));

            bundles.Add(new StyleBundle("~/Content/Admin/LogOnCSS").Include(
                "~/Content/Admin/css.css",
                "~/Content/Validation.css"));

            bundles.Add(new StyleBundle("~/Content/Admin/AdminCSS").Include(
                "~/Content/Admin/admin.css",
                "~/Content/Admin/css.css",
                "~/Content/Validation.css"));            

            bundles.Add(new StyleBundle("~/Content/Admin/LeftMenuCSS").Include(
                "~/Content/Admin/admin.css",
                "~/Content/Admin/css.css",
                "~/Content/Admin/jquery.treeview.css"));

            bundles.Add(new StyleBundle("~/Content/KendoCSS").Include(
                "~/Content/kendo/kendo.common.min.css",
                "~/Content/kendo/kendo.blueopal.min.css"
                ));
        }
    }
}
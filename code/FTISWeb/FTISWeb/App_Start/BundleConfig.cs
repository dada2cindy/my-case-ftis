using System;
using System.Web.Optimization;

namespace FTISWeb
{
    public class BundleConfig
    {
        //public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        //{
        //    if (ignoreList == null)
        //        throw new ArgumentNullException("ignoreList");
        //    ignoreList.Ignore("*.intellisense.js");
        //    ignoreList.Ignore("*-vsdoc.js");
        //    ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
        //    //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
        //    ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        //}

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/JQuery").Include(
                "~/Scripts/jquery-1.9.1.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/TreeView").Include(
                "~/Scripts/jquery-1.9.1.js",
                "~/Scripts/jquery.treeview.js"));

            //bundles.Add(new ScriptBundle("~/bundles/CKEditor").Include(
            //    "~/Scripts/ckeditor/ckeditor.js",
            //    "~/Scripts/ckfinder/ckfinder.js"));


            bundles.Add(new ScriptBundle("~/bundles/TreeView").Include(
                "~/Scripts/jquery-1.9.1.js",
                "~/Scripts/jquery.treeview.js"));

            bundles.Add(new ScriptBundle("~/bundles/SiteCommon").Include(
                "~/Scripts/site.common.js",
                "~/Scripts/exportExcel.js"));

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

            bundles.Add(new ScriptBundle("~/bundles/KendoUI").Include(
                "~/Scripts/kendo/kendo.web.js",
                "~/Scripts/kendo/cultures/kendo.culture.zh-TW.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/KendoCSS").Include(
                "~/Content/kendo/kendo.common.css",
                "~/Content/kendo/kendo.blueopal.css"));

            /***************************************************************************
            前台
            ***************************************************************************/
            bundles.Add(new ScriptBundle("~/bundles/SiteHome").Include(
                "~/Scripts/site.home.js",
                "~/Scripts/AC_RunActiveContent.js",
                "~/Scripts/swfobject_modified.js"));

            bundles.Add(new StyleBundle("~/Content/SiteHomeCSS").Include(
                "~/css/Index-Sdyle.css",
                "~/css/AllCss.css",
                "~/css/IndexCss.css"));

            bundles.Add(new StyleBundle("~/Content/SiteInCSS").Include(
                "~/Content/MvcPaging.css",
                "~/css/AllCss.css",
                "~/css/In-Style.css",
                "~/css/InCss.css"));

            bundles.Add(new StyleBundle("~/Content/EngSiteInCSS").Include(
                "~/Content/MvcPaging.css",
                "~/css/AllCss.css",
                "~/css/In-Style.css",
                "~/css/Ecss.css",
                "~/css/InCss.css"));

            bundles.Add(new ScriptBundle("~/bundles/CKFinder").Include(
                "~/Scripts/ckfinder/ckfinder.js"));
        }
    }
}
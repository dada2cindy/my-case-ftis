using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTISWeb.Utility;

namespace FTISWeb.Controllers
{
    public class FileHandleController : Controller
    {
        public ActionResult GetFromCKFinder(string path)
        {
            string ckFinderBaseDir = AppSettings.CKFinderBaseDir.Replace('/', '\\');
            string filePath = Path.Combine(ckFinderBaseDir, path);
            return File(filePath, "image/png");
        }

    }
}

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
            return File(GetFile(filePath), FileUtil.GetMIMEfromExtension(System.IO.Path.GetExtension(filePath)), System.IO.Path.GetFileName(filePath));
            //if (path.IndexOf("flash/") != -1)
            //{
            //    return File(filePath, "application/x-shockwave-flash");
            //}
            //else if (path.IndexOf("files/") != -1)
            //{
            //    return File(filePath, FileUtil.GetMIMEfromExtension(System.IO.Path.GetExtension(filePath)), System.IO.Path.GetFileName(filePath));
            //}
            //else
            //{
            //    return File(filePath, "image/png");
            //}
        }

        /// <summary>
        /// 取得檔案
        /// </summary>
        /// <returns></returns>
        public FileStream GetFile(string filePath)
        {
            return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
    }
}

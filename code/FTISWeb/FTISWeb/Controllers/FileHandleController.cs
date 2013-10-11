using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTISWeb.Utility;
using WuDada.Core.Generic.Util;

namespace FTISWeb.Controllers
{
    public class FileHandleController : Controller
    {
        public ActionResult GetFromCKFinder(string path)
        {
            string ckFinderBaseDir = AppSettings.CKFinderBaseDir.Replace('/', '\\');
            string filePath = Path.Combine(ckFinderBaseDir, path);

            if (!new FileInfo(filePath).Exists || !filePath.StartsWith(ckFinderBaseDir))
            {
                return new EmptyResult();
            }

            return File(GetFile(filePath), FileUtil.GetMIMEfromExtension(System.IO.Path.GetExtension(filePath)), System.IO.Path.GetFileName(filePath));
        }

        public ActionResult GetFromCKFinderByEncrypt(string encryptPath)
        {
            string path = System.Web.HttpUtility.UrlDecode(EncryptUtil.DecryptDES(encryptPath, AppSettings.EncryptKey, AppSettings.EncryptIV));
            string ckFinderBaseDir = AppSettings.CKFinderBaseDir.Replace('/', '\\');
            string filePath = Path.Combine(ckFinderBaseDir, path);

            if (!new FileInfo(filePath).Exists || !filePath.StartsWith(ckFinderBaseDir))
            {
                return new EmptyResult();
            }

            return File(GetFile(filePath), FileUtil.GetMIMEfromExtension(System.IO.Path.GetExtension(filePath)), System.IO.Path.GetFileName(filePath));
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

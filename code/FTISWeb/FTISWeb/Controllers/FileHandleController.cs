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
        public ActionResult ShowImgFile(string path)
        {
            ViewBag.FilePath = path;
            return View();
        }

        public ActionResult GetFromLocal(string path)
        {
            string filePath = Server.MapPath("~/" + path);

            if (!new FileInfo(filePath).Exists)
            {
                return new EmptyResult();
            }

            return File(GetFile(filePath), FileUtil.GetMIMEfromExtension(System.IO.Path.GetExtension(filePath)), System.IO.Path.GetFileName(filePath));
        }

        public ActionResult GetFromCKFinder(string path, string fileName)
        {
            string ckFinderBaseDir = AppSettings.CKFinderBaseDir.Replace('/', '\\');
            string filePath = Path.Combine(ckFinderBaseDir, path);

            if (!new FileInfo(filePath).Exists || !filePath.StartsWith(ckFinderBaseDir))
            {
                return new EmptyResult();
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = System.IO.Path.GetFileName(filePath);
            }
            else
            {
                fileName += System.IO.Path.GetExtension(filePath);
            }

            return File(GetFile(filePath), FileUtil.GetMIMEfromExtension(System.IO.Path.GetExtension(filePath)), fileName);
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

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="qqfile"></param>
        /// <returns></returns>
        public ActionResult BasicUpload(string qqfile, string folder, string targetField)
        {
            string ckFinderBaseDir = AppSettings.CKFinderBaseDir.Replace('/', '\\');
            string path = Path.Combine(ckFinderBaseDir, folder);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string fileName = string.Empty;

            try
            {
                string originalName = qqfile;
                fileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), originalName.Substring(originalName.LastIndexOf('.')));

                Stream stream = Request.Files.Count > 0
                    ? Request.Files[0].InputStream
                    : Request.InputStream;
                string filePath = Request.Files.Count > 0
                    ? Path.Combine(path, fileName)
                    : Path.Combine(path, fileName);
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                System.IO.File.WriteAllBytes(filePath, buffer);
                return Json(new { success = true, targetField = targetField, filePath = string.Format("{0}/{1}", folder, fileName) }, "text/html");
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message }, "text/html");
            }
        }

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="qqfile"></param>
        /// <returns></returns>
        public ActionResult LocalUpload(string qqfile, string folder, string targetField)
        {
            string rootDir = Server.MapPath("~/");
            string path = Path.Combine(rootDir, folder);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string fileName = string.Empty;

            try
            {
                string originalName = qqfile;
                fileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), originalName.Substring(originalName.LastIndexOf('.')));

                Stream stream = Request.Files.Count > 0
                    ? Request.Files[0].InputStream
                    : Request.InputStream;
                string filePath = Request.Files.Count > 0
                    ? Path.Combine(path, fileName)
                    : Path.Combine(path, fileName);
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                System.IO.File.WriteAllBytes(filePath, buffer);                
                return Json(new { success = true, targetField = targetField, filePath = string.Format("/{0}/{1}", folder, fileName) }, "text/html");
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message }, "text/html");
            }
        }
    }
}

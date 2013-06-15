using System.Collections.Generic;
using System.Web.Mvc;
using FTIS;
using FTIS.ACUtility;
using FTIS.Domain;
using FTIS.Domain.Impl;
using FTIS.Service;
using FTISWeb.Helper;

namespace FTISWeb.Controllers
{
    public class DownloadClassData : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ////下載單員
            IList<SelectListItem> downloadClassList = new List<SelectListItem>()
                {
                    new SelectListItem(){Text= "技術工具/文件", Value= "1"}
                    ,new SelectListItem(){Text= "課程講義", Value= "2"}                    
                };
            context.Controller.ViewData["DownloadClassList"] = downloadClassList;
        }
    }
}

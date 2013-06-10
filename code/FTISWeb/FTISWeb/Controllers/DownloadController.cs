﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using FTIS;
using FTIS.Domain;
using FTIS.Domain.Dto;
using FTIS.Domain.Impl;
using FTIS.Service;
using FTISWeb.Models;
using FTISWeb.Utility;
using MvcPaging;
using WuDada.Core.Generic.Util;

namespace FTISWeb.Controllers
{
    public partial class DownloadController : Controller
    {
        public ActionResult Index(string keyWord, int? page)
        {
            SetConditions(keyWord);
            m_Conditions.Add("Status", "1");
            int total = GetGridTotal();
            int pageIndex = page.HasValue ? page.Value - 1 : 0;
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", AppSettings.InSitePageSize.ToString());

            var data = GetGridData();
            return View(data.ToPagedList(pageIndex, AppSettings.InSitePageSize, total));
        }

        public ActionResult Detail(string id, string cdts)
        {
            GetConditions(cdts);
            EntityCounter(id, "Vister");
            DownloadModel entityModel = new DownloadModel(id);
            if ("1".Equals(entityModel.IsOut) && !string.IsNullOrWhiteSpace(entityModel.AUrl))
            {
                Response.Redirect(entityModel.AUrl);
            }
            return View(entityModel);
        }

        public ActionResult DownLoadFile(string id,string fileUrl)
        {
            EntityCounter(id, "Downer");
            Response.Redirect(fileUrl);

            return new EmptyResult();
        }

        private void EntityCounter(string id, string type)
        {
            Download entity = m_FTISService.GetDownloadById(int.Parse(DecryptId(id)));
            switch (type)
            {
                case "Vister":
                    entity.Vister += 1;
                    break;
                case "Downer":
                    entity.Downer += 1;
                    break;
            }
            m_FTISService.UpdateDownload(entity);
        }

        private string DecryptId(string id)
        {
            return EncryptUtil.DecryptDES(id, AppSettings.EncryptKey, AppSettings.EncryptIV);
        }
    }
}
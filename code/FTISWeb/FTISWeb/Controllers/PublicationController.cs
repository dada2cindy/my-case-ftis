using System;
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
using FTISWeb.Security;
using FTISWeb.Utility;
using KendoGridBinder;
using KendoGridBinder.Containers;
using WuDada.Core.Generic.Util;
using MvcPaging;

namespace FTISWeb.Controllers
{
    public partial class PublicationController : Controller
    {
        [PublicationClassData(OnlyOpen = true)]
        public ActionResult Index(string keyWord, string publicationClassId, int? page)
        {
            publicationClassId = DecryptId(publicationClassId);
            if (string.IsNullOrWhiteSpace(publicationClassId))
            {
                publicationClassId = ((IList<PublicationClass>)ViewData["PublicationClassList"])[0].PublicationClassId.ToString();
            }
            PublicationClass publicationClass = m_FTISService.GetPublicationClassById(int.Parse(publicationClassId));
            ViewData["PublicationClass"] = publicationClass;

            SetConditions(keyWord, publicationClassId);
            m_Conditions.Add("Status", "1");         

            var data = GetGridData();
            return View(data);
        }

        [PublicationClassData(OnlyOpen = true)]
        public ActionResult Print(string publicationClassId)
        {
            publicationClassId = DecryptId(publicationClassId);
            if (string.IsNullOrWhiteSpace(publicationClassId))
            {
                publicationClassId = ((IList<PublicationClass>)ViewData["PublicationClassList"])[0].PublicationClassId.ToString();
            }
            PublicationClass publicationClass = m_FTISService.GetPublicationClassById(int.Parse(publicationClassId));
            ViewData["PublicationClass"] = publicationClass;

            SetConditions(string.Empty, publicationClassId);
            m_Conditions.Add("Status", "1");

            var data = GetGridData();
            return View(data);
        }

        private string DecryptId(string id)
        {
            return EncryptUtil.DecryptDES(id, AppSettings.EncryptKey, AppSettings.EncryptIV);
        }
    }
}

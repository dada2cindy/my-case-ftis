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
    public partial class NormController : Controller
    {
        [NormClassData(OnlyOpen = true)]
        public ActionResult Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = ((IList<NormClass>)ViewData["ParentNormClassList"])[0].NormClassId.ToString();
                id = new NormClassModel().EncryptId(id);
            }

            return View(new NormClassModel(id));
        }

        [NormClassData(OnlyOpen = true)]
        public ActionResult Detail(string id,string parentId)
        {
            string pId= DecryptId(parentId);
            SetConditions(string.Empty, string.Empty, pId);
            m_Conditions.Add("Status", "1");
            IList<Norm> normList = GetGridData().ToList();
            ViewData["NormList"] = normList;

            if (!string.IsNullOrWhiteSpace(parentId))
            {
                NormClass normClass = m_FTISService.GetNormClassById(int.Parse(pId));
                ViewData["NormClass"] = normClass;
            }

            NormModel model = new NormModel();

            if (!string.IsNullOrWhiteSpace(id))
            {
                model = new NormModel(id);
            }
            else
            {
                if (normList != null && normList.Count > 0)
                {
                    model = new NormModel(normList[0].NormId);
                }
            }

            return View(model);
        }


        private string DecryptId(string id)
        {
            return EncryptUtil.DecryptDES(id, AppSettings.EncryptKey, AppSettings.EncryptIV);
        }
    }
}

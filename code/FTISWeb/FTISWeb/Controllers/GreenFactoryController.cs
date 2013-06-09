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

namespace FTISWeb.Controllers
{
    public partial class GreenFactoryController : Controller
    {
        [GreenFactoryClassData(OnlyOpen = true)]
        public ActionResult Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                int classId = ((IList<GreenFactoryClass>)ViewData["GreenFactoryClassList"])[0].GreenFactoryClassId;
                IList<GreenFactory> list = new GreenFactoryClassModel().GetGreenFactoryList(true, classId);
                return View(new GreenFactoryModel(list[0].GreenFactoryId, true));
            }

            return View(new GreenFactoryModel(id, true));
        }
    }
}

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

namespace FTISWeb.Controllers
{
    public partial class ActivityController : Controller
    {
        public ActionResult Index(string cdts)
        {
            GetConditions(cdts);
            return View();
        }

        public ActionResult Detail(string id, string cdts)
        {
            GetConditions(cdts);
            return View(new ActivityModel(id));
        }
    }
}

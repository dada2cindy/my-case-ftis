using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTIS.Domain;
using FTISWeb.Models;

namespace FTISWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeShowModel model = new HomeShowModel();
            model.AddCount("1");
            return View(model);
        }

        public ActionResult EngIndex()
        {
            //Introduction
            int id = 5;
            return View(new NodeModel(id));
        }

        public ActionResult EngSiteMap()
        {
            return View();
        }

        [LinksClassData(OnlyOpen = true)]
        [ApplicationClassData(OnlyOpen = true)]
        [NormClassData(OnlyOpen = true)]
        [NodeSubData(OnlyOpen = true, ParentNodeId = SiteParentNode.Carbon)]
        [GreenFactoryClassData(OnlyOpen = true)]
        [NodeSubData2(OnlyOpen = true, ParentNodeId = SiteParentNode.Green, NodeName = "Green")]
        [NodeSubData3(OnlyOpen = true, ParentNodeId = SiteParentNode.TechnologyParent, NodeName = "Technology")]
        [QuestionClassData(OnlyOpen = true)]
        public ActionResult SiteMap()
        {
            return View();
        }

        public ActionResult Search(string keyWord)
        {
            SearchModel searchModel = new SearchModel(keyWord);

            return View(searchModel);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTISWeb.Models;

namespace FTISWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new HomeShowModel());
        }

        public ActionResult EngIndex()
        {
            //Introduction
            int id = 5; 
            return View(new NodeModel(id));
        }
    }
}

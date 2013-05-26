using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTISWeb.Models;

namespace FTISWeb.Controllers
{
    public class FreeGOController : Controller
    {
        [ValidateInput(false)]
        public ActionResult Show(string title, string htmlValue)
        {
            return View(new FreeGOModel(title, htmlValue));
        }

    }
}

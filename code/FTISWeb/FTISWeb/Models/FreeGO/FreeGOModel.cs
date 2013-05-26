using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using FTIS;
using FTIS.Domain.Impl;
using FTIS.Service;
using FTISWeb.Security;
using FTISWeb.Helper;
using WuDada.Core.Generic.Util;
using System.ComponentModel;
using WuDada.Accessibility.FreeGO;
using FTISWeb.Utility;

namespace FTISWeb.Models
{
    public class FreeGOModel
    {
        ////預設檢測A+
        //private readonly Accessibility.ckdegreeEnum Type = Accessibility.ckdegreeEnum.APlus;

        public string Title { get; set; }
        public string HtmlValue { get; set; }
        public string Msg { get; set; }

        public FreeGOModel()
        {
        }

        public FreeGOModel(string title, string htmlValue)
        {
            this.Title = title;
            this.HtmlValue = htmlValue;
            CheckFreeGO();
        }

        public void CheckFreeGO()
        {                        
            this.Msg = AccessibilityUtil.CheckFreeGOAndReturnMsg(this.HtmlValue);
        }
    }
}

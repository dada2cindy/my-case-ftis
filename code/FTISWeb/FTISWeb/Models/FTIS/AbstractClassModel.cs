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
using Microsoft.Security.Application;

namespace FTISWeb.Models
{
    public abstract class AbstractClassModel : AbstractEntityModel
    {
        public AbstractClassModel()
        {
        }

        /// <summary>
        /// 英文名稱
        /// </summary>
        //[DisplayName("名稱(英)")]
        //[Required]
        public string NameENG { get; set; }
    }
}

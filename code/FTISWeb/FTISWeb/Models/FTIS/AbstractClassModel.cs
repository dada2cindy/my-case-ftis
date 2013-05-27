﻿using System;
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
    public abstract class AbstractClassModel
    {
        protected readonly FTISFactory m_FTISFactory = new FTISFactory();
        protected readonly IFTISService m_FTISService;

        public AbstractClassModel()
        {
            m_FTISService = m_FTISFactory.GetFTISService();
            this.Status = "1";
        }

        public int ClassId { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        [DisplayName("名稱")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        [Required]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "必須為數字")]
        public int SortId { get; set; }

        /// <summary>
        /// 狀態. 0.關閉 1.開啟
        /// </summary>
        [DisplayName("狀態")]
        [Required]
        public string Status { get; set; }
    }
}

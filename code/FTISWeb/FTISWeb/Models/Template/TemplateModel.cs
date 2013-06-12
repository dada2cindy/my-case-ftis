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

namespace FTISWeb.Models
{
    public class TemplateModel : AbstractEntityModel
    {
        protected readonly ITemplateService m_TemplateService;

        /// <summary>
        /// CSS
        /// </summary>
        public string CSS { get; set; }

        /// <summary>
        /// 樣板檔案
        /// </summary>
        [DisplayName("樣板檔案")]
        [Required]
        public string FileName { get; set; }

        /// <summary>
        /// Flash檔案
        /// </summary>
        [DisplayName("Flash檔案")]
        [Required]
        public string FileName2 { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        [DisplayName("樣板檔案")]
        [Required]
        public string StartDate { get; set; }

        /// <summary>
        /// 結束日期
        /// </summary>
        [DisplayName("樣板檔案")]
        [Required]
        public string EndDate { get; set; }

        /// <summary>
        /// 類別
        /// </summary>
        public virtual TemplateVO.Type TemplateType { get; set; }

        public TemplateModel()
        {
            m_TemplateService = m_FTISFactory.GetTemplateService();
            Status = "1";
        }

        public TemplateModel(int id)
        {
            LoadTemplate(id);
        }

        protected void LoadTemplate(int id)
        {
            TemplateVO template;

            template = m_TemplateService.GetTemplateById(id);

            LoadTemplate(template);
        }

        protected void LoadTemplate(TemplateVO template)
        {
            if (template != null)
            {
                EntityId = template.TemplateId;
                Name = template.Name;
                CSS = template.CSS;
                FileName = template.FileName;
                FileName2 = template.FileName2;
                StartDate = template.StartDate;
                EndDate = template.EndDate;
                Status = template.Flag.ToString();
                TemplateType = template.TemplateType;
            }
        }

        public void Insert()
        {
            TemplateVO template = new TemplateVO();
            Save(template);
        }

        public void Update()
        {
            TemplateVO template = m_TemplateService.GetTemplateById(EntityId);
            Save(template);
        }

        private void Save(TemplateVO template)
        {
            template.Name = Name;
            template.Name = Name;
            template.CSS = CSS;
            template.FileName = FileName;
            template.FileName2 = FileName2;
            template.StartDate = StartDate;
            template.EndDate = string.IsNullOrWhiteSpace(EndDate) ? StartDate : EndDate;                   

            if (template.TemplateId == 0)
            {
                template.Flag = 1;
                template.TemplateType = TemplateType;
                m_TemplateService.CreateTemplate(template);
            }
            else
            {
                m_TemplateService.UpdateTemplate(template);
            }

            LoadTemplate(template.TemplateId);
        }
    }
}

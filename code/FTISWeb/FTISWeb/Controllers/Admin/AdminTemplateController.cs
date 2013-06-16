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
    public partial class TemplateController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static ITemplateService m_FTISService = m_FTISFactory.GetTemplateService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Season, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.Season)]
        public ActionResult AdminIndex(string templateType)
        {
            SetConditions(templateType);
            return View();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Season, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Season)]
        public ActionResult Edit(int id, string templateType)
        {
            SetConditions(templateType);
            return View("Save", new TemplateModel(id));
        }

        [AuthorizationData(AppFunction = SiteEntities.Season)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Season, Operation = SiteOperations.Edit)]
        [HttpPost]
        public ActionResult Edit(TemplateModel model, string templateType)
        {
            SetConditions(templateType);
            model.Update();
            return View("AdminIndex");
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Season, Operation = SiteOperations.Create)]
        [AuthorizationData(AppFunction = SiteEntities.Season)]
        public ActionResult Create(string id, string templateType)
        {
            SetConditions(templateType);
            return View("Save", new TemplateModel(templateType));
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Season, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Season)]
        public ActionResult Delete(int id)
        {
            AjaxResult result = new AjaxResult();

            try
            {
                TemplateVO entity = m_FTISService.GetTemplateById(Convert.ToInt32(id));
                entity.Flag = 0;

                m_FTISService.UpdateTemplate(entity);

                result.ErrorCode = AjaxResultStatus.Success;
                result.Message = string.Format("{0}刪除成功", entity.Name);
            }
            catch (Exception ex)
            {
                result.ErrorCode = AjaxResultStatus.Exception;
                result.Message = ex.Message;
            }

            return this.Json(result);
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Season, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Season)]
        public ActionResult MultiDelete(string allId)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            string[] ids = allId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                try
                {
                    TemplateVO entity = m_FTISService.GetTemplateById(Convert.ToInt32(id));
                    entity.Flag = 0;

                    m_FTISService.UpdateTemplate(entity);
                }
                catch (Exception ex)
                {
                    result.ErrorCode = AjaxResultStatus.Fail;
                    sbMsg.AppendFormat(ex.Message + "<br/>");
                }
            }

            result.Message = sbMsg.ToString();
            return this.Json(result);
        }

        [AuthorizationData(AppFunction = SiteEntities.Season)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Season, Operation = SiteOperations.Create)]
        [HttpPost]
        public ActionResult Create(TemplateModel model, string templateType)
        {
            SetConditions(templateType);
            model.Insert();
            return View("AdminIndex");
        }

        private void SetConditions(string templateType)
        {
            ViewData["TemplateType"] = templateType;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Season, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string templateType)
        {
            var data = GetGridData(templateType);
            var result = new KendoGrid<TemplateVO>(request, data, data.Count());
            return Json(result);
        }

        private IEnumerable<TemplateVO> GetGridData(string templateType)
        {
            IList<TemplateVO> datasource = m_FTISService.GetTemplateList((TemplateVO.Type)Enum.Parse(typeof(TemplateVO.Type), templateType));
            return datasource;
        }
    }
}

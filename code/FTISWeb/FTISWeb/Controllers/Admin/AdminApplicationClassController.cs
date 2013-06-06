﻿using System;
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
    public partial class ApplicationClassController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.Application)]
        public ActionResult AdminIndex(string cdts)
        {
            GetConditions(cdts);
            return View();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Application)]
        public ActionResult Edit(int id, string cdts)
        {
            GetConditions(cdts);
            return View("Save", new ApplicationClassModel(id));
        }

        [AuthorizationData(AppFunction = SiteEntities.Application)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Edit)]
        [HttpPost]
        public ActionResult Edit(ApplicationClassModel model, string cdts)
        {
            GetConditions(cdts);
            model.Update();
            return View("AdminIndex");
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Create)]
        [AuthorizationData(AppFunction = SiteEntities.Application)]
        public ActionResult Create(string cdts)
        {
            GetConditions(cdts);
            return View("Save", new ApplicationClassModel());
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Application)]
        public ActionResult Delete(int id)
        {
            AjaxResult result = new AjaxResult();

            try
            {
                ApplicationClass entity = m_FTISService.GetApplicationClassById(id);

                //檢查底下的Application數量
                IDictionary<string, string> conditions = new Dictionary<string, string>();
                conditions.Add("ApplicationClassId", id.ToString());
                int subsCount = m_FTISService.GetApplicationCount(conditions);
                if (subsCount > 0)
                {
                    return this.Json(new AjaxResult(AjaxResultStatus.Fail, string.Format("{0}底下尚有企業社會責任資訊，不可刪除。", entity.Name)));
                }

                m_FTISService.DeleteApplicationClass(entity);

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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Application)]
        public ActionResult MultiDelete(string allId)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            string[] ids = allId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                try
                {
                    ApplicationClass entity = m_FTISService.GetApplicationClassById(Convert.ToInt32(id));

                    //檢查底下的Application數量
                    IDictionary<string, string> conditions = new Dictionary<string, string>();
                    conditions.Add("ApplicationClassId", id.ToString());
                    int subsCount = m_FTISService.GetApplicationCount(conditions);
                    if (subsCount == 0)
                    {
                        m_FTISService.DeleteApplicationClass(entity);
                    }
                    else
                    {
                        result.ErrorCode = AjaxResultStatus.Fail;
                        sbMsg.AppendFormat("{0}，底下尚有企業社會責任資訊，不可刪除。<br/>", entity.Name);
                    }
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

        [AuthorizationData(AppFunction = SiteEntities.Application)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Create)]
        [HttpPost]
        public ActionResult Create(ApplicationClassModel model, string cdts)
        {
            GetConditions(cdts);
            model.Insert();
            return View("AdminIndex");
        }

        private void GetConditions(string cdts)
        {
            if (string.IsNullOrWhiteSpace(cdts))
            {
                ViewData["Conditions"] = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = string.Empty });
            }
            else
            {
                ViewData["Conditions"] = cdts;
            }
        }

        private void SetConditions(string keyWord)
        {
            string cdts = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = keyWord });
            m_Conditions = m_JsonConvert.Deserialize<IDictionary<string, string>>(cdts);

            ViewData["Conditions"] = cdts;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string keyWord)
        {
            SetConditions(keyWord);
            int total = GetGridTotal();
            int pageIndex = (request.Page - 1);
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", request.PageSize.ToString());
            AppendSortingCondition(request);

            var data = GetGridData();
            var result = new KendoGrid<ApplicationClass>(request, data, total);
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.Application)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Read)]
        public ActionResult RefreshAdminGrid(string keyWord)
        {
            SetConditions(keyWord);
            return View("AdminGridList", new ParamaterModel("Edit", "ApplicationClass", (string)ViewData["Conditions"]));
        }

        private void AppendSortingCondition(KendoGridRequest request)
        {
            if (request.SortObjects.Count() == 1)
            {
                SortObject sortObject = request.SortObjects.FirstOrDefault();
                string field = sortObject.Field.Replace("GetStr_", "");
                string direction = sortObject.Direction;

                m_Conditions.Add("Order", string.Format("order by a.{0} {1}", field, direction));
            }
        }

        private int GetGridTotal()
        {
            int total = m_FTISService.GetApplicationClassCount(m_Conditions);
            return total;
        }

        private IEnumerable<ApplicationClass> GetGridData()
        {
            IList<ApplicationClass> datasource = m_FTISService.GetApplicationClassList(m_Conditions);
            return datasource;
        }
    }
}

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
    public partial class GreenFactoryController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.GreenFactory, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.GreenFactory)]
        [GreenFactoryClassData]
        public ActionResult AdminIndex(string cdts)
        {
            GetConditions(cdts);
            return View();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.GreenFactory, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.GreenFactory)]
        [GreenFactoryClassData]
        public ActionResult Edit(int id, string cdts)
        {
            GetConditions(cdts);
            return View("Save", new GreenFactoryModel(id));
        }

        [ValidateInput(false)]
        [AuthorizationData(AppFunction = SiteEntities.GreenFactory)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.GreenFactory, Operation = SiteOperations.Edit)]
        [HttpPost]
        [GreenFactoryClassData]
        public ActionResult Edit(GreenFactoryModel model, string cdts)
        {
            GetConditions(cdts);

            ////檢查內容無障礙是否通過
            if (!AccessibilityUtil.CheckFreeGO(model.Content))
            {
                model.ShowFreeGOMsg = true;
                model.FreeGOColumnName = "Content";
            }

            model.Update();

            if (model.ShowFreeGOMsg)
            {
                return View("Save", model);
            }

            return View("AdminIndex");
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.GreenFactory, Operation = SiteOperations.Create)]
        [AuthorizationData(AppFunction = SiteEntities.GreenFactory)]
        [GreenFactoryClassData]
        public ActionResult Create(string cdts)
        {
            GetConditions(cdts);
            return View("Save", new GreenFactoryModel());
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.GreenFactory, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.GreenFactory)]
        public ActionResult Delete(int id)
        {
            AjaxResult result = new AjaxResult();

            try
            {
                GreenFactory entity = m_FTISService.GetGreenFactoryById(id);

                m_FTISService.DeleteGreenFactory(entity);

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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.GreenFactory, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.GreenFactory)]
        public ActionResult MultiDelete(string allId)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            string[] ids = allId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                try
                {
                    GreenFactory entity = m_FTISService.GetGreenFactoryById(Convert.ToInt32(id));
                    m_FTISService.DeleteGreenFactory(entity);
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

        [ValidateInput(false)]
        [AuthorizationData(AppFunction = SiteEntities.GreenFactory)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.GreenFactory, Operation = SiteOperations.Create)]
        [HttpPost]
        [GreenFactoryClassData]
        public ActionResult Create(GreenFactoryModel model, string cdts)
        {
            GetConditions(cdts);

            ////檢查內容無障礙是否通過
            if (!AccessibilityUtil.CheckFreeGO(model.Content))
            {
                model.ShowFreeGOMsg = true;
                model.FreeGOColumnName = "Content";
            }

            model.Insert();

            if (model.ShowFreeGOMsg)
            {
                return View("Save", model);
            }

            return View("AdminIndex");
        }

        private void GetConditions(string cdts)
        {
            if (string.IsNullOrWhiteSpace(cdts))
            {
                ViewData["Conditions"] = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = string.Empty, GreenFactoryClassId = string.Empty });
            }
            else
            {
                ViewData["Conditions"] = cdts;
            }
        }

        private void SetConditions(string keyWord, string greenFactoryClassId)
        {
            string cdts = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = keyWord, GreenFactoryClassId = greenFactoryClassId });
            m_Conditions = m_JsonConvert.Deserialize<IDictionary<string, string>>(cdts);

            ViewData["Conditions"] = cdts;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.GreenFactory, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string keyWord, string greenFactoryClassId)
        {
            SetConditions(keyWord, greenFactoryClassId);
            int total = GetGridTotal();
            int pageIndex = (request.Page - 1);
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", request.PageSize.ToString());
            AppendSortingCondition(request);

            var data = GetGridData();
            var result = new KendoGrid<GreenFactory>(request, data, total);
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.GreenFactory)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.GreenFactory, Operation = SiteOperations.Read)]
        public ActionResult RefreshAdminGrid(string keyWord, string greenFactoryClassId)
        {
            SetConditions(keyWord, greenFactoryClassId);
            return View("AdminGridList", new ParamaterModel("Edit", "GreenFactory", (string)ViewData["Conditions"]));
        }

        private void AppendSortingCondition(KendoGridRequest request)
        {
            if (request.SortObjects.Count() == 1)
            {
                SortObject sortObject = request.SortObjects.FirstOrDefault();
                string field = sortObject.Field.Replace("GetStr_", "");
                string direction = sortObject.Direction;

                m_Conditions.Add("Order", string.Format("order by g.{0} {1}", field, direction));
            }
        }

        private int GetGridTotal()
        {
            int total = m_FTISService.GetGreenFactoryCount(m_Conditions);
            return total;
        }

        private IEnumerable<GreenFactory> GetGridData()
        {
            IList<GreenFactory> datasource = m_FTISService.GetGreenFactoryListNoLazy(m_Conditions);
            return datasource;
        }
    }
}

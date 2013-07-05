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
    public partial class EpaperEmailController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Epaper, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.Epaper)]
        public ActionResult AdminIndex(string cdts)
        {
            GetConditions(cdts);
            return View();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Epaper, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Epaper)]
        public ActionResult Edit(int id, string cdts)
        {
            GetConditions(cdts);
            return View("Save", new EpaperEmailModel(id));
        }

        [ValidateInput(false)]
        [AuthorizationData(AppFunction = SiteEntities.Epaper)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Epaper, Operation = SiteOperations.Edit)]
        [HttpPost]
        public ActionResult Edit(EpaperEmailModel model, string cdts)
        {
            GetConditions(cdts);
            model.Update();
            return View("AdminIndex");
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Epaper, Operation = SiteOperations.Create)]
        [AuthorizationData(AppFunction = SiteEntities.Epaper)]
        public ActionResult Create(string cdts)
        {
            GetConditions(cdts);
            return View("Save", new EpaperEmailModel());
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Epaper, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Epaper)]
        public ActionResult Delete(int id)
        {
            AjaxResult result = new AjaxResult();

            try
            {
                EpaperEmail entity = m_FTISService.GetEpaperEmailById(id);

                m_FTISService.DeleteEpaperEmail(entity);

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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Epaper, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Epaper)]
        public ActionResult MultiDelete(string allId)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            string[] ids = allId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                try
                {
                    EpaperEmail entity = m_FTISService.GetEpaperEmailById(Convert.ToInt32(id));
                    m_FTISService.DeleteEpaperEmail(entity);
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
        [AuthorizationData(AppFunction = SiteEntities.Epaper)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Epaper, Operation = SiteOperations.Create)]
        [HttpPost]
        public ActionResult Create(EpaperEmailModel model, string cdts)
        {
            GetConditions(cdts);
            model.Insert();
            return View("AdminIndex");
        }

        private void GetConditions(string cdts)
        {
            if (string.IsNullOrWhiteSpace(cdts))
            {
                ViewData["Conditions"] = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = string.Empty, Status = string.Empty });
            }
            else
            {
                ViewData["Conditions"] = cdts;
            }
        }

        private void SetConditions(string keyWord, string status)
        {
            string cdts = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = keyWord, Status = status });
            m_Conditions = m_JsonConvert.Deserialize<IDictionary<string, string>>(cdts);

            ViewData["Conditions"] = cdts;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Epaper, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string keyWord, string status)
        {
            SetConditions(keyWord, status);
            int total = GetGridTotal();
            int pageIndex = (request.Page - 1);
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", request.PageSize.ToString());
            AppendSortingCondition(request);

            var data = GetGridData();
            var result = new KendoGrid<EpaperEmail>(request, data, total);
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.Epaper)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Epaper, Operation = SiteOperations.Read)]
        public ActionResult RefreshAdminGrid(string keyWord, string status)
        {
            SetConditions(keyWord, status);
            return View("AdminGridList", new ParamaterModel("Edit", "EpaperEmail", (string)ViewData["Conditions"]));
        }

        /// <summary>
        /// 匯出
        /// </summary>        
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Epaper, Operation = SiteOperations.Read)]
        public ActionResult Export(string keyWord, string status)
        {
            SetConditions(keyWord, status);
            return View("Export", GetGridData());
        }

        [HttpPost]
        public ActionResult ExportExcel(FormCollection form)
        {

            string strHtml = form["exportHtml"];
            strHtml = HttpUtility.HtmlDecode(strHtml);//Html解碼
            byte[] b = System.Text.Encoding.Default.GetBytes(strHtml);//字串轉byte陣列
            return File(b, "application/vnd.ms-excel", "電子報訂閱.xls");//輸出檔案給Client端
        }

        private void AppendSortingCondition(KendoGridRequest request)
        {
            if (request.SortObjects.Count() == 1)
            {
                SortObject sortObject = request.SortObjects.FirstOrDefault();
                string field = sortObject.Field.Replace("GetStr_", "");
                string direction = sortObject.Direction;

                m_Conditions.Add("Order", string.Format("order by e.{0} {1}", field, direction));
            }
        }

        private int GetGridTotal()
        {
            int total = m_FTISService.GetEpaperEmailCount(m_Conditions);
            return total;
        }

        private IEnumerable<EpaperEmail> GetGridData()
        {
            IList<EpaperEmail> datasource = m_FTISService.GetEpaperEmailList(m_Conditions);
            return datasource;
        }
    }
}

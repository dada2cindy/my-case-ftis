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
    public partial class EpaperExaminationController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Examination, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.Examination)]
        [IndustryData]
        public ActionResult AdminIndex(string cdts, string page)
        {
            GetConditions(cdts);
            SetPage(page);
            return View();
        }

        private void SetPage(string page)
        {
            int currentPage = 1;
            int.TryParse(page, out currentPage);
            ViewData["CurrentPage"] = currentPage;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Examination, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Examination)]
        [IndustryData]
        public ActionResult Edit(int id, string cdts, int page)
        {
            GetConditions(cdts);
            return View("Save", new EpaperExaminationModel(id) { Page = page });
        }

        [ValidateInput(false)]
        [AuthorizationData(AppFunction = SiteEntities.Examination)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Examination, Operation = SiteOperations.Edit)]
        [HttpPost]
        [IndustryData]
        public ActionResult Edit(EpaperExaminationModel model, string cdts)
        {
            GetConditions(cdts);
            model.Update();
            return RedirectToAction("AdminIndex", new { Page = model.Page, Cdts = cdts });
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Examination, Operation = SiteOperations.Create)]
        [AuthorizationData(AppFunction = SiteEntities.Examination)]
        [IndustryData]
        public ActionResult Create(string cdts)
        {
            GetConditions(cdts);
            return View("Save", new EpaperExaminationModel());
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Examination, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Examination)]
        public ActionResult Delete(int id)
        {
            AjaxResult result = new AjaxResult();

            try
            {
                EpaperExamination entity = m_FTISService.GetEpaperExaminationById(id);

                m_FTISService.DeleteEpaperExamination(entity);

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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Examination, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Examination)]
        public ActionResult MultiDelete(string allId)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            string[] ids = allId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                try
                {
                    EpaperExamination entity = m_FTISService.GetEpaperExaminationById(Convert.ToInt32(id));
                    m_FTISService.DeleteEpaperExamination(entity);
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
        [AuthorizationData(AppFunction = SiteEntities.Examination)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Examination, Operation = SiteOperations.Create)]
        [HttpPost]
        [IndustryData]
        public ActionResult Create(EpaperExaminationModel model, string cdts)
        {
            GetConditions(cdts);
            model.Insert();
            return View("AdminIndex");
        }

        private void GetConditions(string cdts)
        {
            if (string.IsNullOrWhiteSpace(cdts))
            {
                ViewData["Conditions"] = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = string.Empty, IndustryId = string.Empty });
            }
            else
            {
                ViewData["Conditions"] = cdts;
            }
        }

        private void SetConditions(string keyWord, string industryId)
        {
            string cdts = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = keyWord, IndustryId = industryId });
            m_Conditions = m_JsonConvert.Deserialize<IDictionary<string, string>>(cdts);

            ViewData["Conditions"] = cdts;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Examination, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string keyWord, string industryId)
        {
            SetConditions(keyWord, industryId);
            int total = GetGridTotal();
            int pageIndex = (request.Page - 1);
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", request.PageSize.ToString());
            AppendSortingCondition(request);

            var data = GetGridData();
            var result = new KendoGrid<EpaperExamination>(request, data, total);
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.Examination)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Examination, Operation = SiteOperations.Read)]
        public ActionResult RefreshAdminGrid(string keyWord, string industryId)
        {
            SetConditions(keyWord, industryId);
            return View("AdminGridList", new ParamaterModel("Edit", "EpaperExamination", (string)ViewData["Conditions"]));
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
            int total = m_FTISService.GetEpaperExaminationCount(m_Conditions);
            return total;
        }

        private IEnumerable<EpaperExamination> GetGridData()
        {
            IList<EpaperExamination> datasource = m_FTISService.GetEpaperExaminationListNoLazy(m_Conditions);
            return datasource;
        }
    }
}

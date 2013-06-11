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
    public partial class ReportController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.Application)]
        [ReportItemData]
        public ActionResult AdminIndex(string cdts)
        {
            GetConditions(cdts);
            return View();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Application)]
        [ReportItemData]
        public ActionResult Edit(int id, string cdts)
        {
            GetConditions(cdts);
            return View("Save", new ReportModel(id));
        }

        [ValidateInput(false)]
        [AuthorizationData(AppFunction = SiteEntities.Application)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Edit)]
        [HttpPost]
        [ReportItemData]
        public ActionResult Edit(ReportModel model, string cdts)
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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Create)]
        [AuthorizationData(AppFunction = SiteEntities.Application)]
        [ReportItemData]
        public ActionResult Create(string cdts)
        {
            GetConditions(cdts);
            return View("Save", new ReportModel());
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Application)]
        public ActionResult Delete(int id)
        {
            AjaxResult result = new AjaxResult();

            try
            {
                Report entity = m_FTISService.GetReportById(id);

                m_FTISService.DeleteReport(entity);

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
                    Report entity = m_FTISService.GetReportById(Convert.ToInt32(id));
                    m_FTISService.DeleteReport(entity);
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
        [AuthorizationData(AppFunction = SiteEntities.Application)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Create)]
        [HttpPost]
        [ReportItemData]
        public ActionResult Create(ReportModel model, string cdts)
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
                ViewData["Conditions"] = ScriptSerializationUtility.GetSerializedQueryConditions(
                    new
                    {
                        KeyWord = string.Empty,
                        Company = string.Empty,
                        CompanyTrade = string.Empty,
                        CompanyNationality = string.Empty,
                        CompanyScale = string.Empty,
                        CompanyType = string.Empty,
                        IsAA1000 = string.Empty,
                        IsGRI = string.Empty,
                        PostYearFrom = string.Empty,
                        PostYearTo = string.Empty,
                        ReportYearFrom = string.Empty,
                        ReportYearTo = string.Empty
                    });
            }
            else
            {
                ViewData["Conditions"] = cdts;
            }
        }

        private void SetConditions(
            string keyWord, string company, string companyTrade, string companyNationality, string companyScale,
            string companyType, string isAA1000, string isGRI, string postYearFrom, string postYearTo,
            string reportYearFrom, string reportYearTo)
        {
            string cdts = ScriptSerializationUtility.GetSerializedQueryConditions(
                new
                {
                    KeyWord = keyWord,
                    Company = company,
                    CompanyTrade = companyTrade,
                    CompanyNationality = companyNationality,
                    CompanyScale = companyScale,
                    CompanyType = companyType,
                    IsAA1000 = isAA1000,
                    IsGRI = isGRI,
                    PostYearFrom = postYearFrom,
                    PostYearTo = postYearTo,
                    ReportYearFrom = reportYearFrom,
                    ReportYearTo = reportYearTo
                });
            m_Conditions = m_JsonConvert.Deserialize<IDictionary<string, string>>(cdts);

            ViewData["Conditions"] = cdts;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string keyWord, string company, string companyTrade,
            string companyNationality, string companyScale, string companyType, string isAA1000, string isGRI,
            string postYearFrom, string postYearTo, string reportYearFrom, string reportYearTo)
        {
            SetConditions(keyWord, company, companyTrade, companyNationality, companyScale, companyType, isAA1000,
                isGRI, postYearFrom, postYearTo, reportYearFrom, reportYearTo);
            int total = GetGridTotal();
            int pageIndex = (request.Page - 1);
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", request.PageSize.ToString());
            AppendSortingCondition(request);

            var data = GetGridData();
            var result = new KendoGrid<Report>(request, data, total);
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.Application)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Read)]
        public ActionResult RefreshAdminGrid(string keyWord, string company, string companyTrade,
            string companyNationality, string companyScale, string companyType, string isAA1000, string isGRI,
            string postYearFrom, string postYearTo, string reportYearFrom, string reportYearTo)
        {
            SetConditions(keyWord, company, companyTrade, companyNationality, companyScale, companyType, isAA1000,
                isGRI, postYearFrom, postYearTo, reportYearFrom, reportYearTo);
            return View("AdminGridList", new ParamaterModel("Edit", "Report", (string)ViewData["Conditions"]));
        }

        private void AppendSortingCondition(KendoGridRequest request)
        {
            if (request.SortObjects.Count() == 1)
            {
                SortObject sortObject = request.SortObjects.FirstOrDefault();
                string field = sortObject.Field.Replace("GetStr_", "");
                string direction = sortObject.Direction;

                m_Conditions.Add("Order", string.Format("order by r.{0} {1}", field, direction));
            }
        }

        private int GetGridTotal()
        {
            int total = m_FTISService.GetReportCount(m_Conditions);
            return total;
        }

        private IEnumerable<Report> GetGridData()
        {
            IList<Report> datasource = m_FTISService.GetReportList(m_Conditions);
            return datasource;
        }
    }
}

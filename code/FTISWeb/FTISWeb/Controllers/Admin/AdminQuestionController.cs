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
    public partial class QuestionController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Question, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.Question)]
        [QuestionClassData]
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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Question, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Question)]
        [QuestionClassData]
        public ActionResult Edit(int id, string cdts, int page)
        {
            GetConditions(cdts);
            return View("Save", new QuestionModel(id) { Page = page });
        }

        [ValidateInput(false)]
        [AuthorizationData(AppFunction = SiteEntities.Question)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Question, Operation = SiteOperations.Edit)]
        [HttpPost]
        [QuestionClassData]
        public ActionResult Edit(QuestionModel model, string cdts)
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

            return RedirectToAction("AdminIndex", new { Page = model.Page, Cdts = cdts });
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Question, Operation = SiteOperations.Create)]
        [AuthorizationData(AppFunction = SiteEntities.Question)]
        [QuestionClassData]
        public ActionResult Create(string cdts)
        {
            GetConditions(cdts);
            return View("Save", new QuestionModel());
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Question, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Question)]
        public ActionResult Delete(int id)
        {
            AjaxResult result = new AjaxResult();

            try
            {
                Question entity = m_FTISService.GetQuestionById(id);

                m_FTISService.DeleteQuestion(entity);

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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Question, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Question)]
        public ActionResult SetSort(string entityId, string sortValue)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            try
            {
                Question entity = m_FTISService.GetQuestionById(Convert.ToInt32(entityId));
                entity.SortId = int.Parse(sortValue);
                m_FTISService.UpdateQuestion(entity);
            }
            catch (Exception ex)
            {
                result.ErrorCode = AjaxResultStatus.Fail;
                sbMsg.AppendFormat(ex.Message + "<br/>");
            }

            result.Message = sbMsg.ToString();
            return this.Json(result);
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Question, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Question)]
        public ActionResult MultiDelete(string allId)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            string[] ids = allId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                try
                {
                    Question entity = m_FTISService.GetQuestionById(Convert.ToInt32(id));
                    m_FTISService.DeleteQuestion(entity);
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
        [AuthorizationData(AppFunction = SiteEntities.Question)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Question, Operation = SiteOperations.Create)]
        [HttpPost]
        [QuestionClassData]
        public ActionResult Create(QuestionModel model, string cdts)
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
                ViewData["Conditions"] = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = string.Empty, QuestionClassId = string.Empty });
            }
            else
            {
                ViewData["Conditions"] = cdts;
            }
        }

        private void SetConditions(string keyWord, string questionClassId)
        {
            string cdts = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = keyWord, QuestionClassId = questionClassId });
            m_Conditions = m_JsonConvert.Deserialize<IDictionary<string, string>>(cdts);

            ViewData["Conditions"] = cdts;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Question, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string keyWord, string questionClassId)
        {
            SetConditions(keyWord, questionClassId);
            int total = GetGridTotal();
            int pageIndex = (request.Page - 1);
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", request.PageSize.ToString());
            AppendSortingCondition(request);

            var data = GetGridData();
            var result = new KendoGrid<Question>(request, data, total);
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.Question)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Question, Operation = SiteOperations.Read)]
        public ActionResult RefreshAdminGrid(string keyWord, string questionClassId)
        {
            SetConditions(keyWord, questionClassId);
            return View("AdminGridList", new ParamaterModel("Edit", "Question", (string)ViewData["Conditions"]));
        }

        private void AppendSortingCondition(KendoGridRequest request)
        {
            if (request.SortObjects.Count() == 1)
            {
                SortObject sortObject = request.SortObjects.FirstOrDefault();
                string field = sortObject.Field.Replace("GetStr_", "");
                string direction = sortObject.Direction;

                m_Conditions.Add("Order", string.Format("order by q.{0} {1}", field, direction));
            }
        }

        private int GetGridTotal()
        {
            int total = m_FTISService.GetQuestionCount(m_Conditions);
            return total;
        }

        private IEnumerable<Question> GetGridData()
        {
            IList<Question> datasource = m_FTISService.GetQuestionListNoLazy(m_Conditions);
            return datasource;
        }
    }
}

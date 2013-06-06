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
    public partial class NewsController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.News)]
        [NewsClassData]
        public ActionResult AdminIndex(string cdts)
        {
            GetConditions(cdts);
            return View();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.News)]
        [NewsClassData]
        public ActionResult Edit(int id, string cdts)
        {
            GetConditions(cdts);
            return View("Save", new NewsModel(id));
        }

        [ValidateInput(false)]
        [AuthorizationData(AppFunction = SiteEntities.News)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Edit)]
        [HttpPost]
        [NewsClassData]
        public ActionResult Edit(NewsModel model, string cdts)
        {
            GetConditions(cdts);

            ////檢查內容無障礙是否通過
            if (!AccessibilityUtil.CheckFreeGO(model.Content))
            {
                model.ShowFreeGOMsg = true;
                model.FreeGOColumnName = "Content";
            }
            else if (!AccessibilityUtil.CheckFreeGO(model.ContentENG))
            {
                model.ShowFreeGOMsg = true;
                model.FreeGOColumnName = "ContentENG";
            }

            model.Update();

            if (model.ShowFreeGOMsg)
            {
                return View("Save", model);
            }

            return View("AdminIndex");
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Create)]
        [AuthorizationData(AppFunction = SiteEntities.News)]
        [NewsClassData]
        public ActionResult Create(string cdts)
        {
            GetConditions(cdts);
            return View("Save", new NewsModel());
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.News)]
        public ActionResult Delete(int id)
        {
            AjaxResult result = new AjaxResult();

            try
            {
                News entity = m_FTISService.GetNewsById(id);

                m_FTISService.DeleteNews(entity);

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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.News)]
        public ActionResult MultiDelete(string allId)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            string[] ids = allId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                try
                {
                    News entity = m_FTISService.GetNewsById(Convert.ToInt32(id));
                    m_FTISService.DeleteNews(entity);
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
        [AuthorizationData(AppFunction = SiteEntities.News)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Create)]
        [HttpPost]
        [NewsClassData]
        public ActionResult Create(NewsModel model, string cdts)
        {
            GetConditions(cdts);

            ////檢查內容無障礙是否通過
            if (!AccessibilityUtil.CheckFreeGO(model.Content))
            {
                model.ShowFreeGOMsg = true;
                model.FreeGOColumnName = "Content";
            }
            else if (!AccessibilityUtil.CheckFreeGO(model.ContentENG))
            {
                model.ShowFreeGOMsg = true;
                model.FreeGOColumnName = "ContentENG";
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
                ViewData["Conditions"] = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = string.Empty, NewsClassId = string.Empty, NewsTypeId = string.Empty });
            }
            else
            {
                ViewData["Conditions"] = cdts;
            }
        }

        private void SetConditions(string keyWord, string newsClassId, string newsTypeId)
        {
            string cdts = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = keyWord, NewsClassId = newsClassId, NewsTypeId = newsTypeId });
            m_Conditions = m_JsonConvert.Deserialize<IDictionary<string, string>>(cdts);

            ViewData["Conditions"] = cdts;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string keyWord, string newsClassId, string newsTypeId)
        {
            SetConditions(keyWord, newsClassId, newsTypeId);
            int total = GetGridTotal();
            int pageIndex = (request.Page - 1);
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", request.PageSize.ToString());
            AppendSortingCondition(request);

            var data = GetGridData();
            var result = new KendoGrid<News>(request, data, total);
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.News)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Read)]
        public ActionResult RefreshAdminGrid(string keyWord, string newsClassId, string newsTypeId)
        {
            SetConditions(keyWord, newsClassId, newsTypeId);
            return View("AdminGridList", new ParamaterModel("Edit", "News", (string)ViewData["Conditions"]));
        }

        private void AppendSortingCondition(KendoGridRequest request)
        {
            if (request.SortObjects.Count() == 1)
            {
                SortObject sortObject = request.SortObjects.FirstOrDefault();
                string field = sortObject.Field.Replace("GetStr_", "");
                string direction = sortObject.Direction;

                m_Conditions.Add("Order", string.Format("order by n.{0} {1}", field, direction));
            }
        }

        private int GetGridTotal()
        {
            int total = m_FTISService.GetNewsCount(m_Conditions);
            return total;
        }

        private IEnumerable<News> GetGridData()
        {
            IList<News> datasource = m_FTISService.GetNewsListNoLazy(m_Conditions);
            return datasource;
        }
    }
}

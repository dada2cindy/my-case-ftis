using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using FTIS;
using FTIS.Domain;
using FTIS.Domain.Impl;
using FTIS.Service;
using FTISWeb.Models;
using FTISWeb.Security;
using FTISWeb.Utility;
using KendoGridBinder;
using KendoGridBinder.Containers;

namespace FTISWeb.Controllers.Admin
{
    public partial class NewsClassController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.News)]
        public ActionResult AdminIndex(string cdts)
        {
            GetConditions(cdts);
            return View();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.News)]
        public ActionResult Edit(int id, string cdts)
        {
            GetConditions(cdts);
            return View("Save", new NewsClassModel(id));
        }

        [AuthorizationData(AppFunction = SiteEntities.News)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Edit)]
        [HttpPost]
        public ActionResult Edit(NewsClassModel model, string cdts)
        {
            GetConditions(cdts);            
            model.Update();
            return View("AdminIndex");
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Create)]
        [AuthorizationData(AppFunction = SiteEntities.News)]
        public ActionResult Create(string cdts)
        {
            GetConditions(cdts);
            return View("Save", new NewsClassModel());
        }

        [AuthorizationData(AppFunction = SiteEntities.News)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Create)]
        [HttpPost]
        public ActionResult Create(NewsClassModel model, string cdts)
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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string keyWord)
        {
            SetConditions(keyWord);
            int total = GetGridTotal();
            int pageIndex = (request.Page - 1);
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", request.PageSize.ToString());
            AppendSortingCondition(request);

            var data = GetGridData();
            var result = new KendoGrid<NewsClass>(request, data, total);
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.News)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Read)]
        public ActionResult RefreshGrid(string keyWord)
        {
            SetConditions(keyWord);
            return View("AdminIndex");
            //return View("AccountGridList");
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
            int total = m_FTISService.GetNewsClassCount(m_Conditions);
            return total;
        }

        private IEnumerable<NewsClass> GetGridData()
        {
            IList<NewsClass> datasource = m_FTISService.GetNewsClassList(m_Conditions);
            return datasource;
        }
    }
}

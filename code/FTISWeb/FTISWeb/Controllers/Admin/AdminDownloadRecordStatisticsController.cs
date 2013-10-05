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
    public partial class DownloadRecordStatisticsController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Member, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.Member)]
        [DownloadClassData]
        public ActionResult AdminIndex(string cdts)
        {
            GetConditions(cdts);
            return View();
        }

        private void GetConditions(string cdts)
        {
            if (string.IsNullOrWhiteSpace(cdts))
            {
                ViewData["Conditions"] = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = string.Empty, ClassId = string.Empty });
            }
            else
            {
                ViewData["Conditions"] = cdts;
            }
        }

        private void SetConditions(string keyWord, string downloadClassId)
        {
            string cdts = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = keyWord, ClassId = downloadClassId });
            m_Conditions = m_JsonConvert.Deserialize<IDictionary<string, string>>(cdts);

            ViewData["Conditions"] = cdts;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Member, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string keyWord, string downloadClassId)
        {
            SetConditions(keyWord, downloadClassId);

            var data = GetGridData();
            var result = new KendoGrid<DownloadRecord>(request, data.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize), data.Count());
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.Member)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Member, Operation = SiteOperations.Read)]
        public ActionResult RefreshAdminGrid(string keyWord, string downloadClassId)
        {
            SetConditions(keyWord, downloadClassId);
            return View("AdminGridList", new ParamaterModel("Edit", "DownloadRecordStatistics", (string)ViewData["Conditions"]));
        }

        private void AppendSortingCondition(KendoGridRequest request)
        {
            if (request.SortObjects.Count() == 1)
            {
                SortObject sortObject = request.SortObjects.FirstOrDefault();
                string field = sortObject.Field.Replace("GetStr_", "");
                string direction = sortObject.Direction;

                m_Conditions.Add("Order", string.Format("order by d.{0} {1}", field, direction));
            }
        }

        private IEnumerable<DownloadRecord> GetGridData()
        {
            IList<DownloadRecord> datasource = m_FTISService.GetDownloadRecordStatistics(m_Conditions);
            return datasource;
        }
    }
}

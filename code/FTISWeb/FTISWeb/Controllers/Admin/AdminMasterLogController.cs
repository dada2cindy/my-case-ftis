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
    public partial class MasterLogController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Member, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.Member)]
        public ActionResult AdminIndex(string cdts)
        {
            GetConditions(cdts);
            return View();
        }        

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Member, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Member)]
        public ActionResult Delete(int id)
        {
            AjaxResult result = new AjaxResult();

            try
            {
                MasterLog entity = m_FTISService.GetMasterLogById(id);

                m_FTISService.DeleteMasterLog(entity);

                result.ErrorCode = AjaxResultStatus.Success;
                result.Message = string.Format("{0}刪除成功", entity.Account);
            }
            catch (Exception ex)
            {
                result.ErrorCode = AjaxResultStatus.Exception;
                result.Message = ex.Message;
            }

            return this.Json(result);
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Member, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Member)]
        public ActionResult MultiDelete(string allId)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            string[] ids = allId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                try
                {
                    MasterLog entity = m_FTISService.GetMasterLogById(Convert.ToInt32(id));
                    m_FTISService.DeleteMasterLog(entity);
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

        private void GetConditions(string cdts)
        {
            if (string.IsNullOrWhiteSpace(cdts))
            {
                ViewData["Conditions"] = ScriptSerializationUtility.GetSerializedQueryConditions(new { EnterTimeFrom = string.Empty, EnterTimeTo = string.Empty });
            }
            else
            {
                ViewData["Conditions"] = cdts;
            }
        }

        private void SetConditions(string enterTimeFrom, string enterTimeTo)
        {
            string cdts = ScriptSerializationUtility.GetSerializedQueryConditions(new { EnterTimeFrom = enterTimeFrom, EnterTimeTo = enterTimeTo });
            m_Conditions = m_JsonConvert.Deserialize<IDictionary<string, string>>(cdts);

            ViewData["Conditions"] = cdts;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Member, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string enterTimeFrom, string enterTimeTo)
        {
            SetConditions(enterTimeFrom, enterTimeTo);
            int total = GetGridTotal();
            int pageIndex = (request.Page - 1);
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", request.PageSize.ToString());
            AppendSortingCondition(request);

            var data = GetGridData();
            var result = new KendoGrid<MasterLog>(request, data, total);
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.Member)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Member, Operation = SiteOperations.Read)]
        public ActionResult RefreshAdminGrid(string enterTimeFrom, string enterTimeTo)
        {
            SetConditions(enterTimeFrom, enterTimeTo);
            return View("AdminGridList", new ParamaterModel("Edit", "MasterLog", (string)ViewData["Conditions"]));
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
            int total = m_FTISService.GetMasterLogCount(m_Conditions);
            return total;
        }

        private IEnumerable<MasterLog> GetGridData()
        {
            IList<MasterLog> datasource = m_FTISService.GetMasterLogList(m_Conditions);
            return datasource;
        }
    }
}

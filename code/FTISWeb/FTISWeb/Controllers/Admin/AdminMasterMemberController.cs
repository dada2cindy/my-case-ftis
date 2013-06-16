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
    public partial class MasterMemberController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Master, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.Master)]
        [IndustryData]
        public ActionResult AdminIndex(string cdts)
        {
            GetConditions(cdts);
            return View();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Master, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Master)]
        [IndustryData]
        public ActionResult Edit(int id, string cdts)
        {
            GetConditions(cdts);
            return View("Save", new MasterMemberModel(id));
        }

        [ValidateInput(false)]
        [AuthorizationData(AppFunction = SiteEntities.Master)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Master, Operation = SiteOperations.Edit)]
        [HttpPost]
        [IndustryData]
        public ActionResult Edit(MasterMemberModel model, string cdts)
        {
            GetConditions(cdts);
            model.Update();
            return View("AdminIndex");
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Master, Operation = SiteOperations.Create)]
        [AuthorizationData(AppFunction = SiteEntities.Master)]
        [IndustryData]
        public ActionResult Create(string cdts)
        {
            GetConditions(cdts);
            return View("Save", new MasterMemberModel());
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Master, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Master)]
        public ActionResult Delete(int id)
        {
            AjaxResult result = new AjaxResult();

            try
            {
                if (id.Equals(1))
                {
                    throw new Exception("admin不可刪除!");
                }

                MasterMember entity = m_FTISService.GetMasterMemberById(id);
                m_FTISService.DeleteMasterMember(entity);

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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Master, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Master)]
        public ActionResult MultiDelete(string allId)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            string[] ids = allId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                try
                {
                    if ("1".Equals(id))
                    {
                        throw new Exception("admin不可刪除!");
                    }

                    MasterMember entity = m_FTISService.GetMasterMemberById(Convert.ToInt32(id));                      
                    m_FTISService.DeleteMasterMember(entity);
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
        [AuthorizationData(AppFunction = SiteEntities.Master)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Master, Operation = SiteOperations.Create)]
        [HttpPost]
        [IndustryData]
        public ActionResult Create(MasterMemberModel model, string cdts)
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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Master, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string keyWord)
        {
            SetConditions(keyWord);
            int total = GetGridTotal();
            int pageIndex = (request.Page - 1);
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", request.PageSize.ToString());
            AppendSortingCondition(request);

            var data = GetGridData();
            var result = new KendoGrid<MasterMember>(request, data, total);
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.Master)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Master, Operation = SiteOperations.Read)]
        public ActionResult RefreshAdminGrid(string keyWord)
        {
            SetConditions(keyWord);
            return View("AdminGridList", new ParamaterModel("Edit", "MasterMember", (string)ViewData["Conditions"]));
        }

        private void AppendSortingCondition(KendoGridRequest request)
        {
            if (request.SortObjects.Count() == 1)
            {
                SortObject sortObject = request.SortObjects.FirstOrDefault();
                string field = sortObject.Field.Replace("GetStr_", "");
                string direction = sortObject.Direction;

                m_Conditions.Add("Order", string.Format("order by m.{0} {1}", field, direction));
            }
        }

        private int GetGridTotal()
        {
            int total = m_FTISService.GetMasterMemberCount(m_Conditions);
            return total;
        }

        private IEnumerable<MasterMember> GetGridData()
        {
            IList<MasterMember> datasource = m_FTISService.GetMasterMemberListNoLazy(m_Conditions);
            return datasource;
        }
    }
}

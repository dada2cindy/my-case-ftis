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
    public partial class PublicationController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Download, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.Download)]
        [PublicationClassData]
        public ActionResult AdminIndex(string cdts)
        {
            GetConditions(cdts);
            return View();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Download, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Download)]
        [PublicationClassData]
        public ActionResult Edit(int id, string cdts)
        {
            GetConditions(cdts);
            return View("Save", new PublicationModel(id));
        }

        [ValidateInput(false)]
        [AuthorizationData(AppFunction = SiteEntities.Download)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Download, Operation = SiteOperations.Edit)]
        [HttpPost]
        [PublicationClassData]
        public ActionResult Edit(PublicationModel model, string cdts)
        {
            GetConditions(cdts);
            model.Update();
            return View("AdminIndex");
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Download, Operation = SiteOperations.Create)]
        [AuthorizationData(AppFunction = SiteEntities.Download)]
        [PublicationClassData]
        public ActionResult Create(string cdts)
        {
            GetConditions(cdts);
            return View("Save", new PublicationModel());
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Download, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Download)]
        public ActionResult Delete(int id)
        {
            AjaxResult result = new AjaxResult();

            try
            {
                Publication entity = m_FTISService.GetPublicationById(id);

                m_FTISService.DeletePublication(entity);

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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Download, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Download)]
        public ActionResult MultiDelete(string allId)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            string[] ids = allId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                try
                {
                    Publication entity = m_FTISService.GetPublicationById(Convert.ToInt32(id));
                    m_FTISService.DeletePublication(entity);
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
        [AuthorizationData(AppFunction = SiteEntities.Download)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Download, Operation = SiteOperations.Create)]
        [HttpPost]
        [PublicationClassData]
        public ActionResult Create(PublicationModel model, string cdts)
        {
            GetConditions(cdts);
            model.Insert();
            return View("AdminIndex");
        }

        private void GetConditions(string cdts)
        {
            if (string.IsNullOrWhiteSpace(cdts))
            {
                ViewData["Conditions"] = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = string.Empty, PublicationClassId = string.Empty });
            }
            else
            {
                ViewData["Conditions"] = cdts;
            }
        }

        private void SetConditions(string keyWord, string publicationClassId)
        {
            string cdts = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = keyWord, PublicationClassId = publicationClassId });
            m_Conditions = m_JsonConvert.Deserialize<IDictionary<string, string>>(cdts);

            ViewData["Conditions"] = cdts;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Download, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string keyWord, string publicationClassId)
        {
            SetConditions(keyWord, publicationClassId);
            int total = GetGridTotal();
            int pageIndex = (request.Page - 1);
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", request.PageSize.ToString());
            AppendSortingCondition(request);

            var data = GetGridData();
            var result = new KendoGrid<Publication>(request, data, total);
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.Download)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Download, Operation = SiteOperations.Read)]
        public ActionResult RefreshAdminGrid(string keyWord, string publicationClassId)
        {
            SetConditions(keyWord, publicationClassId);
            return View("AdminGridList", new ParamaterModel("Edit", "Publication", (string)ViewData["Conditions"]));
        }

        private void AppendSortingCondition(KendoGridRequest request)
        {
            if (request.SortObjects.Count() == 1)
            {
                SortObject sortObject = request.SortObjects.FirstOrDefault();
                string field = sortObject.Field.Replace("GetStr_", "");
                string direction = sortObject.Direction;

                m_Conditions.Add("Order", string.Format("order by p.{0} {1}", field, direction));
            }
        }

        private int GetGridTotal()
        {
            int total = m_FTISService.GetPublicationCount(m_Conditions);
            return total;
        }

        private IEnumerable<Publication> GetGridData()
        {
            IList<Publication> datasource = m_FTISService.GetPublicationListNoLazy(m_Conditions);
            return datasource;
        }
    }
}

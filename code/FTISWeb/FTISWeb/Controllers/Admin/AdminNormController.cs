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
    public partial class NormController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Norm, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.Norm)]
        [NormClassData]
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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Norm, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Norm)]
        [NormClassData]
        public ActionResult Edit(int id, string cdts, int page)
        {
            GetConditions(cdts);
            return View("Save", new NormModel(id) { Page = page });
        }

        [ValidateInput(false)]
        [AuthorizationData(AppFunction = SiteEntities.Norm)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Norm, Operation = SiteOperations.Edit)]
        [HttpPost]
        [NormClassData]
        public ActionResult Edit(NormModel model, string cdts)
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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Norm, Operation = SiteOperations.Create)]
        [AuthorizationData(AppFunction = SiteEntities.Norm)]
        [NormClassData]
        public ActionResult Create(string cdts)
        {
            GetConditions(cdts);
            return View("Save", new NormModel());
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Norm, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Norm)]
        public ActionResult Delete(int id)
        {
            AjaxResult result = new AjaxResult();

            try
            {
                Norm entity = m_FTISService.GetNormById(id);

                m_FTISService.DeleteNorm(entity);

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

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Norm, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Norm)]
        public ActionResult SetSort(string entityId, string sortValue)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            try
            {
                Norm entity = m_FTISService.GetNormById(Convert.ToInt32(entityId));
                entity.SortId = int.Parse(sortValue);
                m_FTISService.UpdateNorm(entity);
            }
            catch (Exception ex)
            {
                result.ErrorCode = AjaxResultStatus.Fail;
                sbMsg.AppendFormat(ex.Message + "<br/>");
            }

            result.Message = sbMsg.ToString();
            return this.Json(result);
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Norm, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Norm)]
        public ActionResult MultiDelete(string allId)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            string[] ids = allId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                try
                {
                    Norm entity = m_FTISService.GetNormById(Convert.ToInt32(id));
                    m_FTISService.DeleteNorm(entity);
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
        [AuthorizationData(AppFunction = SiteEntities.Norm)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Norm, Operation = SiteOperations.Create)]
        [HttpPost]
        [NormClassData]
        public ActionResult Create(NormModel model, string cdts)
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
                ViewData["Conditions"] = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = string.Empty, NormClassId = string.Empty, NormClassParentId = string.Empty });
            }
            else
            {
                ViewData["Conditions"] = cdts;
            }
        }

        private void SetConditions(string keyWord, string normClassId, string normClassParentId)
        {
            string cdts = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = keyWord, NormClassId = normClassId, NormClassParentId = normClassParentId });
            m_Conditions = m_JsonConvert.Deserialize<IDictionary<string, string>>(cdts);

            ViewData["Conditions"] = cdts;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Norm, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string keyWord, string normClassId, string normClassParentId)
        {
            SetConditions(keyWord, normClassId, normClassParentId);
            int total = GetGridTotal();
            int pageIndex = (request.Page - 1);
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", request.PageSize.ToString());
            AppendSortingCondition(request);

            var data = GetGridData();
            var result = new KendoGrid<Norm>(request, data, total);
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.Norm)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Norm, Operation = SiteOperations.Read)]
        public ActionResult RefreshAdminGrid(string keyWord, string normClassId, string normTypeId)
        {
            SetConditions(keyWord, normClassId, normTypeId);
            return View("AdminGridList", new ParamaterModel("Edit", "Norm", (string)ViewData["Conditions"]));
        }

        [HttpPost]

        public JsonResult GetSubNormList(string parentId, bool onlyOpen = false)
        {
            IList<NormClass> list = new List<NormClass>();
            if (!string.IsNullOrWhiteSpace(parentId))
            {
                list = new NormClassModel().GetNormClassList(onlyOpen, int.Parse(parentId));
            }
            //List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
            //if (!string.IsNullOrWhiteSpace(customerID))
            //{

            //    var orders = this.GetOrders(customerID);

            //    if (orders.Count() > 0)
            //    {

            //        foreach (var order in orders)
            //        {

            //            items.Add(new KeyValuePair<string, string>(

            //                order.OrderID.ToString(),

            //                string.Format("{0} ({1:yyyy-MM-dd})", order.OrderID, order.OrderDate)));

            //        }

            //    }

            //}

            return this.Json(list);

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
            int total = m_FTISService.GetNormCount(m_Conditions);
            return total;
        }

        private IEnumerable<Norm> GetGridData()
        {
            IList<Norm> datasource = m_FTISService.GetNormListNoLazy(m_Conditions);
            return datasource;
        }
    }
}

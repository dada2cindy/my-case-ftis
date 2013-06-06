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
using WuDada.Accessibility.FreeGO;

namespace FTISWeb.Controllers
{
    public partial class GreenController : Controller
    {
        protected static FTISFactory m_FTISFactory = new FTISFactory();
        protected static IFTISService m_FTISService = m_FTISFactory.GetFTISService();

        private readonly JavaScriptSerializer m_JsonConvert = new JavaScriptSerializer();
        private IDictionary<string, string> m_Conditions = new Dictionary<string, string>();

        /// <summary>
        /// NodeId 生態化設計
        /// </summary>
        private static readonly string m_NodeId = "15";

        #region 單一頁面
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Application, ControllerName = "Green")]
        public ActionResult NodeSingleEdit(int id)
        {
            return View("NodeSingleEdit", new NodeModel(id));
        }

        [ValidateInput(false)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Application, ControllerName = "Green")]
        [HttpPost]
        public ActionResult NodeSingleEdit(NodeModel model)
        {
            ////檢查內容無障礙是否通過
            if (!AccessibilityUtil.CheckFreeGO(model.Content))
            {
                model.ShowFreeGOMsg = true;
                model.FreeGOColumnName = "Content";
            }

            model.Update();
            return View(model);
        }
        #endregion

        #region PostList By Node

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Read)]
        [AuthorizationData(AppFunction = SiteEntities.Application, ControllerName = "Green")]
        [PostParentNodeData(NodeId = SiteNode.Green)]
        public ActionResult PostAdminIndex(string cdts)
        {
            GetConditions(cdts);
            return View();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Application, ControllerName = "Green")]
        [PostParentNodeData(NodeId = SiteNode.Green)]
        public ActionResult PostEdit(int id, string cdts)
        {
            GetConditions(cdts);
            return View("PostSave", new PostModel(id));
        }

        [ValidateInput(false)]
        [AuthorizationData(AppFunction = SiteEntities.Application, ControllerName = "Green")]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Edit)]
        [HttpPost]
        [PostParentNodeData(NodeId = SiteNode.Green)]
        public ActionResult PostEdit(PostModel model, string cdts)
        {
            GetConditions(cdts);
            model.Update();
            return View("PostAdminIndex");
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Create)]
        [AuthorizationData(AppFunction = SiteEntities.Application, ControllerName = "Green")]
        [PostParentNodeData(NodeId = SiteNode.Green)]
        public ActionResult PostCreate(string cdts)
        {
            GetConditions(cdts);
            return View("PostSave", new PostModel());
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Delete)]
        [AuthorizationData(AppFunction = SiteEntities.Application, ControllerName = "Green")]
        public ActionResult PostDelete(int id)
        {
            AjaxResult result = new AjaxResult();

            try
            {
                Post entity = m_FTISService.GetPostById(id);

                m_FTISService.DeletePost(entity);

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
        [AuthorizationData(AppFunction = SiteEntities.Application, ControllerName = "Green")]
        public ActionResult PostMultiDelete(string allId)
        {
            AjaxResult result = new AjaxResult(AjaxResultStatus.Success, string.Empty);
            StringBuilder sbMsg = new StringBuilder();

            string[] ids = allId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                try
                {
                    Post entity = m_FTISService.GetPostById(Convert.ToInt32(id));
                    m_FTISService.DeletePost(entity);
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
        [AuthorizationData(AppFunction = SiteEntities.Application, ControllerName = "Green")]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Create)]
        [HttpPost]
        [PostParentNodeData(NodeId = SiteNode.Green)]
        public ActionResult PostCreate(PostModel model, string cdts)
        {
            GetConditions(cdts);
            model.Insert();
            return View("PostAdminIndex");
        }

        private void GetConditions(string cdts)
        {
            if (string.IsNullOrWhiteSpace(cdts))
            {
                ViewData["Conditions"] = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = string.Empty, NodeId = m_NodeId });
            }
            else
            {
                ViewData["Conditions"] = cdts;
            }
        }

        private void SetConditions(string keyWord, string nodeId)
        {
            string cdts = ScriptSerializationUtility.GetSerializedQueryConditions(new { KeyWord = keyWord, NodeId = nodeId });
            m_Conditions = m_JsonConvert.Deserialize<IDictionary<string, string>>(cdts);

            ViewData["Conditions"] = cdts;
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Read)]
        public JsonResult AjaxBinding(KendoGridRequest request, string keyWord, string nodeId)
        {
            SetConditions(keyWord, nodeId);
            int total = GetGridTotal();
            int pageIndex = (request.Page - 1);
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", request.PageSize.ToString());
            AppendSortingCondition(request);

            var data = GetGridData();
            var result = new KendoGrid<Post>(request, data, total);
            return Json(result);
        }

        /// <summary>
        /// 重撈Grid 資料
        /// </summary>
        [AuthorizationData(AppFunction = SiteEntities.Application, ControllerName = "Green")]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Application, Operation = SiteOperations.Read)]
        public ActionResult RefreshAdminGrid(string keyWord, string nodeId)
        {
            SetConditions(keyWord, nodeId);
            return View("PostAdminGridList", new ParamaterModel("PostEdit", "Green", (string)ViewData["Conditions"]) { EntityId = "PostId", EntityName = "Name" });
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
            else
            {
                m_Conditions.Add("Order", "order by p.ExpiredDate desc");
            }
        }

        private int GetGridTotal()
        {
            int total = m_FTISService.GetPostCount(m_Conditions);
            return total;
        }

        private IEnumerable<Post> GetGridData()
        {
            IList<Post> datasource = m_FTISService.GetPostListNoLazy(m_Conditions);
            return datasource;
        }

        #endregion
    }
}

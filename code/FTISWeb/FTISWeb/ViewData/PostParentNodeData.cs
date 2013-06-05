using System.Collections.Generic;
using System.Web.Mvc;
using FTIS;
using FTIS.ACUtility;
using FTIS.Domain;
using FTIS.Domain.Impl;
using FTIS.Service;
using FTISWeb.Helper;

namespace FTISWeb.Controllers
{
    public class PostParentNodeData : ActionFilterAttribute
    {
        /// <summary>
        /// 僅Status = 1 (開啟)
        /// </summary>
        public bool OnlyOpen { get; set; }

        /// <summary>
        /// ParentNodeId
        /// </summary>
        public SiteParentNode ParentNodeId { get; set; }

        /// <summary>
        /// NodeId
        /// </summary>
        public SiteNode NodeId { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            FTISFactory m_FTISFactory = new FTISFactory();
            IFTISService m_FTISService = m_FTISFactory.GetFTISService();
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            if (OnlyOpen)
            {
                conditions.Add("Status", "1");
            }
            if ((int)ParentNodeId > 0)
            {
                conditions.Add("ParentNodeId", ((int)ParentNodeId).ToString());

                Node parentNode = m_FTISService.GetNodeById((int)ParentNodeId);
                context.Controller.ViewData["FuncName"] = parentNode.Name + "管理";
                IList<Node> nodeList = m_FTISService.GetNodeList(conditions);
                context.Controller.ViewData["NodeList"] = nodeList;
            }
            else if ((int)NodeId > 0)
            {
                Node node = m_FTISService.GetNodeById((int)NodeId);
                context.Controller.ViewData["FuncName"] = node.Name + "管理";
                context.Controller.ViewData["Node"] = node;
            }
        
        }
    }
}

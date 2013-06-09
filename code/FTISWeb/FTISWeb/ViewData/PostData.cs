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
    public class PostData : ActionFilterAttribute
    {
        /// <summary>
        /// 僅Status = 1 (開啟)
        /// </summary>
        public bool OnlyOpen { get; set; }

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
            if ((int)NodeId > 0)
            {
                conditions.Add("NodeId", ((int)NodeId).ToString());
                IList<Post> postList = m_FTISService.GetPostList(conditions);
                context.Controller.ViewData["PostList"] = postList;
            }
        
        }
    }
}

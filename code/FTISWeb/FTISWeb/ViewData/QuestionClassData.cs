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
    public class QuestionClassData : ActionFilterAttribute
    {
        /// <summary>
        /// 僅Status = 1 (開啟)
        /// </summary>
        public bool OnlyOpen { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            FTISFactory m_FTISFactory = new FTISFactory();
            IFTISService m_FTISService = m_FTISFactory.GetFTISService();
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            if (OnlyOpen)
            {
                conditions.Add("Status", "1");
            }

            IList<QuestionClass> QuestionClassList = m_FTISService.GetQuestionClassList(conditions);
            context.Controller.ViewData["QuestionClassList"] = QuestionClassList;
        }
    }
}

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
    public class ReportItemData : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ////行業別
            IList<SelectListItem> companyTradeList = new List<SelectListItem>()
                {
                    new SelectListItem(){Text= "一般服務業", Value= "1"}
                    ,new SelectListItem(){Text= "技術服務業", Value= "2"}
                    ,new SelectListItem(){Text= "旅遊餐飲業", Value= "3"}
                    ,new SelectListItem(){Text= "批發零售業", Value= "4"}
                    ,new SelectListItem(){Text= "金融保險業", Value= "5"}
                    ,new SelectListItem(){Text= "電子資訊業", Value= "6"}
                    ,new SelectListItem(){Text= "倉儲物流業", Value= "7"}
                    ,new SelectListItem(){Text= "傳統製造業", Value= "8"}
                    ,new SelectListItem(){Text= "傳播出版業", Value= "9"}
                    ,new SelectListItem(){Text= "營建不動產", Value= "10"}
                    ,new SelectListItem(){Text= "公行社福業", Value= "11"}
                    ,new SelectListItem(){Text= "教育學術業", Value= "12"}
                    ,new SelectListItem(){Text= "醫療保健業", Value= "13"}
                    ,new SelectListItem(){Text= "其他行業", Value= "14"}
                };
            context.Controller.ViewData["CompanyTradeList"] = companyTradeList;

            ////國籍
            IList<SelectListItem> companyNationalityList = new List<SelectListItem>()
                {
                    new SelectListItem(){Text= "國內", Value= "1"}
                    ,new SelectListItem(){Text= "國外", Value= "0"}
                };
            context.Controller.ViewData["CompanyNationalityList"] = companyNationalityList;

            ////公司規模
            IList<SelectListItem> companyScaleList = new List<SelectListItem>()
                {
                    new SelectListItem(){Text= "大企業", Value= "1"}
                    ,new SelectListItem(){Text= "中小企業", Value= "0"}
                };
            context.Controller.ViewData["CompanyScaleList"] = companyScaleList;

            ////公司類型
            IList<SelectListItem> companyTypeList = new List<SelectListItem>()
                {
                    new SelectListItem(){Text= "上市公司", Value= "1"}
                    ,new SelectListItem(){Text= "上櫃公司", Value= "2"}
                    ,new SelectListItem(){Text= "興櫃公司", Value= "3"}
                    ,new SelectListItem(){Text= "其它", Value= "4"}
                };
            context.Controller.ViewData["CompanyTypeList"] = companyTypeList;

            ////AA1000標準
            IList<SelectListItem> aa1000List = new List<SelectListItem>()
                {
                    new SelectListItem(){Text= "通過", Value= "1"}
                    ,new SelectListItem(){Text= "無", Value= "2"}
                };
            context.Controller.ViewData["AA1000List"] = aa1000List;

            ////GRI等級
            IList<SelectListItem> griList = new List<SelectListItem>()
                {
                    new SelectListItem(){Text= "A+", Value= "1"}
                    ,new SelectListItem(){Text= "A", Value= "2"}
                    ,new SelectListItem(){Text= "B+", Value= "3"}
                    ,new SelectListItem(){Text= "B", Value= "4"}
                    ,new SelectListItem(){Text= "C+", Value= "5"}
                    ,new SelectListItem(){Text= "C", Value= "6"}
                    ,new SelectListItem(){Text= "無", Value= "7"}
                };
            context.Controller.ViewData["GRIList"] = griList;
        }
    }
}

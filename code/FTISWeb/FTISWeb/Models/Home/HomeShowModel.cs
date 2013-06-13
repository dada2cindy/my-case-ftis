using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using FTIS;
using FTIS.Domain.Impl;
using FTIS.Service;
using FTISWeb.Security;
using FTISWeb.Helper;
using WuDada.Core.Generic.Util;
using System.ComponentModel;
using FTISWeb.Utility;

namespace FTISWeb.Models
{
    public class HomeShowModel : AbstractShowModel
    {
        protected readonly ITemplateService m_TemplateService;

        public HomeShowModel()
        {
            m_TemplateService = m_FTISFactory.GetTemplateService();
        }

        public IList<News> GetNewsList()
        {
            IDictionary<string, string> conditions = GetDefaultConditions();
            conditions.Add("IsHome", "1");
            IList<News> list = m_FTISService.GetNewsList(conditions);
            if (list == null)
            {
                list = new List<News>();
            }

            return list;
        }

        public IList<Activity> GetActivityList()
        {
            IDictionary<string, string> conditions = GetDefaultConditions();
            conditions.Add("IsHome", "1");
            IList<Activity> list = m_FTISService.GetActivityList(conditions);
            if (list == null)
            {
                list = new List<Activity>();
            }

            return list;
        }

        public IList<HomeNews> GetHomeNews()
        {
            IDictionary<string, string> conditions = GetDefaultConditions(5);
            IList<HomeNews> list = m_FTISService.GetHomeNewsList(conditions);
            if (list == null)
            {
                list = new List<HomeNews>();
            }

            return list;
        }

        public IList<Publication> GetPublication()
        {
            IDictionary<string, string> conditions = GetDefaultConditions(1);
            IList<Publication> list = m_FTISService.GetPublicationList(conditions);
            if (list == null)
            {
                list = new List<Publication>();
            }

            return list;
        }

        public TemplateVO GetCurrentTemplate()
        {
            TemplateVO templateVO = m_TemplateService.GetCurrentTemplate(AppSettings.TemplateBeforeDays);
            if (templateVO == null)
            {
                templateVO = new TemplateVO();
            }

            return templateVO;
        }

        public void AddCount(string barId)
        {
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("BarId", barId);

            CountVO count = m_FTISService.GetTodayCount(barId);
            if (count == null)
            {
                count = new CountVO() { BarId = int.Parse(barId), Hits = 1, CountDate = DateTime.Today };
                m_FTISService.CreateCount(count);
            }
            else
            {
                count.Hits += 1;
                m_FTISService.UpdateCount(count);
            }
        }

        public int GetSumCountHits(string barId)
        {
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("BarId", barId);
            int total = m_FTISService.GetSumCountHits(conditions);

            return total;
        }
    }
}

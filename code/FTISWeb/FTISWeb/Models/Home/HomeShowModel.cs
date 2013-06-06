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

namespace FTISWeb.Models
{
    public class HomeShowModel : AbstractShowModel
    {
        public HomeShowModel()
        {
        }

        public IList<News> GetNewsList()
        {
            IDictionary<string, string> conditions = GetDefaultConditions();
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
    }
}

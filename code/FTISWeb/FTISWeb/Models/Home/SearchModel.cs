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
    public class SearchModel : AbstractShowModel
    {

        public SearchModel(string keyWord)
        {
            this.KeyWord = keyWord;
        }

        private readonly int m_PageSize = 5;

        public string KeyWord { get; set; }

        public IList<News> GetNewsList()
        {
            IDictionary<string, string> conditions = GetDefaultConditions(m_PageSize);
            conditions.Add("KeyWord", KeyWord);
            IList<News> list = m_FTISService.GetNewsListNoLazy(conditions);
            if (list == null)
            {
                list = new List<News>();
            }

            return list;
        }

        public int GetNewsCount()
        {
            IDictionary<string, string> conditions = GetDefaultCountConditions();
            conditions.Add("KeyWord", KeyWord);
            return m_FTISService.GetNewsCount(conditions);
        }

        public IList<Activity> GetActivityList()
        {
            IDictionary<string, string> conditions = GetDefaultConditions(m_PageSize);
            conditions.Add("KeyWord", KeyWord);
            IList<Activity> list = m_FTISService.GetActivityList(conditions);
            if (list == null)
            {
                list = new List<Activity>();
            }

            return list;
        }

        public int GetActivityCount()
        {
            IDictionary<string, string> conditions = GetDefaultCountConditions();
            conditions.Add("KeyWord", KeyWord);
            return m_FTISService.GetActivityCount(conditions);
        }

        public IList<Download> GetDownLoadList()
        {
            IDictionary<string, string> conditions = GetDefaultConditions(m_PageSize);
            conditions.Add("KeyWord", KeyWord);
            IList<Download> list = m_FTISService.GetDownloadList(conditions);
            if (list == null)
            {
                list = new List<Download>();
            }

            return list;
        }

        public int GetDownLoadCount()
        {
            IDictionary<string, string> conditions = GetDefaultCountConditions();
            conditions.Add("KeyWord", KeyWord);
            return m_FTISService.GetDownloadCount(conditions);
        }
    }
}

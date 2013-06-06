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
using Microsoft.Security.Application;
using FTISWeb.Utility;

namespace FTISWeb.Models
{
    public abstract class AbstractShowModel
    {
        protected readonly FTISFactory m_FTISFactory = new FTISFactory();
        protected readonly IFTISService m_FTISService;

        public AbstractShowModel()
        {
            m_FTISService = m_FTISFactory.GetFTISService();
        }

        public IDictionary<string, string> GetDefaultConditions(int pagesize = 10)
        {
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("Status", "1");
            conditions.Add("PageIndex", "0");
            conditions.Add("PageSize", pagesize.ToString());

            return conditions;
        }

        public string SubString(string source, int maxLength, bool ellipsis)
        {
            string result = source;
            
            if (!string.IsNullOrWhiteSpace(source) && source.Length > maxLength)
            {
                result = source.Substring(0, maxLength);
                if (ellipsis)
                {
                    result += "...";
                }
            }

            return result;
        }

        public string FilterHtml(string source, int maxLength, bool ellipsis)
        {
            string result = ConvertUtil.FilterHtml(source);

            return SubString(result, maxLength, ellipsis);
        }

        //ConvertUtil

        public string EncryptId(int id)
        {
            return EncryptUtil.EncryptDES(id.ToString(), AppSettings.EncryptKey, AppSettings.EncryptIV);
        }

        public string DecryptId(string id)
        {
            return EncryptUtil.DecryptDES(id, AppSettings.EncryptKey, AppSettings.EncryptIV);
        }

        public string GetFile(string fileName)
        {
            return AppSettings.CKFinderFileHandler + fileName;
        }
    }
}

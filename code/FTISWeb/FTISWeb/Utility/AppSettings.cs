﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FTISWeb.Utility
{
    public static class AppSettings
    {
        //public static string UploadPath
        //{
        //    get
        //    {
        //        return ConfigurationManager.AppSettings["UploadPath"];
        //    }
        //}

        public static string CKFinderFileHandler
        {
            get
            {
                return ConfigurationManager.AppSettings["CKFinderFileHandler"];
            }
        }

        public static string CKFinderFileHandlerByEncrypt
        {
            get
            {
                return ConfigurationManager.AppSettings["CKFinderFileHandlerByEncrypt"];
            }
        }
        
        public static string CKFinderBaseDir
        {
            get
            {
                string baseDir = ConfigurationManager.AppSettings["CKFinderBaseDir"].TrimEnd('/');
                string baseUrl = ConfigurationManager.AppSettings["CKFinderBaseUrl"];
                return baseDir + baseUrl;
            }
        }

        public static string CKFinderBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["CKFinderBaseUrl"];
            }
        }

        public static int AdminPageSize
        {
            get
            {
                return 20;
            }
        }

        public static int InSitePageSize
        {
            get
            {
                return 15;
            }
        }

        public static string EncryptKey
        {
            get
            {
                return ConfigurationManager.AppSettings["EncryptKey"];
            }
        }

        public static string EncryptIV
        {
            get
            {
                return ConfigurationManager.AppSettings["EncryptIV"];
            }
        }

        public static int TemplateBeforeDays
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["TemplateBeforeDays"].ToString());
            }
        }
    }
}
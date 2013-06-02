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

namespace FTISWeb.Models
{
    public abstract class AbstractArticleENGModel : AbstractArticleModel
    {
        public AbstractArticleENGModel()
        {
        }

        /// <summary>
        /// 標題
        /// </summary>
        public string NameENG { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        public string ContentENG { get; set; }

        /// <summary>
        /// 圖片1
        /// </summary>
        public string Pic1ENG { get; set; }

        /// <summary>
        /// 圖片2
        /// </summary>
        public string Pic2ENG { get; set; }

        /// <summary>
        /// 圖片3
        /// </summary>
        public string Pic3ENG { get; set; }

        /// <summary>
        /// 圖片1名稱
        /// </summary>
        public string Pic1NameENG { get; set; }

        /// <summary>
        /// 圖片2名稱
        /// </summary>
        public string Pic2NameENG { get; set; }

        /// <summary>
        /// 圖片3名稱
        /// </summary>
        public string Pic3NameENG { get; set; }

        /// <summary>
        /// 附件檔1
        /// </summary>
        public string AFile1ENG { get; set; }

        /// <summary>
        /// 附件檔2
        /// </summary>
        public string AFile2ENG { get; set; }

        /// <summary>
        /// 附件檔3
        /// </summary>
        public string AFile3ENG { get; set; }

        /// <summary>
        /// 附件檔1名稱
        /// </summary>
        public string AFile1NameENG { get; set; }

        /// <summary>
        /// 附件檔2名稱
        /// </summary>
        public string AFile2NameENG { get; set; }

        /// <summary>
        /// 附件檔3名稱
        /// </summary>
        public string AFile3NameENG { get; set; }

        /// <summary>
        /// 資料來源連結1
        /// </summary>
        public string AUrl1LinkENG { get; set; }

        /// <summary>
        /// 資料來源連結2
        /// </summary>
        public string AUrl2LinkENG { get; set; }

        /// <summary>
        /// 資料來源連結3
        /// </summary>
        public string AUrl3LinkENG { get; set; }

        /// <summary>
        /// 資料來源1名稱
        /// </summary>
        public string AUrl1ENG { get; set; }

        /// <summary>
        /// 資料來源2名稱
        /// </summary>
        public string AUrl2ENG { get; set; }

        /// <summary>
        /// 資料來源3名稱
        /// </summary>
        public string AUrl3ENG { get; set; }

        ///// <summary>
        ///// 首頁顯示. 0.關閉 1.開啟
        ///// </summary>
        //public string IsHomeENG { get; set; }

        ///// <summary>
        ///// 顯示New. 0.關閉 1.開啟
        ///// </summary>
        //public string IsNewENG { get; set; }

        /// <summary>
        /// 瀏覽人數
        /// </summary>
        public int VisterENG { get; set; }

        /// <summary>
        /// 轉寄數
        /// </summary>
        public int EmailerENG { get; set; }

        /// <summary>
        /// 列印數
        /// </summary>
        public int PrinterENG { get; set; }

        ///// <summary>
        ///// 主題分類編號
        ///// </summary>
        //public string MainCodeENG { get; set; }

        ///// <summary>
        ///// 主題分類名稱
        ///// </summary>
        //public string MainNameENG { get; set; }

        ///// <summary>
        ///// 施政分類編號
        ///// </summary>
        //public string AdminCodeENG { get; set; }

        ///// <summary>
        ///// 施政分類名稱
        ///// </summary>
        //public string AdminNameENG { get; set; }

        ///// <summary>
        ///// 服務分類編號
        ///// </summary>
        //public string ServiceCodeENG { get; set; }

        ///// <summary>
        ///// 服務分類名稱
        ///// </summary>
        //public string ServiceNameENG { get; set; }

        ///// <summary>
        ///// Tag 注：和國際環保標準 / 規範介紹相關聯 用,區隔
        ///// </summary>
        //public string TagENG { get; set; }

        ///// <summary>
        ///// 是否站外
        ///// </summary>
        //public string IsOutENG { get; set; }

        /// <summary>
        /// 站外連結
        /// </summary>
        public string AUrlENG { get; set; }
    }
}

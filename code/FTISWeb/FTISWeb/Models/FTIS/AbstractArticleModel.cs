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
    public abstract class AbstractArticleModel : AbstractEntityModel
    {
        public AbstractArticleModel()
        {
            PostDate = DateTime.Today;
            this.IsHome = "0";
            this.IsNew = "0";
            this.IsOut = "0";
        }

        /// <summary>
        /// 標題
        /// </summary>
        [DisplayName("標題")]
        [Required]
        public override string Name { get; set; }

        /// <summary>
        /// 語言. 1.英文 2.中文
        /// </summary>
        public string LangId { get; set; }

        /// <summary>
        /// 刊登日期
        /// </summary>
        [DisplayName("刊登日期")]
        [Required]
        public DateTime? PostDate { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [DisplayName("內容")]
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 圖片1
        /// </summary>
        public  string Pic1 { get; set; }

        /// <summary>
        /// 圖片2
        /// </summary>
        public string Pic2 { get; set; }

        /// <summary>
        /// 圖片3
        /// </summary>
        public string Pic3 { get; set; }

        /// <summary>
        /// 圖片1名稱
        /// </summary>
        public string Pic1Name { get; set; }

        /// <summary>
        /// 圖片2名稱
        /// </summary>
        public string Pic2Name { get; set; }

        /// <summary>
        /// 圖片3名稱
        /// </summary>
        public string Pic3Name { get; set; }

        /// <summary>
        /// 附件檔1
        /// </summary>
        public string AFile1 { get; set; }

        /// <summary>
        /// 附件檔2
        /// </summary>
        public string AFile2 { get; set; }

        /// <summary>
        /// 附件檔3
        /// </summary>
        public string AFile3 { get; set; }

        /// <summary>
        /// 附件檔1名稱
        /// </summary>
        public string AFile1Name { get; set; }

        /// <summary>
        /// 附件檔2名稱
        /// </summary>
        public string AFile2Name { get; set; }

        /// <summary>
        /// 附件檔3名稱
        /// </summary>
        public string AFile3Name { get; set; }

        /// <summary>
        /// 資料來源連結1
        /// </summary>
        public string AUrl1Link { get; set; }

        /// <summary>
        /// 資料來源連結2
        /// </summary>
        public string AUrl2Link { get; set; }

        /// <summary>
        /// 資料來源連結3
        /// </summary>
        public string AUrl3Link { get; set; }

        /// <summary>
        /// 資料來源1名稱
        /// </summary>
        public string AUrl1 { get; set; }

        /// <summary>
        /// 資料來源2名稱
        /// </summary>
        public string AUrl2 { get; set; }

        /// <summary>
        /// 資料來源3名稱
        /// </summary>
        public string AUrl3 { get; set; }

        /// <summary>
        /// 首頁顯示. 0.關閉 1.開啟
        /// </summary>
        [DisplayName("首頁顯示")]
        [Required]
        public string IsHome { get; set; }

        /// <summary>
        /// 顯示New. 0.關閉 1.開啟
        /// </summary>
        [DisplayName("顯示New")]
        [Required]
        public string IsNew { get; set; }

        /// <summary>
        /// 瀏覽人數
        /// </summary>
        [DisplayName("瀏覽人數")]
        [Required]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "必須為數字")]
        public int Vister { get; set; }

        /// <summary>
        /// 轉寄數
        /// </summary>
        [DisplayName("轉寄數")]
        [Required]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "必須為數字")]
        public int Emailer { get; set; }

        /// <summary>
        /// 列印數
        /// </summary>
        [DisplayName("列印數")]
        [Required]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "必須為數字")]
        public int Printer { get; set; }

        /// <summary>
        /// 主題分類編號
        /// </summary>
        public string MainCode { get; set; }

        /// <summary>
        /// 主題分類名稱
        /// </summary>
        public string MainName { get; set; }

        /// <summary>
        /// 施政分類編號
        /// </summary>
        public string AdminCode { get; set; }

        /// <summary>
        /// 施政分類名稱
        /// </summary>
        public string AdminName { get; set; }

        /// <summary>
        /// 服務分類編號
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// 服務分類名稱
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Tag 注：和國際環保標準 / 規範介紹相關聯 用,區隔
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 是否站外
        /// </summary>
        [DisplayName("是否站外")]
        [Required]
        public string IsOut { get; set; }

        /// <summary>
        /// 站外連結
        /// </summary>
        public virtual string AUrl { get; set; }
    }
}

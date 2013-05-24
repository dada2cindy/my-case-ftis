using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 新聞動態
    /// </summary>
    [Serializable]
    [DataContract]
    public class News
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int NewsId { get; set; }

        /// <summary>
        /// 新聞分類(議題)
        /// </summary>
        [DataMember]
        public virtual NewsClass NewsClass { get; set; }

        /// <summary>
        /// 新聞種類
        /// </summary>
        [DataMember]
        public virtual NewsType NewsType { get; set; }

        /// <summary>
        /// 語言. 1.英文 2.中文
        /// </summary>
        [DataMember]
        public virtual string LangId { get; set; }

        #region 中文欄位

        /// <summary>
        /// 標題
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 刊登日期
        /// </summary>
        [DataMember]
        public virtual DateTime? PostDate { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [DataMember]
        public virtual string Content { get; set; }

        /// <summary>
        /// 圖片1
        /// </summary>
        [DataMember]
        public virtual string Pic1 { get; set; }

        /// <summary>
        /// 圖片2
        /// </summary>
        [DataMember]
        public virtual string Pic2 { get; set; }

        /// <summary>
        /// 圖片3
        /// </summary>
        [DataMember]
        public virtual string Pic3 { get; set; }

        /// <summary>
        /// 圖片1名稱
        /// </summary>
        [DataMember]
        public virtual string Pic1Name { get; set; }

        /// <summary>
        /// 圖片2名稱
        /// </summary>
        [DataMember]
        public virtual string Pic2Name { get; set; }

        /// <summary>
        /// 圖片3名稱
        /// </summary>
        [DataMember]
        public virtual string Pic3Name { get; set; }

        /// <summary>
        /// 附件檔1
        /// </summary>
        [DataMember]
        public virtual string AFile1 { get; set; }

        /// <summary>
        /// 附件檔2
        /// </summary>
        [DataMember]
        public virtual string AFile2 { get; set; }

        /// <summary>
        /// 附件檔3
        /// </summary>
        [DataMember]
        public virtual string AFile3 { get; set; }

        /// <summary>
        /// 附件檔1名稱
        /// </summary>
        [DataMember]
        public virtual string AFile1Name { get; set; }

        /// <summary>
        /// 附件檔2名稱
        /// </summary>
        [DataMember]
        public virtual string AFile2Name { get; set; }

        /// <summary>
        /// 附件檔3名稱
        /// </summary>
        [DataMember]
        public virtual string AFile3Name { get; set; }

        /// <summary>
        /// 資料來源連結1
        /// </summary>
        [DataMember]
        public virtual string AUrl1Link { get; set; }

        /// <summary>
        /// 資料來源連結2
        /// </summary>
        [DataMember]
        public virtual string AUrl2Link { get; set; }

        /// <summary>
        /// 資料來源連結3
        /// </summary>
        [DataMember]
        public virtual string AUrl3Link { get; set; }

        /// <summary>
        /// 資料來源1名稱
        /// </summary>
        [DataMember]
        public virtual string AUrl1 { get; set; }

        /// <summary>
        /// 資料來源2名稱
        /// </summary>
        [DataMember]
        public virtual string AUrl2 { get; set; }

        /// <summary>
        /// 資料來源3名稱
        /// </summary>
        [DataMember]
        public virtual string AUrl3 { get; set; }

        /// <summary>
        /// 首頁顯示. 0.關閉 1.開啟
        /// </summary>
        [DataMember]
        public virtual string IsHome { get; set; }

        /// <summary>
        /// 顯示New. 0.關閉 1.開啟
        /// </summary>
        [DataMember]
        public virtual string IsNew { get; set; }

        /// <summary>
        /// 瀏覽人數
        /// </summary>
        [DataMember]
        public virtual int Vister { get; set; }

        /// <summary>
        /// 轉寄數
        /// </summary>
        [DataMember]
        public virtual int Emailer { get; set; }

        /// <summary>
        /// 列印數
        /// </summary>
        [DataMember]
        public virtual int Printer { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public virtual int SortId { get; set; }

        /// <summary>
        /// 狀態. 0.關閉 1.開啟
        /// </summary>
        [DataMember]
        public virtual string Status { get; set; }

        /// <summary>
        /// 主題分類編號
        /// </summary>
        [DataMember]
        public virtual string MainCode { get; set; }

        /// <summary>
        /// 主題分類名稱
        /// </summary>
        [DataMember]
        public virtual string MainName { get; set; }

        /// <summary>
        /// 施政分類編號
        /// </summary>
        [DataMember]
        public virtual string AdminCode { get; set; }

        /// <summary>
        /// 施政分類名稱
        /// </summary>
        [DataMember]
        public virtual string AdminName { get; set; }

        /// <summary>
        /// 服務分類編號
        /// </summary>
        [DataMember]
        public virtual string ServiceCode { get; set; }

        /// <summary>
        /// 服務分類名稱
        /// </summary>
        [DataMember]
        public virtual string ServiceName { get; set; }

        /// <summary>
        /// Tag 注：和國際環保標準 / 規範介紹相關聯 用,區隔
        /// </summary>
        [DataMember]
        public virtual string Tag { get; set; }

        /// <summary>
        /// 是否站外
        /// </summary>
        [DataMember]
        public virtual string IsOut { get; set; }

        /// <summary>
        /// 站外連結
        /// </summary>
        [DataMember]
        public virtual string AUrl { get; set; }

        #endregion

        #region 英文欄位

        /// <summary>
        /// 標題
        /// </summary>
        [DataMember]
        public virtual string NameENG { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [DataMember]
        public virtual string ContentENG { get; set; }

        /// <summary>
        /// 圖片1
        /// </summary>
        [DataMember]
        public virtual string Pic1ENG { get; set; }

        /// <summary>
        /// 圖片2
        /// </summary>
        [DataMember]
        public virtual string Pic2ENG { get; set; }

        /// <summary>
        /// 圖片3
        /// </summary>
        [DataMember]
        public virtual string Pic3ENG { get; set; }

        /// <summary>
        /// 圖片1名稱
        /// </summary>
        [DataMember]
        public virtual string Pic1NameENG { get; set; }

        /// <summary>
        /// 圖片2名稱
        /// </summary>
        [DataMember]
        public virtual string Pic2NameENG { get; set; }

        /// <summary>
        /// 圖片3名稱
        /// </summary>
        [DataMember]
        public virtual string Pic3NameENG { get; set; }

        /// <summary>
        /// 附件檔1
        /// </summary>
        [DataMember]
        public virtual string AFile1ENG { get; set; }

        /// <summary>
        /// 附件檔2
        /// </summary>
        [DataMember]
        public virtual string AFile2ENG { get; set; }

        /// <summary>
        /// 附件檔3
        /// </summary>
        [DataMember]
        public virtual string AFile3ENG { get; set; }

        /// <summary>
        /// 附件檔1名稱
        /// </summary>
        [DataMember]
        public virtual string AFile1NameENG { get; set; }

        /// <summary>
        /// 附件檔2名稱
        /// </summary>
        [DataMember]
        public virtual string AFile2NameENG { get; set; }

        /// <summary>
        /// 附件檔3名稱
        /// </summary>
        [DataMember]
        public virtual string AFile3NameENG { get; set; }

        /// <summary>
        /// 資料來源連結1
        /// </summary>
        [DataMember]
        public virtual string AUrl1LinkENG { get; set; }

        /// <summary>
        /// 資料來源連結2
        /// </summary>
        [DataMember]
        public virtual string AUrl2LinkENG { get; set; }

        /// <summary>
        /// 資料來源連結3
        /// </summary>
        [DataMember]
        public virtual string AUrl3LinkENG { get; set; }

        /// <summary>
        /// 資料來源1名稱
        /// </summary>
        [DataMember]
        public virtual string AUrl1ENG { get; set; }

        /// <summary>
        /// 資料來源2名稱
        /// </summary>
        [DataMember]
        public virtual string AUrl2ENG { get; set; }

        /// <summary>
        /// 資料來源3名稱
        /// </summary>
        [DataMember]
        public virtual string AUrl3ENG { get; set; }

        /// <summary>
        /// 首頁顯示. 0.關閉 1.開啟
        /// </summary>
        [DataMember]
        public virtual string IsHomeENG { get; set; }

        /// <summary>
        /// 顯示New. 0.關閉 1.開啟
        /// </summary>
        [DataMember]
        public virtual string IsNewENG { get; set; }

        /// <summary>
        /// 瀏覽人數
        /// </summary>
        [DataMember]
        public virtual int VisterENG { get; set; }

        /// <summary>
        /// 轉寄數
        /// </summary>
        [DataMember]
        public virtual int EmailerENG { get; set; }

        /// <summary>
        /// 列印數
        /// </summary>
        [DataMember]
        public virtual int PrinterENG { get; set; }

        /// <summary>
        /// 主題分類編號
        /// </summary>
        [DataMember]
        public virtual string MainCodeENG { get; set; }

        /// <summary>
        /// 主題分類名稱
        /// </summary>
        [DataMember]
        public virtual string MainNameENG { get; set; }

        /// <summary>
        /// 施政分類編號
        /// </summary>
        [DataMember]
        public virtual string AdminCodeENG { get; set; }

        /// <summary>
        /// 施政分類名稱
        /// </summary>
        [DataMember]
        public virtual string AdminNameENG { get; set; }

        /// <summary>
        /// 服務分類編號
        /// </summary>
        [DataMember]
        public virtual string ServiceCodeENG { get; set; }

        /// <summary>
        /// 服務分類名稱
        /// </summary>
        [DataMember]
        public virtual string ServiceNameENG { get; set; }

        /// <summary>
        /// Tag 注：和國際環保標準 / 規範介紹相關聯 用,區隔
        /// </summary>
        [DataMember]
        public virtual string TagENG { get; set; }

        /// <summary>
        /// 是否站外
        /// </summary>
        [DataMember]
        public virtual string IsOutENG { get; set; }

        /// <summary>
        /// 站外連結
        /// </summary>
        [DataMember]
        public virtual string AUrlENG { get; set; }

        #endregion

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 活動訊息
    /// </summary>
    [Serializable]
    [DataContract]
    public class Activity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int ActivityId { get; set; }

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
        /// 主辦單位
        /// </summary>
        [DataMember]
        public virtual string MCompany { get; set; }

        /// <summary>
        /// 連絡人姓名
        /// </summary>
        [DataMember]
        public virtual string Contact { get; set; }

        /// <summary>
        /// 舉辦日期
        /// </summary>
        [DataMember]
        public virtual string ActDate { get; set; }

        /// <summary>
        /// 連絡人電話
        /// </summary>
        [DataMember]
        public virtual string Tel { get; set; }

        /// <summary>
        /// 活動地點
        /// </summary>
        [DataMember]
        public virtual string Address { get; set; }

        /// <summary>
        /// 連絡人傳真
        /// </summary>
        [DataMember]
        public virtual string Fax { get; set; }        

        /// <summary>
        /// 費用
        /// </summary>
        [DataMember]
        public virtual string ActFee { get; set; }

        /// <summary>
        /// 報名方式
        /// </summary>
        [DataMember]
        public virtual string ActType { get; set; }

        /// <summary>
        /// 報名狀態
        /// </summary>
        [DataMember]
        public virtual string JoinStatus { get; set; }

        /// <summary>
        /// 活動狀態
        /// </summary>
        [DataMember]
        public virtual string ActStatus { get; set; }

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
        /// 相關連接
        /// </summary>
        [DataMember]
        public virtual string AUrl { get; set; }

        /// <summary>
        /// 資料來源1
        /// </summary>
        [DataMember]
        public virtual string AUrl1 { get; set; }

        /// <summary>
        /// 資料來源2
        /// </summary>
        [DataMember]
        public virtual string AUrl2 { get; set; }

        /// <summary>
        /// 資料來源3
        /// </summary>
        [DataMember]
        public virtual string AUrl3 { get; set; }

        /// <summary>
        /// 顯示New. 0.關閉 1.開啟
        /// </summary>
        [DataMember]
        public virtual string IsNew { get; set; }

        /// <summary>
        /// 首頁顯示. 0.關閉 1.開啟
        /// </summary>
        [DataMember]
        public virtual string IsHome { get; set; }        

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

        public virtual string GetStr_Status
        {
            get
            {
                string result = string.Empty;
                switch (this.Status)
                {
                    case "0":
                        result = "關閉";
                        break;
                    case "1":
                        result = "開啟";
                        break;
                }

                return result;
            }

        }

        #endregion        
    }
}

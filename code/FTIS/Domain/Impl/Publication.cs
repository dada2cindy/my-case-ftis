using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 期刊
    /// </summary>
    [Serializable]
    [DataContract]
    public class Publication
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int PublicationId { get; set; }

        /// <summary>
        /// 期刊分類
        /// </summary>
        [DataMember]
        public virtual PublicationClass PublicationClass { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 期別
        /// </summary>
        [DataMember]
        public virtual string PubNo { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [DataMember]
        public virtual string Content { get; set; }            

        /// <summary>
        /// 本期封面圖片
        /// </summary>
        [DataMember]
        public virtual string Pic1 { get; set; }             

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
        /// 特別企劃項目1
        /// </summary>
        [DataMember]
        public virtual string Spec1 { get; set; }

        /// <summary>
        /// 特別企劃項目2
        /// </summary>
        [DataMember]
        public virtual string Spec2 { get; set; }

        /// <summary>
        /// 特別企劃項目3
        /// </summary>
        [DataMember]
        public virtual string Spec3 { get; set; }

        /// <summary>
        /// 特別企劃項目4
        /// </summary>
        [DataMember]
        public virtual string Spec4 { get; set; }

        /// <summary>
        /// 特別企劃項目5
        /// </summary>
        [DataMember]
        public virtual string Spec5 { get; set; }

        /// <summary>
        /// 特別企劃項目6
        /// </summary>
        [DataMember]
        public virtual string Spec6 { get; set; }

        /// <summary>
        /// A.本期刊物電子檔案上傳
        /// </summary>
        [DataMember]
        public virtual string AFile1 { get; set; }   

        /// <summary>
        /// B.本期刊物電子檔案遠端連結網址
        /// </summary>
        [DataMember]
        public virtual string LinkFile { get; set; }

        #endregion
    }
}

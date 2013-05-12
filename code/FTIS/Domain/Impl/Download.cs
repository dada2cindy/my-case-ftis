using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 技術工具/文件下載
    /// </summary>
    [Serializable]
    [DataContract]
    public class Download
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int DownloadId { get; set; }

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
        /// 附件檔1
        /// </summary>
        [DataMember]
        public virtual string AFile1 { get; set; }

        /// <summary>
        /// 附件檔1名稱
        /// </summary>
        [DataMember]
        public virtual string AFile1Name { get; set; }                

        /// <summary>
        /// 瀏覽人數
        /// </summary>
        [DataMember]
        public virtual int Vister { get; set; }

        /// <summary>
        /// 下載數
        /// </summary>
        [DataMember]
        public virtual int Downer { get; set; }

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
    }
}

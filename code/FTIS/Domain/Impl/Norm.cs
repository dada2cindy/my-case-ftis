using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 標準/規範資訊
    /// </summary>
    [Serializable]
    [DataContract]
    public class Norm : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int NormId { get; set; }

        /// <summary>
        /// 地區
        /// </summary>
        [DataMember]
        public virtual NormClass NormClass { get; set; }

        /// <summary>
        /// 區域
        /// </summary>
        [DataMember]
        public virtual NormClass NormClassParent { get; set; }

        /// <summary>
        /// 標題英文
        /// </summary>
        [DataMember]
        public virtual string NameENG { get; set; }

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
        /// Tag 注：和新聞動態相關聯,多個請用","分隔.
        /// </summary>
        [DataMember]
        public virtual string Tag { get; set; }

        #endregion
    }
}

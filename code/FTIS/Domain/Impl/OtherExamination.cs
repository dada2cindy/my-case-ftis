using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 問卷調查
    /// </summary>
    [Serializable]
    [DataContract]
    public class OtherExamination
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int OtherExaminationId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [DataMember]
        public virtual string Content { get; set; }

        /// <summary>
        /// 連接地址
        /// </summary>
        [DataMember]
        public virtual string AUrl { get; set; }

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

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 行業分類
    /// </summary>
    [Serializable]
    [DataContract]
    public class Industry
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int IndustryId { get; set; }

        /// <summary>
        /// 分類名稱
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

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

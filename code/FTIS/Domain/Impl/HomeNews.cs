using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 即時訊息
    /// </summary>
    [Serializable]
    [DataContract]
    public class HomeNews
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int HomeNewsId { get; set; }

        /// <summary>
        /// 分類名稱
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 連結網址
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

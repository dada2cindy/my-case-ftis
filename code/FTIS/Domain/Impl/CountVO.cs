using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 網站計數器
    /// </summary>
    [Serializable]
    [DataContract]
    public class CountVO : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int CountId { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]
        public virtual DateTime CountDate { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        [DataMember]
        public virtual int Hits { get; set; }

        /// <summary>
        /// 對象 (目前只有1)
        /// </summary>
        [DataMember]
        public virtual int BarId { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 後台功能
    /// </summary>
    [Serializable]
    [DataContract]
    public class AdminBar
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int AdminBarId { get; set; }

        /// <summary>
        /// 分類名稱
        /// </summary>
        [DataMember]
        public virtual string BarName { get; set; }

        #endregion
    }
}

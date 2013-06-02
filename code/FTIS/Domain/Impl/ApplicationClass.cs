using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 企業社會責任分類
    /// </summary>
    [Serializable]
    [DataContract]
    public class ApplicationClass : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int ApplicationClassId { get; set; }

        #endregion
    }
}

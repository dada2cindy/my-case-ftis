using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 期刊分類
    /// </summary>
    [Serializable]
    [DataContract]
    public class PublicationClass : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int PublicationClassId { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 綠色工廠分類
    /// </summary>
    [Serializable]
    [DataContract]
    public class GreenFactoryClass : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int GreenFactoryClassId { get; set; }

        #endregion
    }
}

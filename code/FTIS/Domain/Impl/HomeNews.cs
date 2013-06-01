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
    public class HomeNews : Entity
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
        /// 連結網址
        /// </summary>
        [DataMember]
        public virtual string AUrl { get; set; }       

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 分類
    /// </summary>
    [Serializable]
    [DataContract]
    public class Node : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int NodeId { get; set; }

        /// <summary>
        /// 上層分類
        /// </summary>
        [DataMember]
        public virtual Node ParentNode { get; set; }

        /// <summary>
        /// 名稱_英文
        /// </summary>
        [DataMember]
        public virtual string NameENG { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [DataMember]
        public virtual string Content { get; set; }

        /// <summary>
        /// 內容_英文
        /// </summary>
        [DataMember]
        public virtual string ContentENG { get; set; }

        #endregion
    }
}

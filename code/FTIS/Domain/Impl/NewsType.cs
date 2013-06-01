using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 新聞動態種類
    /// </summary>
    [Serializable]
    [DataContract]
    public class NewsType : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int NewsTypeId { get; set; }

        /// <summary>
        /// 分類名稱_英文
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

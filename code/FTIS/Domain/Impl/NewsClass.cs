using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 新聞動態議題
    /// </summary>
    [Serializable]
    [DataContract]
    public class NewsClass : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int NewsClassId { get; set; }

        /// <summary>
        /// 分類名稱_英文
        /// </summary>
        [DataMember]
        public virtual string NameENG { get; set; }

        /// <summary>
        /// 語言. 1.英文 2.中文
        /// </summary>
        [DataMember]
        public virtual string LangId { get; set; }

        #endregion
    }
}

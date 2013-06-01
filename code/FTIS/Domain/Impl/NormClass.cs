using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 標準/規範分類
    /// </summary>
    [Serializable]
    [DataContract]
    public class NormClass : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int NormClassId { get; set; }

        /// <summary>
        /// 上層
        /// </summary>
        [DataMember]
        public virtual NormClass ParentNormClass { get; set; }

        /// <summary>
        /// 分類名稱_英文
        /// </summary>
        [DataMember]
        public virtual string NameENG { get; set; }

        /// <summary>
        /// 圖片1
        /// </summary>
        [DataMember]
        public virtual string Pic1 { get; set; }

        #endregion
    }
}

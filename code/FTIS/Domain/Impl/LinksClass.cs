using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 網路資源分類
    /// </summary>
    [Serializable]
    [DataContract]
    public class LinksClass : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int LinksClassId { get; set; }

        /// <summary>
        /// 語言. 1.英文 2.中文
        /// </summary>
        [DataMember]
        public virtual string LangId { get; set; }

        public virtual string GetStr_LangId
        {
            get
            {
                string result = string.Empty;
                switch (this.LangId)
                {
                    case "1":
                        result = "英文";
                        break;
                    case "2":
                        result = "中文";
                        break;
                }

                return result;
            }
        }

        #endregion
    }
}

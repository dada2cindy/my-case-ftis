using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    [Serializable]
    [DataContract]
    public class Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// 名稱
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public virtual int SortId { get; set; }

        /// <summary>
        /// 狀態. 0.關閉 1.開啟
        /// </summary>
        [DataMember]
        public virtual string Status { get; set; }

        public virtual string GetStr_Status
        {
            get
            {
                string result = string.Empty;
                switch (this.Status)
                {
                    case "0":
                        result = "關閉";
                        break;
                    case "1":
                        result = "開啟";
                        break;
                }

                return result;
            }
        }

        #endregion
    }
}

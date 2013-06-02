using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 電子報
    /// </summary>
    [Serializable]
    [DataContract]
    public class Epaper : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int EpaperId { get; set; }

        /// <summary>
        /// 期數
        /// </summary>
        [DataMember]
        public virtual string ENo { get; set; }

        /// <summary>
        /// 刊登日期
        /// </summary>
        [DataMember]
        public virtual DateTime? PostDate { get; set; }        

        /// <summary>
        /// 瀏覽人數
        /// </summary>
        [DataMember]
        public virtual int Vister { get; set; }      

        /// <summary>
        /// 連接網址
        /// </summary>
        [DataMember]
        public virtual string AUrl { get; set; }

        public virtual string GetStr_PostDate
        {
            get
            {
                string result = string.Empty;

                if (PostDate != null)
                {
                    result = PostDate.Value.ToString("yyyy/MM/dd");
                }

                return result;
            }
        }

        #endregion
    }
}

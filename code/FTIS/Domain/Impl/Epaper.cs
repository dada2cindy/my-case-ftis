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
    public class Epaper
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
        /// 標題
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

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
        /// 排序
        /// </summary>
        [DataMember]
        public virtual int SortId { get; set; }

        /// <summary>
        /// 狀態. 0.關閉 1.開啟
        /// </summary>
        [DataMember]
        public virtual string Status { get; set; }        

        /// <summary>
        /// 連接網址
        /// </summary>
        [DataMember]
        public virtual string AUrl { get; set; }

        #endregion
    }
}

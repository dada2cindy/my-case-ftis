using System;
using System.Runtime.Serialization;

namespace WuDada.Core.Common.Domain
{
    /// <summary>
    /// 序號紀錄
    /// </summary>
    [Serializable]
    [DataContract]
    public class SerialVO
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// 代碼
        /// </summary>
        [DataMember]
        public virtual string SerialId { get; set; }

        /// <summary>
        /// 流水號
        /// </summary>
        [DataMember]
        public virtual int Count { get; set; }

        /// <summary>
        /// 流水號取得最後日期
        /// </summary>
        [DataMember]
        public virtual DateTime? Date { get; set; }

        #endregion
    }
}

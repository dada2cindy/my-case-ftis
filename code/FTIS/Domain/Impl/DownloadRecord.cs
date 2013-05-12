using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 會員下載紀錄
    /// </summary>
    [Serializable]
    [DataContract]
    public class DownloadRecord
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int DownloadRecordId { get; set; }

        /// <summary>
        /// 會員
        /// </summary>
        [DataMember]
        public virtual Member Member { get; set; }

        /// <summary>
        /// 文件名稱
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 下載單元 1.技術工具/文件 2.課程講義
        /// </summary>
        [DataMember]
        public virtual string ClassId { get; set; }

        /// <summary>
        /// 下載日期
        /// </summary>
        [DataMember]
        public virtual DateTime? PostDate { get; set; }

        /// <summary>
        /// 下載次數(統計時用，不是db的欄位)
        /// </summary>
        [DataMember]
        public virtual int Downer { get; set; }

        #endregion
    }
}

using System;
using System.Runtime.Serialization;

namespace WuDada.Core.SystemApplications.Domain
{
    /// <summary>
    /// 系統紀錄
    /// </summary>
    [Serializable]
    [DataContract]
    public class LogSystemVO
    {
        #region Constructor

        public LogSystemVO()
        {

        }

        #endregion

        #region Property

        [DataMember]
        public virtual int LogSystemId { get; set; }

        [DataMember]
        public virtual DateTime UpdateDate { get; set; }

        [DataMember]
        public virtual string Fucntion { get; set; }

        [DataMember]
        public virtual string SubFucntion { get; set; }

        [DataMember]
        public virtual string Action { get; set; }

        [DataMember]
        public virtual string UpdateId { get; set; }

        [DataMember]
        public virtual string Note { get; set; }

        [DataMember]
        public virtual string UpdateClassName { get; set; }

        [DataMember]
        public virtual string IpAddress { get; set; }

        #endregion

    }
}

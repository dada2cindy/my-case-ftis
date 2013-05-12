using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 後台帳號登入紀錄
    /// </summary>
    [Serializable]
    [DataContract]
    public class MasterLog
    {
        #region Constructor

        public MasterLog()
        {
        }

        public MasterLog(string account, string ip)
        {
            this.Account = account;
            this.IP = ip;
            EnterTime = DateTime.Now;
            LeaveTime = DateTime.Now.AddMinutes(14);
            Status = "1";
        }

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int MasterLogId { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        [DataMember]
        public virtual string Account { get; set; }

        /// <summary>
        /// 登入日期
        /// </summary>
        [DataMember]
        public virtual DateTime? EnterTime { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        [DataMember]
        public virtual string IP { get; set; }

        /// <summary>
        /// 登出日期(預設都加14-15分鐘)
        /// </summary>
        [DataMember]
        public virtual DateTime? LeaveTime { get; set; }

        /// <summary>
        /// 狀態.(數字意思不詳)
        /// </summary>
        [DataMember]
        public virtual string Status { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [DataMember]
        public virtual string Password { get; set; }

        #endregion
    }
}

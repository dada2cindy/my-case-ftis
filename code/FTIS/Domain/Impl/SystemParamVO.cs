using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 系統參數
    /// </summary>
    [Serializable]
    [DataContract]
    public class SystemParamVO
    {
        #region Property

        /// <summary>
        /// 識別碼
        /// </summary>
        [DataMember]
        public virtual int SystemParamId { get; set; }

        /// <summary>
        /// 寄信Email
        /// </summary>
        [DataMember]
        public virtual string SendEmail { get; set; }

        /// <summary>
        /// 寄信Email帳號
        /// </summary>
        [DataMember]
        public virtual string Account { get; set; }

        /// <summary>
        /// 寄信Email密碼
        /// </summary>
        [DataMember]
        public virtual string Password { get; set; }

        /// <summary>
        /// 寄件伺服器位置
        /// </summary>
        [DataMember]
        public virtual string MailSmtp { get; set; }

        /// <summary>
        /// 寄件伺服器Port
        /// </summary>
        [DataMember]
        public virtual string MailPort { get; set; }

        /// <summary>
        /// 啟用SSL
        /// </summary>
        [DataMember]
        public virtual bool EnableSSL { get; set; }

        #endregion
    }
}

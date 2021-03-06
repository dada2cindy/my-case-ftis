﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 後台帳號
    /// </summary>
    [Serializable]
    [DataContract]
    public class MasterMember : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int MasterMemberId { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        [DataMember]
        public virtual string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [DataMember]
        public virtual string Password { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        [DataMember]
        public virtual string Tel { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [DataMember]
        public virtual string Memo { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DataMember]
        public virtual string Email { get; set; }

        /// <summary>
        /// 註冊日期
        /// </summary>
        [DataMember]
        public virtual DateTime? RegDate { get; set; }

        /// <summary>
        /// 功能權限
        /// </summary>
        [DataMember]
        public virtual IList<AdminRole> AdminRoles { get; set; }

        public virtual string GetStr_RegDate
        {
            get
            {
                string result = string.Empty;

                if (RegDate != null)
                {
                    result = RegDate.Value.ToString("yyyy/MM/dd");
                }

                return result;
            }
        }

        #endregion
    }
}

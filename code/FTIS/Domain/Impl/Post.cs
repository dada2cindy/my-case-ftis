using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 文章
    /// </summary>
    [Serializable]
    [DataContract]
    public class Post : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int PostId { get; set; }

        /// <summary>
        /// 分類
        /// </summary>
        [DataMember]
        public virtual Node Node { get; set; }

        /// <summary>
        /// 廠商名稱
        /// </summary>
        [DataMember]
        public virtual string Company { get; set; }

        /// <summary>
        /// 廠商名稱(英)
        /// </summary>
        [DataMember]
        public virtual string CompanyENG { get; set; }

        /// <summary>
        /// 登錄效期
        /// </summary>
        [DataMember]
        public virtual DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// 負責人
        /// </summary>
        [DataMember]
        public virtual string Charge { get; set; }

        /// <summary>
        /// 連絡人
        /// </summary>
        [DataMember]
        public virtual string Contact { get; set; }        

        /// <summary>
        /// 電話
        /// </summary>
        [DataMember]
        public virtual string Tel { get; set; }

        /// <summary>
        /// 傳真
        /// </summary>
        [DataMember]
        public virtual string Fax { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [DataMember]
        public virtual string Address { get; set; }

        /// <summary>
        /// 電子郵件
        /// </summary>
        [DataMember]
        public virtual string Email { get; set; }

        /// <summary>
        /// 登錄項目明細
        /// </summary>
        [DataMember]
        public virtual string Content { get; set; }

        public virtual string GetStr_ExpiredDate
        {
            get
            {
                string result = string.Empty;

                if (ExpiredDate != null)
                {
                    result = ExpiredDate.Value.ToString("yyyy/MM/dd");
                }

                return result;
            }
        }

        #endregion
    }
}

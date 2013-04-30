using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Auth.Domain
{
    /// <summary>
    /// 後台使用者
    /// </summary>
    [Serializable]
    [DataContract]
    public class LoginUserVO
    {
        #region Constructor

        public LoginUserVO()
        {
            this.IsAlive = 1;
        }

        #endregion

        #region Property

        /// <summary>
        /// 帳號
        /// </summary>
        [DataMember]
        public virtual string UserId { get; set; }

        [DataMember]
        public virtual int Version { get; set; }

        /// <summary>
        /// 後台角色
        /// </summary>
        [DataMember]
        public virtual IList<LoginRoleVO> LoginRoleList { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [DataMember]
        public virtual string Password { get; set; }

        [DataMember]
        public virtual string Comment { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [DataMember]
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// 中文姓名
        /// </summary>
        [DataMember]
        public virtual string FullNameInChinese { get; set; }

        /// <summary>
        /// 英文姓名
        /// </summary>
        [DataMember]
        public virtual string FullNameInEnglish { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        [DataMember]
        public virtual string Sex { get; set; }

        /// <summary>
        /// 聯絡地址
        /// </summary>
        [DataMember]
        public virtual string ContactAddress { get; set; }

        /// <summary>
        /// 聯絡郵遞區號
        /// </summary>
        [DataMember]
        public virtual string ContactZipCode { get; set; }

        /// <summary>
        /// 住家電話區碼
        /// </summary>
        [DataMember]
        public virtual string HousePhoneAreaCode { get; set; }

        /// <summary>
        /// 住家電話
        /// </summary>
        [DataMember]
        public virtual string HousePhone { get; set; }

        /// <summary>
        /// 住家其他電話
        /// </summary>
        [DataMember]
        public virtual string HouseOtherPhone { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        [DataMember]
        public virtual string Email { get; set; }

        /// <summary>
        /// 身份證號
        /// </summary>
        [DataMember]
        public virtual string SSID { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [DataMember]
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// 身高(cm)
        /// </summary>
        [DataMember]
        public virtual int Height { get; set; }

        /// <summary>
        /// 體重(kg)
        /// </summary>
        [DataMember]
        public virtual int Weight { get; set; }

        /// <summary>
        /// 血型(O型、A型、B型、AB型)
        /// </summary>
        [DataMember]
        public virtual string Blood { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        [DataMember]
        public virtual string JobTitle { get; set; }

        /// <summary>
        /// 手機電話
        /// </summary>
        [DataMember]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// (到職日)開始日期
        /// </summary>
        [DataMember]
        public virtual DateTime? ArrivedDate { get; set; }

        /// <summary>
        /// (離職日)結束日期
        /// </summary>
        [DataMember]
        public virtual DateTime? LeaveDate { get; set; }

        /// <summary>
        /// 緊急聯絡人
        /// </summary>
        [DataMember]
        public virtual string EmergencyContactPerson { get; set; }

        /// <summary>
        /// 與緊急聯絡人關係
        /// </summary>
        [DataMember]
        public virtual string EmergencyRelationship { get; set; }

        /// <summary>
        /// 與緊急聯絡人電話
        /// </summary>
        [DataMember]
        public virtual string EmergencyPhone { get; set; }

        /// <summary>
        /// 與緊急聯絡人電話
        /// </summary>
        [DataMember]
        public virtual string EmergencyAddress { get; set; }

        /// <summary>
        /// UpdatedBy
        /// </summary>
        [DataMember]
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        /// UpdatedDate
        /// </summary>
        [DataMember]
        public virtual DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// 是否在職
        /// </summary>
        [DataMember]
        public virtual int IsAlive { get; set; }

        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 網站滿意度問卷
    /// </summary>
    [Serializable]
    [DataContract]
    public class Examination : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int ExaminationId { get; set; }

        /// <summary>
        /// 行業別
        /// </summary>
        [DataMember]
        public virtual Industry Industry { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        [DataMember]
        public virtual string Gender { get; set; }

        /// <summary>
        /// 電子郵件
        /// </summary>
        [DataMember]
        public virtual string Email { get; set; }

        /// <summary>
        /// 填表日期
        /// </summary>
        [DataMember]
        public virtual DateTime? PostDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Question1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Question2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string QOther2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Question3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string QOther3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Question4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Question5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Question6 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Question7 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Question8 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Question9 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string QOther9 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Question10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string QOther10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Question11 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Question12 { get; set; }

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

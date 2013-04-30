using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.SystemApplications.Domain
{
    /// <summary>
    /// 樣板
    /// </summary>
    [Serializable]
    [DataContract]
    public class TemplateVO
    {
        #region Constructor

        public TemplateVO()
        {
            this.Flag = 1;
        }

        #endregion

        #region Property

        /// <summary>
        /// 識別碼
        /// </summary>
        [DataMember]
        public virtual int TemplateId { get; set; }

        /// <summary>
        /// 類別
        /// </summary>
        [DataMember]
        public virtual TemplateVO.Type TemplateType { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// CSS
        /// </summary>
        [DataMember]
        public virtual string CSS { get; set; }

        /// <summary>
        /// 樣板檔案
        /// </summary>
        [DataMember]
        public virtual string FileName { get; set; }

        /// <summary>
        /// Flash檔案
        /// </summary>
        [DataMember]
        public virtual string FileName2 { get; set; }

        /// <summary>
        /// 起始日期(節日) 四碼
        /// </summary>
        [DataMember]
        public virtual string StartDate { get; set; }

        /// <summary>
        /// 結束日期 四碼
        /// </summary>
        [DataMember]
        public virtual string EndDate { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        [DataMember]
        public virtual int Flag { get; set; }

        /// <summary>
        /// 樣板類別
        /// </summary>
        public enum Type
        {
            Season = 0,
            Festival = 1
        }

        /// <summary>
        /// 動態排序日期順序用的
        /// </summary>
        [DataMember]
        public virtual DateTime? BeginDate { get; set; }

        /// <summary>
        /// 動態排序日期順序用的
        /// </summary>
        [DataMember]
        public virtual DateTime? LastDate { get; set; }

        #endregion
    }
}

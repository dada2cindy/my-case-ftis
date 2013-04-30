using System;
using System.Runtime.Serialization;

namespace WuDada.Core.SystemApplications.Domain
{
    /// <summary>
    /// 項目參數
    /// </summary>
    [Serializable]
    [DataContract]
    public class ItemParamVO
    {
        #region Constructor

        public ItemParamVO()
        {

        }

        public ItemParamVO(string Classify, string Name, string Value, bool Deleted)
        {
            this.Classify = Classify;
            this.Name = Name;
            this.Value = Value;
            this.Deleted = Deleted;
        }
        #endregion

        #region Property

        /// <summary>
        /// 識別碼
        /// </summary>
        [DataMember]
        public virtual int ItemParamId { get; set; }

        /// <summary>
        /// 選項分類
        /// </summary>
        [DataMember]
        public virtual string Classify { get; set; }

        /// <summary>
        /// 選項名稱
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 選項值
        /// </summary>
        [DataMember]
        public virtual string Value { get; set; }

        /// <summary>
        /// 是否刪除
        /// </summary>
        [DataMember]
        public virtual bool Deleted { get; set; }

        #endregion
    }
}

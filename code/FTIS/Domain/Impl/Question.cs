using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// Q&A
    /// </summary>
    [Serializable]
    [DataContract]
    public class Question : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int QuestionId { get; set; }

        /// <summary>
        /// 新聞分類(議題)
        /// </summary>
        [DataMember]
        public virtual QuestionClass QuestionClass { get; set; }

        /// <summary>
        /// 語言. 1.英文 2.中文
        /// </summary>
        [DataMember]
        public virtual string LangId { get; set; }

        /// <summary>
        /// 上線日期
        /// </summary>
        [DataMember]
        public virtual DateTime? PostDate { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [DataMember]
        public virtual string Content { get; set; }

        /// <summary>
        /// 主題分類編號
        /// </summary>
        [DataMember]
        public virtual string MainCode { get; set; }

        /// <summary>
        /// 主題分類名稱
        /// </summary>
        [DataMember]
        public virtual string MainName { get; set; }

        /// <summary>
        /// 施政分類編號
        /// </summary>
        [DataMember]
        public virtual string AdminCode { get; set; }

        /// <summary>
        /// 施政分類名稱
        /// </summary>
        [DataMember]
        public virtual string AdminName { get; set; }

        /// <summary>
        /// 服務分類編號
        /// </summary>
        [DataMember]
        public virtual string ServiceCode { get; set; }

        /// <summary>
        /// 服務分類名稱
        /// </summary>
        [DataMember]
        public virtual string ServiceName { get; set; }

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

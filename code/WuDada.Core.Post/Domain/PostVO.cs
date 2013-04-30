using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Post.Domain
{
    [Serializable]
    [DataContract]
    public class PostVO : BasePost
    {
        public PostVO()
        {
            Flag = 1;
            SortNo = 0;
        }

        /// <summary>
        /// Pkey
        /// </summary>
        [DataMember]
        public virtual int PostId { get; set; }

        /// <summary>
        /// 屬於的Node
        /// </summary>
        [DataMember]
        public virtual NodeVO Node { get; set; }

        /// <summary>
        /// 圖檔
        /// </summary>
        [DataMember]
        public virtual string PicFileName { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        [DataMember]
        public virtual string DocFileName { get; set; }

        [DataMember]
        public virtual string StoreName { get; set; }

        [DataMember]
        public virtual string Phone { get; set; }

        [DataMember]
        public virtual string Fax { get; set; }

        [DataMember]
        public virtual string Address { get; set; }

        [DataMember]
        public virtual string GoogleMap { get; set; }

        [DataMember]
        public virtual int SortNo { get; set; }

        [DataMember]
        public virtual int Flag { get; set; }

        /// <summary>
        /// 上架日
        /// </summary>
        [DataMember]
        public virtual DateTime? ShowDate { get; set; }

        /// <summary>
        /// 0.站內 1.連結
        /// </summary>
        [DataMember]
        public virtual int Type { get; set; }

        /// <summary>
        /// 連結網址
        /// </summary>
        [DataMember]
        public virtual string LinkUrl { get; set; }

        /// <summary>
        /// 內容2
        /// </summary>
        [DataMember]
        public virtual string HtmlContent2 { get; set; }

        /// <summary>
        /// 內容3
        /// </summary>
        [DataMember]
        public virtual string HtmlContent3 { get; set; }

        /// <summary>
        /// 內容4
        /// </summary>
        [DataMember]
        public virtual string HtmlContent4 { get; set; }

        /// <summary>
        /// 取文字_上下架
        /// </summary>
        [DataMember]
        public virtual string GetStr_Flag
        {
            get
            {
                string result = "";

                if (Flag == 0)
                {
                    result = "否"; 
                }
                else if (Flag == 1)
                {
                    result = "是";
                }

                return result;
            }
        }
    }
}

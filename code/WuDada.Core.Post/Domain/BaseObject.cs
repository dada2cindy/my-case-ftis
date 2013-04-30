using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Post.Domain
{
    [Serializable]
    [DataContract]
    public class BaseObject
    {
        public BaseObject()
        {

        }

        /// <summary>
        /// 新增的使用者ID
        /// </summary>
        [DataMember]
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// 更新的使用者ID
        /// </summary>
        [DataMember]
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        /// 建立日期
        /// </summary>
        [DataMember]
        public virtual DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        [DataMember]
        public virtual DateTime? UpdatedDate { get; set; }
    }
}

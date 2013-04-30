using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Post.Domain
{
    [Serializable]
    [DataContract]
    public class NodeVO : BaseObject
    {
        #region Constructor

        public NodeVO()
        {
            this.Flag = 1;
            this.SortNo = 0;
            this.UType = NodeVO.UnitType.None;
        }

        #endregion

        #region Property

        /// <summary>
        /// Pkey
        /// </summary>
        [DataMember]
        public virtual int NodeId { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 上層Node
        /// </summary>
        [DataMember]
        public virtual NodeVO ParentNode { get; set; }

        /// <summary>
        /// 圖檔
        /// </summary>
        [DataMember]
        public virtual string PicFileName { get; set; }

        [DataMember]
        public virtual int SortNo { get; set; }

        [DataMember]
        public virtual int Flag { get; set; }

        /// <summary>
        /// 單元類別
        /// </summary>
        [DataMember]
        public virtual UnitType UType { get; set; }

        /// <summary>
        /// 單元類別
        /// </summary>
        public enum UnitType
        {
            None = 0,
            Pic = 1,
            Flash = 2
        }

        #endregion
    }
}

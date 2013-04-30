using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain
{
    [Serializable]
    [DataContract]
    public class AdminRole
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int AdminRoleId { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        [DataMember]
        public virtual MasterMember MasterMember { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        [DataMember]
        public virtual AdminBar AdminBar { get; set; }

        /// <summary>
        /// 權限總合 1,2,4,8 (查看,新增,修改,刪除)... 總合&權限 為true表示有這功能權限 
        /// </summary>
        [DataMember]
        public virtual int AdminValue { get; set; }

        #endregion
    }
}

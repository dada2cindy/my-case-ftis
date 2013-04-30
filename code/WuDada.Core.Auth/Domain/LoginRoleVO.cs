using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Auth.Domain
{
    /// <summary>
    /// 後台角色
    /// </summary>
    [Serializable]
    [DataContract]
    public class LoginRoleVO
    {
        #region Constructor

        public LoginRoleVO()
        {
        }

        public LoginRoleVO(string roleName)
        {
            this.RoleName = roleName;
        }

        #endregion

        #region Property

        /// <summary>
        /// 代碼
        /// </summary>
        [DataMember]
        public virtual int RoleId { get; set; }

        /// <summary>
        /// 角色名稱
        /// </summary>
        [DataMember]
        public virtual string RoleName { get; set; }

        [DataMember]
        public virtual string LoweredRoleName { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        /// <summary>
        /// 屬於此角色的後台功能
        /// </summary>
        [DataMember]
        public virtual IList<MenuFuncVO> MenuFuncList { get; set; }

        /// <summary>
        /// 屬於此角色的後台使用者
        /// </summary>
        [DataMember]
        public virtual IList<LoginUserVO> LoginUserList { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Auth.Domain;

namespace WuDada.Core.Auth.Persistence
{
    public interface ILoginRoleDao
    {
        /// <summary>
        /// 取得全部的後台角色清單
        /// </summary>
        /// <returns>全部的後台角色清單</returns>
        IList<LoginRoleVO> GetAllLoginRoleList();

        /// <summary>
        /// 新增後台角色
        /// </summary>
        /// <param name="loginRoleVO">被新增的後台角色</param>
        /// <returns>新增後的後台角色</returns>
        LoginRoleVO CreateLoginRole(LoginRoleVO loginRoleVO);

        /// <summary>
        /// 取得後台角色 By 角色Id No Lazy
        /// </summary>
        /// <param name="roleId">使用者Id</param>
        /// <returns>後台使用者</returns>
        LoginRoleVO GetLoginRoleById(int roleId);

        /// <summary>
        /// 更新後台角色
        /// </summary>
        /// <param name="loginRoleVO">被更新的後台角色</param>
        /// <returns>更新後的後台角色</returns>
        LoginRoleVO UpdateLoginRole(LoginRoleVO loginRoleVO);

        /// <summary>
        /// 刪除後台角色
        /// </summary>
        /// <param name="loginRoleVO">被刪除的後台角色</param>
        void DeleteLoginRole(LoginRoleVO loginRoleVO);
    }
}

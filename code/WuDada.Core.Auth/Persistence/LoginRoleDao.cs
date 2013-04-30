using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Persistence.Ado;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Persistence.NHibernate;
using WuDada.Core.Persistence;

namespace WuDada.Core.Auth.Persistence
{
    public class LoginRoleDao : AdoDao, ILoginRoleDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        /// <summary>
        /// 取得全部的後台角色清單
        /// </summary>
        /// <returns>全部的後台角色清單</returns>
        public IList<LoginRoleVO> GetAllLoginRoleList()
        {
            return NHibernateDao.GetAllVO<LoginRoleVO>();
        }

        /// <summary>
        /// 新增後台角色
        /// </summary>
        /// <param name="loginRoleVO">被新增的後台角色</param>
        /// <returns>新增後的後台角色</returns>
        public LoginRoleVO CreateLoginRole(LoginRoleVO loginRoleVO)
        {
            NHibernateDao.Insert(loginRoleVO);

            return loginRoleVO;
        }

        /// <summary>
        /// 取得後台角色 By 角色Id No Lazy
        /// </summary>
        /// <param name="roleId">使用者Id</param>
        /// <returns>後台使用者</returns>
        public LoginRoleVO GetLoginRoleById(int roleId)
        {
            return NHibernateDao.GetVOById<LoginRoleVO>(roleId);
        }

        /// <summary>
        /// 更新後台角色
        /// </summary>
        /// <param name="loginRoleVO">被更新的後台角色</param>
        /// <returns>更新後的後台角色</returns>
        public LoginRoleVO UpdateLoginRole(LoginRoleVO loginRoleVO)
        {
            NHibernateDao.Update(loginRoleVO);

            return loginRoleVO;
        }

        /// <summary>
        /// 刪除後台角色
        /// </summary>
        /// <param name="loginRoleVO">被刪除的後台角色</param>
        public void DeleteLoginRole(LoginRoleVO loginRoleVO)
        {
            NHibernateDao.Delete(loginRoleVO);
        }
    }
}

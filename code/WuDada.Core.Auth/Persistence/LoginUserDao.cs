using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Persistence.Ado;
using WuDada.Core.Persistence;
using WuDada.Core.Auth.Domain;

namespace WuDada.Core.Auth.Persistence
{
    public class LoginUserDao : AdoDao, ILoginUserDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        /// <summary>
        /// 取得後台使用者 By 使用者Id
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <returns>後台使用者</returns>
        public LoginUserVO GetLoginUserById(string userId)
        {
            return NHibernateDao.GetVOById<LoginUserVO>(userId);
        }

        /// <summary>
        /// 更新後台使用者
        /// </summary>
        /// <param name="loginUserVO">被更新的後台使用者</param>
        /// <returns>更新後的後台使用者</returns>
        public LoginUserVO UpdateLoginUser(LoginUserVO loginUserVO)
        {
            NHibernateDao.Update(loginUserVO);

            return loginUserVO;
        }

        /// <summary>
        /// 取得後台使用者清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <returns>後台使用者清單</returns>
        public IList<LoginUserVO> GetLoginUserList(string queryString)
        {
            return NHibernateDao.SearchByWhere<LoginUserVO>(queryString);
        }

        /// <summary>
        /// 刪除後台使用者
        /// </summary>
        /// <param name="loginUserVO">被刪除的後台使用者</param>
        public void DeleteLoginUser(LoginUserVO loginUserVO)
        {
            NHibernateDao.Delete(loginUserVO);
        }

        /// <summary>
        /// 新增後台使用者
        /// </summary>
        /// <param name="loginUserVO">被新增的後台使用者</param>
        /// <returns>新增後的後台使用者</returns>
        public LoginUserVO CreateLoginUser(LoginUserVO loginUserVO)
        {
            NHibernateDao.Insert(loginUserVO);

            return loginUserVO;
        }

        /// <summary>
        /// 取得全部的後台使用者清單
        /// </summary>
        /// <returns>全部的後台使用者清單</returns>
        public IList<LoginUserVO> GetAllLoginUserList()
        {
            return NHibernateDao.GetAllVO<LoginUserVO>();
        }
    }
}

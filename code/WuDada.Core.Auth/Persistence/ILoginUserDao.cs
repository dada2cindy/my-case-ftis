using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Auth.Domain;

namespace WuDada.Core.Auth.Persistence
{
    public interface ILoginUserDao
    {
        /// <summary>
        /// 取得後台使用者 By 使用者Id
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <returns>後台使用者</returns>
        LoginUserVO GetLoginUserById(string userId);

        /// <summary>
        /// 更新後台使用者
        /// </summary>
        /// <param name="loginUserVO">被更新的後台使用者</param>
        /// <returns>更新後的後台使用者</returns>
        LoginUserVO UpdateLoginUser(LoginUserVO loginUserVO);

        /// <summary>
        /// 取得後台使用者清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <returns>後台使用者清單</returns>
        IList<LoginUserVO> GetLoginUserList(string queryString);

        /// <summary>
        /// 刪除後台使用者
        /// </summary>
        /// <param name="loginUserVO">被刪除的後台使用者</param>
        void DeleteLoginUser(LoginUserVO loginUserVO);

        /// <summary>
        /// 新增後台使用者
        /// </summary>
        /// <param name="loginUserVO">被新增的後台使用者</param>
        /// <returns>新增後的後台使用者</returns>
        LoginUserVO CreateLoginUser(LoginUserVO loginUserVO);

        /// <summary>
        /// 取得全部的後台使用者清單
        /// </summary>
        /// <returns>全部的後台使用者清單</returns>
        IList<LoginUserVO> GetAllLoginUserList();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Auth.Domain;

namespace WuDada.Core.Auth.Service
{
    public interface IAuthService
    {
        /// <summary>
        /// 取得全部的後台角色清單
        /// </summary>
        /// <returns>全部的後台角色清單</returns>
        IList<LoginRoleVO> GetAllLoginRoleList();

        /// <summary>
        /// 取出為第一層的後台功能
        /// </summary>
        /// <returns>第一層的後台功能</returns>
        IList<MenuFuncVO> GetTopMenuFunc();

        /// <summary>
        /// 取得屬於這個User的權限的功能清單
        /// </summary>
        /// <returns>屬於這個User的權限的功能清單</returns>
        IList<MenuFuncVO> GetTopMenuFunc(LoginUserVO user, IList<MenuFuncVO> allMenu, Dictionary<int, LoginRoleVO> roleDic);

        /// <summary>
        /// 取得後台使用者 By 使用者Id
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <returns>後台使用者</returns>
        LoginUserVO GetLoginUserById(string userId);

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="userId">使用者密碼</param>
        /// <returns>後台使用者</returns>
        LoginUserVO Login(string userId, string password);

        /// <summary>
        /// 新增後台功能
        /// </summary>
        /// <param name="menuFuncVO">被新增的後台功能</param>
        /// <returns>新增後的後台功能</returns>
        MenuFuncVO CreateMenuFunc(MenuFuncVO menuFuncVO);

        /// <summary>
        /// 新增後台角色
        /// </summary>
        /// <param name="loginRoleVO">被新增的後台角色</param>
        /// <returns>新增後的後台角色</returns>
        LoginRoleVO CreateLoginRole(LoginRoleVO loginRoleVO);

        /// <summary>
        /// 新增後台使用者
        /// </summary>
        /// <param name="loginUserVO">被新增的後台使用者</param>
        /// <returns>新增後的後台使用者</returns>
        LoginUserVO CreateLoginUser(LoginUserVO loginUserVO);

        /// <summary>
        /// 取出不為第一層的後台功能
        /// </summary>
        /// <returns>不為第一層的後台功能</returns>
        IList<MenuFuncVO> GetNotTopMenuFunc();

        /// <summary>
        /// 取出子後台功能清單 By 父層Id
        /// </summary>
        /// <param name="parentId">父層Id</param>
        /// <returns>子後台功能清單</returns>
        IList<MenuFuncVO> GetMenuFuncByParentId(int parentId);

        /// <summary>
        /// 取得後台功能 By 功能Id
        /// </summary>
        /// <param name="menuFuncId">功能Id</param>
        /// <returns>後台功能</returns>
        MenuFuncVO GetMenuFuncById(int menuFuncId);

        /// <summary>
        /// 取得全部的後台角色清單 No Lazy
        /// </summary>
        /// <returns>全部的後台角色清單</returns>
        IList<LoginRoleVO> GetAllLoginRoleListNoLazy();

        /// <summary>
        /// 取出為第一層的後台功能 No Lazy
        /// </summary>
        /// <returns>第一層的後台功能</returns>
        IList<MenuFuncVO> GetTopMenuFuncNoLazy();

        /// <summary>
        /// 取得後台使用者 By 使用者Id No Lazy
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <returns>後台使用者</returns>
        LoginUserVO GetLoginUserByIdNoLazy(string userId);

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
        /// 取得後台角色 By 角色Id
        /// </summary>
        /// <param name="roleId">使用者Id</param>
        /// <returns>後台使用者</returns>
        LoginRoleVO GetLoginRoleById(int roleId);

        /// <summary>
        /// 取得後台角色 By 角色Id No Lazy
        /// </summary>
        /// <param name="roleId">使用者Id</param>
        /// <returns>後台使用者</returns>
        LoginRoleVO GetLoginRoleByIdNoLazy(int roleId);

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

        /// <summary>
        /// 取得全部的後台使用者清單
        /// </summary>
        /// <returns>全部的後台使用者清單</returns>
        IList<LoginUserVO> GetAllLoginUserList();

        /// <summary>
        /// 判斷後台角色是否有此功能的權限
        /// </summary>
        /// <param name="loginRoleVO">後台角色</param>
        /// <param name="subMenuFuncVO">後台功能</param>
        /// <returns>群組是否有此功能清單的權限</returns>
        bool RoleHasMenuRight(LoginRoleVO loginRoleVO, MenuFuncVO subMenuFuncVO);

        /// <summary>
        /// 判斷路徑是否有權限 
        /// </summary>
        /// <param name="loginUserVO">後台使用者</param>
        /// <param name="uri">路徑</param>
        /// <returns>路徑是否有權限 </returns>
        bool PathHasAuth(LoginUserVO loginUserVO, Uri uri);

        /// <summary>
        /// 取得後台功能清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        IList<MenuFuncVO> GetMenuFuncList(string queryString, int pageIndex, int pageSize);
    }
}

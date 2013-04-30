using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Auth.Persistence;
using WuDada.Core.Auth.Domain;
using NHibernate;

namespace WuDada.Core.Auth.Service.Impl
{
    public class AuthService : IAuthService
    {
        public ILoginRoleDao LoginRoleDao { get; set; }
        public ILoginUserDao LoginUserDao { get; set; }
        public IMenuFuncDao MenuFuncDao { get; set; }

        /// <summary>
        /// 取得全部的後台角色清單
        /// </summary>
        /// <returns>全部的後台角色清單</returns>
        public IList<LoginRoleVO> GetAllLoginRoleList()
        {
            return LoginRoleDao.GetAllLoginRoleList();
        }

        /// <summary>
        /// 取出為第一層的後台功能
        /// </summary>
        /// <returns>第一層的後台功能</returns>
        public IList<MenuFuncVO> GetTopMenuFunc()
        {
            return MenuFuncDao.GetTopMenuFunc();
        }

        /// <summary>
        /// 取得後台使用者 By 使用者Id
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <returns>後台使用者</returns>
        public LoginUserVO GetLoginUserById(string userId)
        {
            return LoginUserDao.GetLoginUserById(userId);
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="userId">使用者密碼</param>
        /// <returns>後台使用者</returns>
        public LoginUserVO Login(string userId, string password)
        {
            LoginUserVO loginUserVO = GetLoginUserById(userId);
            if (loginUserVO != null && loginUserVO.Password.Equals(password))
            {
                return loginUserVO;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 新增後台功能
        /// </summary>
        /// <param name="menuFuncVO">被新增的後台功能</param>
        /// <returns>新增後的後台功能</returns>
        public MenuFuncVO CreateMenuFunc(MenuFuncVO menuFuncVO)
        {
            return MenuFuncDao.CreateMenuFunc(menuFuncVO);                
        }

        /// <summary>
        /// 新增後台角色
        /// </summary>
        /// <param name="loginRoleVO">被新增的後台角色</param>
        /// <returns>新增後的後台角色</returns>
        public LoginRoleVO CreateLoginRole(LoginRoleVO loginRoleVO)
        {
            return LoginRoleDao.CreateLoginRole(loginRoleVO);  
        }

        /// <summary>
        /// 新增後台使用者
        /// </summary>
        /// <param name="loginUserVO">被新增的後台使用者</param>
        /// <returns>新增後的後台使用者</returns>
        public LoginUserVO CreateLoginUser(LoginUserVO loginUserVO)
        {
            return LoginUserDao.CreateLoginUser(loginUserVO); 
        }

        /// <summary>
        /// 取出不為第一層的後台功能
        /// </summary>
        /// <returns>不為第一層的後台功能</returns>
        public IList<MenuFuncVO> GetNotTopMenuFunc()
        {
            return MenuFuncDao.GetNotTopMenuFunc();
        }

        /// <summary>
        /// 取出子後台功能清單 By 父層Id
        /// </summary>
        /// <param name="parentId">父層Id</param>
        /// <returns>子後台功能清單</returns>
        public IList<MenuFuncVO> GetMenuFuncByParentId(int parentId)
        {
            return MenuFuncDao.GetMenuFuncByParentId(parentId);
        }

        /// <summary>
        /// 取得後台功能 By 功能Id
        /// </summary>
        /// <param name="menuFuncId">功能Id</param>
        /// <returns>後台功能</returns>
        public MenuFuncVO GetMenuFuncById(int menuFuncId)
        {
            return MenuFuncDao.GetMenuFuncById(menuFuncId);
        }

        /// <summary>
        /// 取得全部的後台角色清單 No Lazy
        /// </summary>
        /// <returns>全部的後台角色清單</returns>
        public IList<LoginRoleVO> GetAllLoginRoleListNoLazy()
        {
            IList<LoginRoleVO> list = LoginRoleDao.GetAllLoginRoleList();

            if (list != null && list.Count > 0)
            {
                foreach (LoginRoleVO role in list)
                {
                    NHibernateUtil.Initialize(role.MenuFuncList);
                }
            }

            return list;
        }

        /// <summary>
        /// 取出為第一層的後台功能 No Lazy
        /// </summary>
        /// <returns>第一層的後台功能</returns>
        public IList<MenuFuncVO> GetTopMenuFuncNoLazy()
        {
            IList<MenuFuncVO> list = MenuFuncDao.GetTopMenuFunc();

            if (list != null && list.Count > 0)
            {
                foreach (MenuFuncVO menu in list)
                {
                    NHibernateUtil.Initialize(menu.SubFuncs);

                    if (menu.SubFuncs != null && menu.SubFuncs.Count > 0)
                    {
                        foreach (MenuFuncVO subFunc in menu.SubFuncs)
                        {
                            NHibernateUtil.Initialize(subFunc.FuncionPaths);
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 取得後台使用者 By 使用者Id No Lazy
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <returns>後台使用者</returns>
        public LoginUserVO GetLoginUserByIdNoLazy(string userId)
        {
            LoginUserVO user = LoginUserDao.GetLoginUserById(userId);

            if (user != null)
            {
                NHibernateUtil.Initialize(user.LoginRoleList);

                if (user.LoginRoleList != null && user.LoginRoleList.Count > 0)
                {
                    foreach (LoginRoleVO role in user.LoginRoleList)
                    {
                        NHibernateUtil.Initialize(role.MenuFuncList);

                        if (role.MenuFuncList != null && role.MenuFuncList.Count > 0)
                        {
                            foreach (MenuFuncVO menufunc in role.MenuFuncList)
                            {
                                NHibernateUtil.Initialize(menufunc.FuncionPaths);
                            }
                        }
                    }
                }
            }

            return user;
        }

        /// <summary>
        /// 取得屬於這個User的權限的功能清單
        /// </summary>
        /// <returns>屬於這個User的權限的功能清單</returns>
        public IList<MenuFuncVO> GetTopMenuFunc(LoginUserVO user, IList<MenuFuncVO> allMenu, Dictionary<int, LoginRoleVO> roleDic)
        {
            IList<MenuFuncVO> authMenuList = new List<MenuFuncVO>();

            foreach (MenuFuncVO menu in allMenu)
            {
                if (menu.SubFuncs.Count > 0)
                {
                    int i = 1;  //判斷是否第一次執行
                    MenuFuncVO parentMenu = new MenuFuncVO();

                    foreach (MenuFuncVO subFunc in menu.SubFuncs)
                    {
                        if (UserHasMenuRight(user, subFunc, roleDic))
                        {
                            if (i == 1)
                            {
                                parentMenu = menu;
                                authMenuList.Add(parentMenu);
                                parentMenu.SubFuncs = new List<MenuFuncVO>();
                            }
                            parentMenu.SubFuncs.Add(subFunc);
                            i++;
                        }
                    }
                }
            }
            return authMenuList;
        }

        /// <summary>
        /// 檢查使用者是否有此單一清單的權限
        /// </summary>
        /// <param name="user"></param>
        /// <param name="subFunc"></param>
        /// <returns></returns>
        private bool UserHasMenuRight(LoginUserVO user, MenuFuncVO subFunc, Dictionary<int, LoginRoleVO> roleDic)
        {
            if (user.LoginRoleList != null && user.LoginRoleList.Count > 0)
            {
                foreach (LoginRoleVO role in user.LoginRoleList)
                {
                    LoginRoleVO cacheRole = roleDic[role.RoleId];

                    if (RoleHasMenuRight(cacheRole, subFunc))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 群組是否有此功能清單的權限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="subFunc"></param>
        /// <returns></returns>
        private bool RoleHasMenuRight(LoginRoleVO role, MenuFuncVO subFunc)
        {
            foreach (MenuFuncVO roleHasFunc in role.MenuFuncList)
            {
                if (roleHasFunc.MenuFuncId.Equals(subFunc.MenuFuncId))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 更新後台使用者
        /// </summary>
        /// <param name="loginUserVO">被更新的後台使用者</param>
        /// <returns>更新後的後台使用者</returns>
        public LoginUserVO UpdateLoginUser(LoginUserVO loginUserVO)
        {
            return LoginUserDao.UpdateLoginUser(loginUserVO);
        }

        /// <summary>
        /// 取得後台使用者清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <returns>後台使用者清單</returns>
        public IList<LoginUserVO> GetLoginUserList(string queryString)
        {
            return LoginUserDao.GetLoginUserList(queryString);
        }

        /// <summary>
        /// 刪除後台使用者
        /// </summary>
        /// <param name="loginUserVO">被刪除的後台使用者</param>
        public void DeleteLoginUser(LoginUserVO loginUserVO)
        {
            LoginUserDao.DeleteLoginUser(loginUserVO);
        }

        /// <summary>
        /// 取得後台角色 By 角色Id
        /// </summary>
        /// <param name="roleId">使用者Id</param>
        /// <returns>後台使用者</returns>
        public LoginRoleVO GetLoginRoleById(int roleId)
        {
            return LoginRoleDao.GetLoginRoleById(roleId);
        }

        /// <summary>
        /// 取得後台角色 By 角色Id No Lazy
        /// </summary>
        /// <param name="roleId">使用者Id</param>
        /// <returns>後台使用者</returns>
        public LoginRoleVO GetLoginRoleByIdNoLazy(int roleId)
        {
            LoginRoleVO role = LoginRoleDao.GetLoginRoleById(roleId);

            if (role != null)
            {
                NHibernateUtil.Initialize(role.LoginUserList);
                NHibernateUtil.Initialize(role.MenuFuncList);
            }

            return role;
        }

        /// <summary>
        /// 更新後台角色
        /// </summary>
        /// <param name="loginRoleVO">被更新的後台角色</param>
        /// <returns>更新後的後台角色</returns>
        public LoginRoleVO UpdateLoginRole(LoginRoleVO loginRoleVO)
        {
            return LoginRoleDao.UpdateLoginRole(loginRoleVO);
        }

        /// <summary>
        /// 刪除後台角色
        /// </summary>
        /// <param name="loginRoleVO">被刪除的後台角色</param>
        public void DeleteLoginRole(LoginRoleVO loginRoleVO)
        {
            LoginRoleDao.DeleteLoginRole(loginRoleVO);
        }

        /// <summary>
        /// 取得全部的後台使用者清單
        /// </summary>
        /// <returns>全部的後台使用者清單</returns>
        public IList<LoginUserVO> GetAllLoginUserList()
        {
            return LoginUserDao.GetAllLoginUserList();
        }

        /// <summary>
        /// 判斷後台角色是否有此功能的權限
        /// </summary>
        /// <param name="loginRoleVO">後台角色</param>
        /// <param name="subMenuFuncVO">後台功能</param>
        /// <returns>群組是否有此功能清單的權限</returns>
        bool IAuthService.RoleHasMenuRight(LoginRoleVO loginRoleVO, MenuFuncVO subMenuFuncVO)
        {
            foreach (MenuFuncVO roleHasFunc in loginRoleVO.MenuFuncList)
            {
                if (roleHasFunc.MenuFuncId.Equals(subMenuFuncVO.MenuFuncId))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判斷路徑是否有權限 
        /// </summary>
        /// <param name="loginUserVO">後台使用者</param>
        /// <param name="uri">路徑</param>
        /// <returns>路徑是否有權限 </returns>
        public bool PathHasAuth(LoginUserVO user, Uri uri)
        {
            string path = uri.ToString();

            LoginUserVO loginUserVO = LoginUserDao.GetLoginUserById(user.UserId);
            IList<LoginRoleVO> loginRoleList = loginUserVO.LoginRoleList;

            HashSet<string> pathRightSet = new HashSet<string>();

            foreach (LoginRoleVO role in loginRoleList)
            {
                IList<MenuFuncVO> menuFuncList = role.MenuFuncList;

                foreach (MenuFuncVO menuFunc in menuFuncList)
                {
                    pathRightSet.Add(menuFunc.MainPath);

                    if (menuFunc.FuncionPaths != null && menuFunc.FuncionPaths.Count > 0)
                    {
                        foreach (FunctionPathVO fpth in menuFunc.FuncionPaths)
                        {
                            if (!string.IsNullOrEmpty(fpth.Path))
                            {
                                pathRightSet.Add(fpth.Path);
                            }
                        }
                    }
                }
            }

            pathRightSet.Add("admin/index.aspx");
            if (pathRightSet.Count > 0)
            {
                foreach (string rightPath in pathRightSet.ToArray<string>())
                {
                    if (path.IndexOf(rightPath) != -1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 取得後台功能清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        public IList<MenuFuncVO> GetMenuFuncList(string queryString, int pageIndex, int pageSize)
        {
            return MenuFuncDao.GetMenuFuncList(queryString, pageIndex, pageSize);
        }
    }
}

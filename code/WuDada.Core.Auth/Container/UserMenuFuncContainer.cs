using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Auth.Service;
using NHibernate;

/// <summary>
/// UserMenuFuncContainer 使用 Singleton pattern
/// </summary>
namespace WuDada.Core.Auth.Container
{
    public class UserMenuFuncContainer
    {        
        private static ILog m_Log = null;

        private static UserMenuFuncContainer m_UserMenuFunc = null;

        /// <summary>
        /// 登入者的快取
        /// </summary>
        private Dictionary<string, LoginUserVO> m_UserDic = new Dictionary<string, LoginUserVO>();

        /// <summary>
        /// 路徑與功能
        /// </summary>
        public Dictionary<string, List<int>> PathFunc = new Dictionary<string, List<int>>();
        public Dictionary<int, LoginRoleVO> RoleDic = new Dictionary<int, LoginRoleVO>();
        public IList<MenuFuncVO> AllMenu = null;

        private UserMenuFuncContainer()
        {
            m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static UserMenuFuncContainer GetInstance()
        {
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("....System Init....");

            lock (typeof(UserMenuFuncContainer))
            {
                if (m_UserMenuFunc == null)
                {
                    UserMenuFuncContainer container = new UserMenuFuncContainer();
                    m_UserMenuFunc = container;

                    //載入快取全部的menu
                    container.AllMenu = new List<MenuFuncVO>();

                    initAllMenu();

                    //載入全部的Role的功能
                    initAllRole();

                    initPathFunc();
                }
                return m_UserMenuFunc;
            }
        }

        private static void initPathFunc()
        {
            IList<MenuFuncVO> menuList = m_UserMenuFunc.AllMenu;
            Dictionary<string, List<int>> myPathFunc = m_UserMenuFunc.PathFunc;
            //top menu
            foreach (MenuFuncVO menuFunc in menuList)
            {
                if (menuFunc.SubFuncs != null && menuFunc.SubFuncs.Count > 0)
                {
                    foreach (MenuFuncVO subMenuFunc in menuFunc.SubFuncs)
                    {
                        string path = subMenuFunc.MainPath;
                        if (!string.IsNullOrEmpty(path))
                        {
                            if (!myPathFunc.ContainsKey(path))
                            {
                                List<int> funList = new List<int>();
                                funList.Add(subMenuFunc.MenuFuncId);
                                myPathFunc.Add(path, funList);
                            }
                            else
                            {
                                List<int> funList = myPathFunc[path];
                                funList.Add(subMenuFunc.MenuFuncId);
                            }
                        }
                    }
                }
            }
        }

        private static void initAllRole()
        {
            AuthFactory authFactory = new AuthFactory();
            IAuthService authService = authFactory.GetAuthService();

            IList<LoginRoleVO> roleList = authService.GetAllLoginRoleListNoLazy();
            m_UserMenuFunc.RoleDic = new Dictionary<int, LoginRoleVO>();

            foreach (LoginRoleVO role in roleList)
            {
                m_UserMenuFunc.RoleDic.Add(role.RoleId, role);
            }
        }

        public void ReloadAllMenu()
        {
            initAllMenu();
            initAllRole();
        }

        private static void initAllMenu()
        {
            AuthFactory authFactory = new AuthFactory();
            IAuthService authService = authFactory.GetAuthService();

            IList<MenuFuncVO> iList = authService.GetTopMenuFuncNoLazy();

            m_UserMenuFunc.AllMenu = iList;
        }

        /// <summary>
        /// 重新建立Container
        /// </summary>
        public void ResetAll()
        {
            m_UserMenuFunc = null;
            RoleDic = null;
        }


        /// <summary>
        /// 重新載入使用者(當更換權限時要reload)
        /// </summary>
        /// <param name="userId"></param>
        public void Reload(string userId)
        {
            m_UserMenuFunc.m_UserDic.Remove(userId);
            loadUser(userId);
        }

        public LoginUserVO GetUser(string userId)
        {
            m_UserDic = m_UserMenuFunc.m_UserDic;

            initData(userId);

            return m_UserDic[userId];
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="userId"></param>
        private void initData(string userId)
        {
            m_UserDic = m_UserMenuFunc.m_UserDic;

            if (m_UserDic.Count > 0)
            {
                if (!m_UserDic.ContainsKey(userId))
                {
                    loadUser(userId);
                }
            }
            else
            {
                loadUser(userId);
            }
        }


        /// <summary>
        /// 載入user的資料
        /// </summary>
        /// <param name="userId"></param>
        private void loadUser(string userId)
        {
            m_UserDic = m_UserMenuFunc.m_UserDic;

            AuthFactory authFactory = new AuthFactory();
            IAuthService authService = authFactory.GetAuthService();

            LoginUserVO user = authService.GetLoginUserByIdNoLazy(userId);
            
            m_Log.Debug("lock UserMenuFuncContainer loadUser");
            lock (typeof(UserMenuFuncContainer))
            {
                m_UserDic.Remove(userId);
                m_UserDic.Add(userId, user);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using FTIS.Domain.Impl;
using FTIS.Service;
using WuDada.Core.Generic.Extension;

namespace FTIS.Domain.Container
{
    public class LoginUserContainer
    {
        private static ILog m_Log = null;

        private static LoginUserContainer m_LoginUserContainer = null;

        /// <summary>
        /// 登入者的快取
        /// </summary>
        private Dictionary<string, MasterMember> m_UserDic = new Dictionary<string, MasterMember>();

        private LoginUserContainer()
        {
            m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static LoginUserContainer GetInstance()
        {
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("....LoginUserContainer Init....");

            lock (typeof(LoginUserContainer))
            {
                if (m_LoginUserContainer == null)
                {
                    LoginUserContainer container = new LoginUserContainer();
                    m_LoginUserContainer = container;

                    LoadAllUser();
                }
                return m_LoginUserContainer;
            }
        }

        private static void LoadAllUser()
        {
            FTISFactory ftisFactory = new FTISFactory();
            IFTISService ftisService = ftisFactory.GetFTISService();

            IDictionary<string, string> conditions = new Dictionary<string, string>();
            IList<MasterMember> userList = ftisService.GetMasterMemberListNoLazy(conditions);

            if (userList != null && userList.Count > 0)
            {
                foreach (MasterMember user in userList)
                {
                    m_LoginUserContainer.m_UserDic.Add(user.Account, user);
                }
            }
        }

        public MasterMember GetUser(string userId)
        {
            InitData(userId);

            return m_LoginUserContainer.m_UserDic[userId];
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="userId"></param>
        public void InitMember(string userId)
        {
            LoadUser(userId);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="userId"></param>
        private void InitData(string userId)
        {
            if (m_LoginUserContainer.m_UserDic.Count > 0)
            {
                if (!m_LoginUserContainer.m_UserDic.ContainsKey(userId))
                {
                    LoadUser(userId);
                }
            }
            else
            {
                LoadUser(userId);
            }
        }

        /// <summary>
        /// 載入user的資料
        /// </summary>
        /// <param name="userId"></param>
        private void LoadUser(string userId)
        {
            FTISFactory ftisFactory = new FTISFactory();
            IFTISService ftisService = ftisFactory.GetFTISService();

            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("Account", userId);
            IList<MasterMember> userList = ftisService.GetMasterMemberListNoLazy(conditions);

            m_Log.Debug("lock LoginUserContainer loadUser");
            lock (typeof(LoginUserContainer))
            {
                m_LoginUserContainer.m_UserDic.Remove(userId);
                m_LoginUserContainer.m_UserDic.Add(userId, userList[0]);
            }
        }
    }
}

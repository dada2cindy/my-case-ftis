using System;
using System.Collections.Generic;
using NHibernate;

namespace WuDada.Core.Helper
{
    public sealed class NHibernateHelper
    {
        private const string m_CurrentSessionKey = "nhibernate.current_session";
        private static readonly ISessionFactory m_SessionFactory;

        [ThreadStatic]
        private static Dictionary<string, ISession> m_Dictionary = new Dictionary<string, ISession>();

        static NHibernateHelper()
        {           
            m_SessionFactory = SpringHelper.ApplicationContext["NHibernateSessionFactory"] as ISessionFactory;
        }

        public static ISession GetCurrentSession()
        {
            ISession currentSession;

            if (m_Dictionary == null)
            {
                m_Dictionary = new Dictionary<string, ISession>();
            }

            if (!m_Dictionary.ContainsKey(m_CurrentSessionKey))
            {
                currentSession = m_SessionFactory.OpenSession();
                m_Dictionary.Add(m_CurrentSessionKey, currentSession);
            }
            else
            {
                currentSession = m_Dictionary[m_CurrentSessionKey] as ISession;
            }
            return currentSession;
        }

        public static void CloseSession()
        {
            if (m_Dictionary.Count == 0) return;

            ISession currentSession = m_Dictionary[m_CurrentSessionKey] as ISession;

            if (currentSession == null)
            {

                return;
            }

            currentSession.Close();
            m_Dictionary.Remove(m_CurrentSessionKey);
        }

        public static void CloseSessionFactory()
        {
            if (m_SessionFactory != null)
            {
                m_SessionFactory.Close();
            }
        }
    }
}

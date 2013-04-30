using WuDada.Core.Helper;
using WuDada.Core.Persistence;
using WuDada.Core.Persistence.Ado;
using log4net.Config;
using Spring.Context;
using Spring.Context.Support;
using NHibernate.Impl;
using NHibernate;
using System.Collections.Generic;
using NHibernate.Cfg;
using System.Collections;

namespace WuDada.Core.SystemApplications.Test
{
    public class TestHelper
    {
        public TestHelper()
        {
            XmlConfigurator.Configure();
        }

        public TestHelper(IApplicationContext applicationContext)
        {
            SpringHelper.ApplicationContext = applicationContext;
            XmlConfigurator.Configure();
        }

        public INHibernateDao GetNHibernateDao()
        {
            return SpringHelper.ApplicationContext["NHibernateDao"] as INHibernateDao;
        }

        public AdoDao GetAdoDao()
        {
            return SpringHelper.ApplicationContext["AdoDao"] as AdoDao;
        }

        public global::NHibernate.Cfg.Configuration GetCfg(string[] addAssemblies)
        {            
            SessionFactoryImpl sessionFactoryImpl = SpringHelper.ApplicationContext["NHibernateSessionFactory"] as SessionFactoryImpl;
            IDictionary springObjectDic = getSpringObjectPropertyValue("NHibernateSessionFactory", "HibernateProperties") as IDictionary;
            IDictionary<string, string> dic = new Dictionary<string, string>();            
            foreach (DictionaryEntry de in springObjectDic)
            {
                dic.Add(de.Key.ToString(), de.Value.ToString());
                //if (de.Key.ToString().Equals("hibernate.dialect"))
                //{
                //    dialectName = de.Value.ToString();
                //}
            }

            #region //真正抓取設定檔的內容
            ISession session = sessionFactoryImpl.OpenSession();
            string connectionStr = session.Connection.ConnectionString;
            #endregion

            dic.Add("connection.connection_string", connectionStr);
            Configuration cfg = new Configuration();
            cfg.AddProperties(dic);

            foreach (string assembly in addAssemblies)
            {
                cfg.AddAssembly(assembly);
            }

            return cfg;
        }

        private object getSpringObjectPropertyValue(string objectName, string propertyName)
        {
            IApplicationContext ctx = SpringHelper.ApplicationContext;

            Spring.Objects.Factory.Config.IObjectDefinition def =
                ((IConfigurableApplicationContext)ctx).ObjectFactory.GetObjectDefinition(objectName);
            return def.PropertyValues.GetPropertyValue(propertyName).Value;
        }
    }
}

using WuDada.Core.Helper;
using WuDada.Core.Service;
using log4net.Config;
using Spring.Context;

namespace WuDada.Core
{
    public class CoreFactory
    {
        public CoreFactory()
        {
            XmlConfigurator.Configure();
        }
        public CoreFactory(IApplicationContext applicationContext)
        {
            SpringHelper.ApplicationContext = applicationContext;
            XmlConfigurator.Configure();
        }

        public INHibernateService GetNHibernateService()
        {
            return SpringHelper.ApplicationContext["NHibernateServiceProxy"] as INHibernateService;
        }
    }
}

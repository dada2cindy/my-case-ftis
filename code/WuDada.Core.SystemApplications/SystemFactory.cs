using WuDada.Core.Helper;
using log4net.Config;
using Spring.Context;
using WuDada.Core.SystemApplications.Service;

namespace WuDada.Core.SystemApplications
{
    public class SystemFactory
    {
        public SystemFactory()
        {
            XmlConfigurator.Configure();
        }
        public SystemFactory(IApplicationContext applicationContext)
        {
            SpringHelper.ApplicationContext = applicationContext;
            XmlConfigurator.Configure();
        }

        public ISystemService GetSystemService()
        {
            return SpringHelper.ApplicationContext["SystemServiceProxy"] as ISystemService;
        }

        public ILogService GetLogService()
        {
            return SpringHelper.ApplicationContext["LogServiceProxy"] as ILogService;
        }

        public ITemplateService GetTemplateService()
        {
            return SpringHelper.ApplicationContext["TemplateServiceProxy"] as ITemplateService;
        }
    }
}

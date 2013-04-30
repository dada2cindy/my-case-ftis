using WuDada.Core.Helper;
using log4net.Config;
using Spring.Context;
using WuDada.Core.Common.Service;

namespace WuDada.Core.Common
{
    public class CommonFactory
    {
        public CommonFactory()
        {
            XmlConfigurator.Configure();
        }

        public CommonFactory(IApplicationContext applicationContext)
        {
            SpringHelper.ApplicationContext = applicationContext;
            XmlConfigurator.Configure();
        }

        public ICommonService GetCommonService()
        {
            return SpringHelper.ApplicationContext["CommonServiceProxy"] as ICommonService;
        }
    }
}

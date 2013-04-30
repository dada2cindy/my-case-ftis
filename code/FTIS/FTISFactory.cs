using WuDada.Core.Helper;
using log4net.Config;
using Spring.Context;
using FTIS.Service;
//using FTIS.Service;

namespace FTIS
{
    public class FTISFactory
    {
        public FTISFactory()
        {
            XmlConfigurator.Configure();
        }

        public FTISFactory(IApplicationContext applicationContext)
        {
            SpringHelper.ApplicationContext = applicationContext;
            XmlConfigurator.Configure();
        }

        public IFTISService GetFTISService()
        {
            return SpringHelper.ApplicationContext["FTISServiceProxy"] as IFTISService;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Config;
using Spring.Context;
using WuDada.Core.Helper;
using WuDada.Core.Auth.Service;

namespace WuDada.Core.Auth
{
    public class AuthFactory
    {
        public AuthFactory()
        {
            XmlConfigurator.Configure();
        }

        public AuthFactory(IApplicationContext applicationContext)
        {
            SpringHelper.ApplicationContext = applicationContext;
            XmlConfigurator.Configure();
        }

        public IAuthService GetAuthService()
        {
            return SpringHelper.ApplicationContext["AuthServiceProxy"] as IAuthService;
        }
    }
}

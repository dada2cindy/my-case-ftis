using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using log4net.Config;
using WuDada.Core.Helper;
using WuDada.Core.Post.Service;

namespace WuDada.Core.Post
{
    public class PostFactory
    {
        public PostFactory()
        {
            XmlConfigurator.Configure();
        }

        public PostFactory(IApplicationContext applicationContext)
        {
            SpringHelper.ApplicationContext = applicationContext;
            XmlConfigurator.Configure();
        }

        public IPostService GetPostService()
        {
            return SpringHelper.ApplicationContext["PostServiceProxy"] as IPostService;
        }

        public IMessageService GetMessageService()
        {
            return SpringHelper.ApplicationContext["MessageServiceProxy"] as IMessageService;
        }
    }
}

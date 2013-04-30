using Spring.Context;
using Spring.Context.Support;

namespace WuDada.Core.Helper
{
    public class SpringHelper
    {
        private static IApplicationContext m_Ctx = null;

        public static IApplicationContext ApplicationContext
        {
            get
            {
                if (m_Ctx == null)
                {
                    m_Ctx = ContextRegistry.GetContext();
                }
                return m_Ctx;
            }
            set
            {
                m_Ctx = value;
            }
        }
    }
}

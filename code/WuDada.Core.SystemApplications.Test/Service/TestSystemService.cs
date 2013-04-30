using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WuDada.Core.SystemApplications.Domain;
using WuDada.Core.SystemApplications.Service;
using NUnit.Framework;

namespace WuDada.Core.SystemApplications.Test.Service
{
    [TestFixture]
    public class TestSystemService
    {
        private SystemFactory m_SystemFactory { get; set; }
        private ISystemService m_SystemService { get; set; }

        [TestFixtureSetUp]
        public void TestCaseInit()
        {
            m_SystemFactory = new SystemFactory();
            m_SystemService = m_SystemFactory.GetSystemService();
        }

        [Test]
        public void Test_002_GetAllItemParamList()
        {
            Console.WriteLine("start = " + DateTime.Now);
            IList<ItemParamVO> itemParamList = m_SystemService.GetAllItemParamList(0, 10, "ItemParamId", true);
            Console.WriteLine("end = " + DateTime.Now);
            Console.WriteLine("count = " + itemParamList.Count());

            Console.WriteLine("start = " + DateTime.Now);
            IList<ItemParamVO> itemParamList2 = m_SystemService.GetAllItemParamList();
            Console.WriteLine("end = " + DateTime.Now);
            Console.WriteLine("count = " + itemParamList2.Count());
        }

        [Test]
        public void Test_003_CreateItemParam()
        {
            Console.WriteLine("start = " + DateTime.Now);
            for (int i = 0; i < 10; i++)
            {
                m_SystemService.CreateItemParam(new ItemParamVO("a" + i, "b" + i, "c" + i, false));
            }
            Console.WriteLine("end = " + DateTime.Now);
        } 
    }
}

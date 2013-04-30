using System;
using System.Collections.Generic;
using WuDada.Core.Persistence;
using WuDada.Core.Persistence.NHibernate;
using WuDada.Core.SystemApplications.Domain;
using NUnit.Framework;

namespace WuDada.Core.SystemApplications.Test.Persistence
{
    [TestFixture]
    public class TestNHibernateDao
    {
        public TestHelper TestHelper { get; set; }
        INHibernateDao NHibernateDao = null;

        [TestFixtureSetUp]
        public void TestCaseInit()
        {
            TestHelper = new TestHelper();
            NHibernateDao = TestHelper.GetNHibernateDao();
        }

        [Test]
        public void Test_001_GetAllVO()
        {
            IList<ItemParamVO> itemParamList = NHibernateDao.GetAllVO<ItemParamVO>();

            if (itemParamList != null && itemParamList.Count > 0)
            {
                foreach (ItemParamVO itemParamVO in itemParamList)
                {
                    Console.Out.WriteLine("itemParamVO.Id = " + itemParamVO.ItemParamId);
                }
            }
        }

        [Test]
        public void Test_002_GetVOById()
        {
            ItemParamVO itemParamVO = NHibernateDao.GetVOById<ItemParamVO>(1);

            if (itemParamVO != null )
            {
                Console.Out.WriteLine("itemParamVO.Id = " + itemParamVO.ItemParamId);
            }
        }        

        [Test]
        public void Test_003_Insert()
        {
            ItemParamVO itemParamVO = new ItemParamVO("aaa", "bbb", "ccc", false);
            NHibernateDao.Insert(itemParamVO);

            if (itemParamVO != null)
            {
                Console.Out.WriteLine("itemParamVO.Id = " + itemParamVO.ItemParamId);
            }
        }
    }
}

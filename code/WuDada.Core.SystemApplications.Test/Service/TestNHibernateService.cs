using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WuDada.Core.Helper;
using WuDada.Core.Service;
using WuDada.Core.SystemApplications.Domain;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NUnit.Framework;

namespace WuDada.Core.SystemApplications.Test.Service
{
    [TestFixture]
    public class TestNHibernateService
    {
        private CoreFactory m_CoreFactory { get; set; }
        private INHibernateService m_NHibernateService { get; set; }

        [TestFixtureSetUp]
        public void TestCaseInit()
        {
            m_CoreFactory = new CoreFactory();
            m_NHibernateService = m_CoreFactory.GetNHibernateService();
        }

        [Test]
        public void Test_001_ExecutableDetachedCriteria()
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<ItemParamVO>();
            //dCriteria.Add(Expression.Like("Name", "1", MatchMode.Anywhere));

            int count = m_NHibernateService.CountByDetachedCriteria(dCriteria);

            Console.Out.WriteLine("count = " + count);

            IList<ItemParamVO> itemParamList = m_NHibernateService.SearchByDetachedCriteria<ItemParamVO>(dCriteria);

            if (itemParamList != null && itemParamList.Count > 0)
            {
                foreach (ItemParamVO itemParamVO in itemParamList)
                {
                    Console.Out.WriteLine("itemParamVO.Id = " + itemParamVO.ItemParamId);
                }
            }

            itemParamList = m_NHibernateService.SearchByDetachedCriteria<ItemParamVO>(dCriteria, 0, 10);

            if (itemParamList != null && itemParamList.Count > 0)
            {
                foreach (ItemParamVO itemParamVO in itemParamList)
                {
                    Console.Out.WriteLine("itemParamVO.Id = " + itemParamVO.ItemParamId);
                }
            }
        }

        [Test]
        public void Test_002_Update()
        {
            ItemParamVO itemParamVO = new ItemParamVO("dgg", "33e", "tete", true);
            m_NHibernateService.Insert(itemParamVO);

            itemParamVO.Name = "dafdafee";

            m_NHibernateService.Update(itemParamVO);
        }

        [Test]
        public void Test_003_SearchByLinq()
        {         
            ISession session = NHibernateHelper.GetCurrentSession();

            var linqQuery = (from itemParamVO in session.Query<ItemParamVO>()
                                   //where itemParamVO.Name == "bbb"
                                   orderby itemParamVO.ItemParamId ascending
                                   select itemParamVO)
                                   .Skip(10).Take(2); //跳過前10項後取其他的前2項

            var itemParamList = linqQuery.ToList();
            //linqQuery.OfType<ItemParamVO>().ToList();
            
            if (itemParamList != null && itemParamList.Count > 0)
            {
                foreach (ItemParamVO itemParamVO in itemParamList)
                {
                    Console.Out.WriteLine("itemParamVO.Id = " + itemParamVO.ItemParamId);
                }
            }

            NHibernateHelper.CloseSession();
            NHibernateHelper.CloseSessionFactory();
        }

        [Test]
        public void Test_004_Insert()
        {
            ItemParamVO itemParamVO = new ItemParamVO("aadfdbb", "iiii", "oooo", true);

            m_NHibernateService.Insert(itemParamVO);
        }
    }
}

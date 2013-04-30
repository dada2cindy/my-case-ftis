using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WuDada.Core.SystemApplications.Domain;
using WuDada.Core.SystemApplications.Service;
using NUnit.Framework;
using WuDada.Core.Common;
using WuDada.Core.Common.Service;

namespace WuDada.Core.SystemApplications.Test.Service
{
    [TestFixture]
    public class TestCommonService
    {
        private CommonFactory m_CommonFactory { get; set; }
        private ICommonService m_CommonService { get; set; }

        [TestFixtureSetUp]
        public void TestCaseInit()
        {
            m_CommonFactory = new CommonFactory();
            m_CommonService = m_CommonFactory.GetCommonService();
        }

        [Test]
        public void Test_001_CreateSer_Code_And_Num()
        {
            string code = "bu";
            int numLength = 8;

            string serial = m_CommonService.CreateSer_Code_And_Num(code, numLength);

            Console.WriteLine("serial = " + serial);
        }

        
    }
}

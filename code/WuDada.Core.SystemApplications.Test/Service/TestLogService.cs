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
    public class TestLogService
    {
        private SystemFactory m_SystemFactory { get; set; }
        private ILogService m_LogService { get; set; }

        [TestFixtureSetUp]
        public void TestCaseInit()
        {
            m_SystemFactory = new SystemFactory();
            m_LogService = m_SystemFactory.GetLogService();
        }        

        [Test]
        public void Test_CreateLogSystem()
        {
            LogSystemVO logVO = new LogSystemVO();
            logVO.Action = MsgVO.LogTitleName.登入記錄.ToString();
            logVO.Fucntion = MsgVO.LogTitleName.登入記錄.ToString();
            logVO.SubFucntion = MsgVO.LogTitleName.登入記錄.ToString();
            logVO.IpAddress = "127.0.0.1";
            logVO.UpdateId = "admin";
            logVO.UpdateDate = DateTime.Now;
            m_LogService.CreateLogSystem(logVO);
        }

        [Test]
        public void Test_GetLogSystemList()
        {
            Console.WriteLine("start = " + DateTime.Now);
            IList<LogSystemVO> logSystemList = m_LogService.GetLogSystemList("UpdateId = 'admin' ORDER BY LogSystemId");
            Console.WriteLine("end = " + DateTime.Now);
            Console.WriteLine("count = " + logSystemList.Count());

            foreach (LogSystemVO logSystemVO in logSystemList)
            {
                Console.WriteLine("id = " + logSystemVO.LogSystemId);
            }

            Console.WriteLine("start2 = " + DateTime.Now);
            IList<LogSystemVO> logSystemList2 = m_LogService.GetLogSystemList("UpdateId = 'admin' ORDER BY UpdateDate DESC", 0, 10);
            Console.WriteLine("end2 = " + DateTime.Now);
            Console.WriteLine("count = " + logSystemList2.Count());

            foreach (LogSystemVO logSystemVO in logSystemList2)
            {
                Console.WriteLine("id2 = " + logSystemVO.LogSystemId);
            }
        }
    }
}

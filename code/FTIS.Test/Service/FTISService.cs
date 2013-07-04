using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Globalization;
using WuDada.Core.Generic.Mail;
using WuDada.Core.Generic.Util;
using FTIS;
using FTIS.Service;
using FTIS.Domain.Impl;
using FTIS.Domain;
using FTIS.ACUtility;

namespace FOTIS.Test.Service
{
    [TestFixture]
    public class FTISService
    {
        private FTISFactory m_FTISFactory { get; set; }
        private IFTISService m_FTISService { get; set; }

        [TestFixtureSetUp]
        public void TestCaseInit()
        {
            m_FTISFactory = new FTISFactory();
            m_FTISService = m_FTISFactory.GetFTISService();
        }

        [Test]
        public void Test_NewsClass()
        {
            //新增
            string name = "新增NewsClass測試aaaaaaaaaa";
            NewsClass newsClass = CreateNewsClass(name);

            //更新
            string updateName = "更新NewsClass測試bbbbbbbbb";
            NewsClass updateNewsClass = m_FTISService.GetNewsClassById(newsClass.NewsClassId);
            Assert.AreEqual(name, updateNewsClass.Name);
            updateNewsClass.Name = updateName;
            m_FTISService.UpdateNewsClass(updateNewsClass);

            //查詢
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("Name", updateName);
            conditions.Add("Status", "1");
            IList<NewsClass> newsClassList = m_FTISService.GetNewsClassList(conditions);
            Assert.AreEqual(1, newsClassList.Count);
            Assert.AreEqual(1, m_FTISService.GetNewsClassCount(conditions));
            Assert.AreEqual(updateName, newsClassList[0].Name);

            conditions = new Dictionary<string, string>();
            conditions.Add("Name", name);
            conditions.Add("Status", "1");
            newsClassList = m_FTISService.GetNewsClassList(conditions);
            Assert.AreEqual(0, newsClassList.Count);
            Assert.AreEqual(0, m_FTISService.GetNewsClassCount(conditions));

            conditions = new Dictionary<string, string>();
            conditions.Add("Name", updateName);
            conditions.Add("Status", "0");
            newsClassList = m_FTISService.GetNewsClassList(conditions);
            Assert.AreEqual(1, newsClassList.Count);
            Assert.AreEqual(1, m_FTISService.GetNewsClassCount(conditions));

            //刪除
            m_FTISService.DeleteNewsClass(updateNewsClass);
            conditions = new Dictionary<string, string>();
            conditions.Add("Name", updateName);
            conditions.Add("Status", "1");
            newsClassList = m_FTISService.GetNewsClassList(conditions);
            Assert.AreEqual(0, newsClassList.Count);
            Assert.AreEqual(0, m_FTISService.GetNewsClassCount(conditions));
        }

        private NewsClass CreateNewsClass(string name)
        {
            NewsClass newsClass = new NewsClass() { Name = name, LangId = "2", SortId = 250, Status = "1" };
            m_FTISService.CreateNewsClass(newsClass);
            Assert.AreNotEqual(0, newsClass.NewsClassId);
            return newsClass;
        }

        [Test]
        public void Test_News()
        {
            //新增
            string newsClassName = "新增NewsClass測試234reeewwwww";
            NewsClass newsClass = CreateNewsClass(newsClassName);

            string name = "新增News測試adfsfdfeee3vvvv";
            News news = new News()
            {
                NewsClass = newsClass,
                LangId = "2",
                Name = name,
                PostDate = DateTime.Now,
                Content = "123",
                Pic1="Pic1",
                Pic2 = "Pic2",
                Pic3 = "Pic3",
                Pic1Name="Pic1Name",
                Pic2Name = "Pic2Name",
                Pic3Name = "Pic3Name",
                AFile1 = "AFile1",
                AFile2 = "AFile2",
                AFile3 = "AFile3",
                AFile1Name = "AFile1Name",
                AFile2Name = "AFile2Name",
                AFile3Name = "AFile3Name",
                AUrl1 = "AUrl1",
                AUrl2 = "AUrl2",
                AUrl3 = "AUrl3",
                AUrl1Link = "AUrl1Link",
                AUrl2Link = "AUrl2Link",
                AUrl3Link = "AUrl3Link",
                IsHome = "1",
                IsNew = "1",                
                Vister = 1,
                Emailer = 2,
                Printer = 3,
                SortId = 250,
                Status = "1",                              
                MainCode ="200",
                MainName="MainName",
                AdminCode = "100",
                AdminName = "AdminName",
                ServiceCode = "300",
                ServiceName = "ServiceName",
                Tag = "Tag",
                IsOut = "1",
                AUrl = "AUrl"                
            };
            m_FTISService.CreateNews(news);
            Assert.AreNotEqual(0, news.NewsId);

            //更新
            string updateName = "更新News測試535353533ddee";
            News updateNews = m_FTISService.GetNewsByIdNoLazy(news.NewsId);
            Assert.AreEqual(name, updateNews.Name);
            Assert.AreEqual(newsClassName, updateNews.NewsClass.Name);
            updateNews.Name = updateName;
            m_FTISService.UpdateNews(updateNews);

            //查詢
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("KeyWord", updateName);
            conditions.Add("Status", "1");
            conditions.Add("IsHome", "1");
            IList<News> newsList = m_FTISService.GetNewsList(conditions);
            Assert.AreEqual(1, newsList.Count);
            Assert.AreEqual(1, m_FTISService.GetNewsCount(conditions));
            Assert.AreEqual(updateName, newsList[0].Name);

            conditions = new Dictionary<string, string>();
            conditions.Add("KeyWord", name);
            conditions.Add("Status", "1");
            conditions.Add("IsHome", "1");
            newsList = m_FTISService.GetNewsList(conditions);
            Assert.AreEqual(0, newsList.Count);
            Assert.AreEqual(0, m_FTISService.GetNewsCount(conditions));

            conditions = new Dictionary<string, string>();
            conditions.Add("KeyWord", updateName);
            conditions.Add("Status", "0");
            conditions.Add("IsHome", "1");
            newsList = m_FTISService.GetNewsList(conditions);
            Assert.AreEqual(0, newsList.Count);
            Assert.AreEqual(0, m_FTISService.GetNewsCount(conditions));

            //刪除
            m_FTISService.DeleteNews(updateNews);
            m_FTISService.DeleteNewsClass(newsClass);
            conditions = new Dictionary<string, string>();
            conditions.Add("KeyWord", updateName);
            conditions.Add("Status", "1");
            conditions.Add("IsHome", "1");
            newsList = m_FTISService.GetNewsList(conditions);
            Assert.AreEqual(0, newsList.Count);
            Assert.AreEqual(0, m_FTISService.GetNewsCount(conditions));

            ////查詢Tags
            string searchTags = "歐盟EuP,歐盟CLP";
            conditions.Clear();
            conditions.Add("Tags", searchTags);
            newsList = m_FTISService.GetNewsList(conditions);
            if (newsList != null && newsList.Count > 0)
            {
                foreach (News n in newsList)
                {
                    string tags = n.Tag;
                }
            }

            //測試舊資料英文內容
            News newsENG = m_FTISService.GetNewsById(7);
            Assert.IsNull(newsENG.NameENG);
        }

        [Test]
        public void Test_AdminBar()
        {
            IList<AdminBar> allAdminBar = m_FTISService.GetAllAdminBar();
            Assert.AreEqual(17, allAdminBar.Count);            
        }

        [Test]
        public void Test_MasterMember()
        {
            //新增
            MasterMember masterMember = m_FTISService.MakeMasterMember();
            masterMember.Account = "dadaTest";
            masterMember.Password = "1234";
            masterMember.RegDate = DateTime.Now;
            masterMember.Memo = "memo";
            masterMember.Status = "1";
            masterMember.Tel = "12345678";
            masterMember.Email = "test@yahoo.com.tw";
            masterMember.Name = "dada12345";
            foreach (AdminRole role in masterMember.AdminRoles)
            {
                int adminValue = 15; ////預設全部權限

                ////特別幾個功能給不一樣的權限
                if (role.AdminBar.AdminBarId.Equals((int)SiteEntities.AboutUs))
                {
                    adminValue = (int)SiteOperations.Read;
                }
                if (role.AdminBar.AdminBarId.Equals((int)SiteEntities.Activity))
                {
                    adminValue = (int)SiteOperations.Create;
                }
                if (role.AdminBar.AdminBarId.Equals((int)SiteEntities.Application))
                {
                    adminValue = (int)SiteOperations.Edit;
                }
                if (role.AdminBar.AdminBarId.Equals((int)SiteEntities.Download))
                {
                    adminValue = (int)SiteOperations.Delete;
                }

                role.AdminValue = adminValue;
            }
            m_FTISService.CreateMasterMember(masterMember);

            //查詢
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            IList<MasterMember> adminList = m_FTISService.GetMasterMemberListNoLazy(conditions);
            Assert.AreEqual(9, adminList.Count);
            foreach (MasterMember admin in adminList)
            {
                foreach (AdminRole role in admin.AdminRoles)
                {
                    int value = role.AdminValue;
                }
            }            

            //檢查權限
            try
            {
                Assert.IsTrue(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.AboutUs, (int)SiteOperations.Read));
                Assert.IsFalse(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.AboutUs, (int)SiteOperations.Create));
                Assert.IsFalse(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.AboutUs, (int)SiteOperations.Edit));
                Assert.IsFalse(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.AboutUs, (int)SiteOperations.Delete));

                Assert.IsFalse(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Activity, (int)SiteOperations.Read));
                Assert.IsTrue(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Activity, (int)SiteOperations.Create));
                Assert.IsFalse(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Activity, (int)SiteOperations.Edit));
                Assert.IsFalse(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Activity, (int)SiteOperations.Delete));

                Assert.IsFalse(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Application, (int)SiteOperations.Read));
                Assert.IsFalse(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Application, (int)SiteOperations.Create));
                Assert.IsTrue(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Application, (int)SiteOperations.Edit));
                Assert.IsFalse(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Application, (int)SiteOperations.Delete));

                Assert.IsFalse(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Download, (int)SiteOperations.Read));
                Assert.IsFalse(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Download, (int)SiteOperations.Create));
                Assert.IsFalse(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Download, (int)SiteOperations.Edit));
                Assert.IsTrue(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Download, (int)SiteOperations.Delete));

                Assert.IsTrue(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Grade, (int)SiteOperations.Read));
                Assert.IsTrue(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.News, (int)SiteOperations.Create));
                Assert.IsTrue(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Question, (int)SiteOperations.Edit));
                Assert.IsTrue(ACUtility.CheckAuthorization(masterMember, (int)SiteEntities.Season, (int)SiteOperations.Delete));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            //刪除
            m_FTISService.DeleteMasterMember(masterMember);            
        }

        [Test]
        public void Test_MasterLog()
        {
            //登入紀錄
            MasterLog masterLog = new MasterLog("test1234", "127.0.0.1");
            m_FTISService.CreateMasterLog(masterLog);

            //查詢登入紀錄
            IDictionary<string, string> conditionsLog = new Dictionary<string, string>();
            conditionsLog.Add("EnterTimeFrom", DateTime.Today.ToShortDateString());
            conditionsLog.Add("EnterTimeTo", DateTime.Today.ToShortDateString());
            IList<MasterLog> logList = m_FTISService.GetMasterLogList(conditionsLog);
            Assert.AreEqual(1, logList.Count);

            conditionsLog.Clear();
            conditionsLog.Add("EnterTimeFrom", DateTime.Today.AddDays(-1).ToShortDateString());
            conditionsLog.Add("EnterTimeTo", DateTime.Today.AddDays(-1).ToShortDateString());
            logList = m_FTISService.GetMasterLogList(conditionsLog);
            Assert.AreEqual(0, logList.Count);

            m_FTISService.DeleteMasterLog(masterLog);
        }

        [Test]
        public void Test_DownloadRecord()
        {
            IDictionary<string, string> conditionsLog = new Dictionary<string, string>();
            IList<DownloadRecord> recordList = m_FTISService.GetDownloadRecordStatistics(conditionsLog);
            foreach(DownloadRecord record in recordList)
            {
                string name = record.Name;
                string classId = record.ClassId;
                int downer = record.Downer;
            }
        }

        [Test]
        public void Test_MasterMemberPassword()
        {
            //查詢
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("Account", "admin");
            IList<MasterMember> adminList = m_FTISService.GetMasterMemberListNoLazy(conditions);
            MasterMember admin = adminList[0];
            string password = admin.Password;
            string md5 = EncryptUtil.GetMD5("01801726");
            Assert.IsTrue(password.Equals(md5,StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void Test_GetEpaperYearList()
        {
            //查詢
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("Status", "1");
            IList<string> yearList = m_FTISService.GetEpaperYearList(conditions);
            foreach (string year in yearList)
            {
                Console.WriteLine("year = " + year);
            }
        }

        [Test]
        public void Test_Count()
        {
            //CountVO count = m_FTISService.GetTodayCount();

            //Assert.IsNull(count);

            //count = new CountVO() { BarID = 1, CountDate = DateTime.Today, Hits = 1 };
            //m_FTISService.CreateCount(count);

            //CountVO count2 = m_FTISService.GetTodayCount();
            //Assert.IsNotNull(count2);

            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("BarId", "1");
            int total = m_FTISService.GetSumCountHits(conditions);
            Assert.AreNotEqual(total, 0);
        }

        [Test]
        public void Test_GetNormClassCount()
        {
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("ParentNormClassId", "1");
            conditions.Add("OnlySub", "OnlySub");
            int subsCount = m_FTISService.GetNormClassCount(conditions);
            Assert.AreNotEqual(subsCount, 0);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Globalization;
using WuDada.Core.Generic.Mail;
using WuDada.Core.Generic.Util;
using FTIS;
using FTIS.Service;
using FTIS.Domain;

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
            string name = "新增NewsClass測試";
            NewsClass newsClass = CreateNewsClass(name);

            //更新
            string updateName = "更新NewsClass測試";
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
            Assert.AreEqual(0, newsClassList.Count);
            Assert.AreEqual(0, m_FTISService.GetNewsClassCount(conditions));

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
            string newsClassName = "新增NewsClass測試";
            NewsClass newsClass = CreateNewsClass(newsClassName);

            string name = "新增News測試";
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
            string updateName = "更新News測試";
            News updateNews = m_FTISService.GetNewsById(news.NewsId);
            Assert.AreEqual(name, updateNews.Name);
            updateNews.Name = updateName;
            m_FTISService.UpdateNews(updateNews);

            //查詢
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("Name", updateName);
            conditions.Add("Status", "1");
            conditions.Add("IsHome", "1");
            IList<News> newsList = m_FTISService.GetNewsList(conditions);
            Assert.AreEqual(1, newsList.Count);
            Assert.AreEqual(1, m_FTISService.GetNewsCount(conditions));
            Assert.AreEqual(updateName, newsList[0].Name);

            conditions = new Dictionary<string, string>();
            conditions.Add("Name", name);
            conditions.Add("Status", "1");
            conditions.Add("IsHome", "1");
            newsList = m_FTISService.GetNewsList(conditions);
            Assert.AreEqual(0, newsList.Count);
            Assert.AreEqual(0, m_FTISService.GetNewsCount(conditions));

            conditions = new Dictionary<string, string>();
            conditions.Add("Name", updateName);
            conditions.Add("Status", "0");
            conditions.Add("IsHome", "1");
            newsList = m_FTISService.GetNewsList(conditions);
            Assert.AreEqual(0, newsList.Count);
            Assert.AreEqual(0, m_FTISService.GetNewsCount(conditions));

            //刪除
            m_FTISService.DeleteNews(updateNews);
            m_FTISService.DeleteNewsClass(newsClass);
            conditions = new Dictionary<string, string>();
            conditions.Add("Name", updateName);
            conditions.Add("Status", "1");
            conditions.Add("IsHome", "1");
            newsList = m_FTISService.GetNewsList(conditions);
            Assert.AreEqual(0, newsList.Count);
            Assert.AreEqual(0, m_FTISService.GetNewsCount(conditions));
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
                role.AdminValue = 1;
            }
            m_FTISService.CreateMasterMember(masterMember);

            //查詢
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            IList<MasterMember> adminList = m_FTISService.GetMasterMemberListNoLazy(conditions);
            Assert.AreEqual(10, adminList.Count);
            foreach (MasterMember admin in adminList)
            {
                foreach (AdminRole role in admin.AdminRoles)
                {
                    int value = role.AdminValue;
                }
            }

            //刪除
            m_FTISService.DeleteMasterMember(masterMember);
        }
    }
}

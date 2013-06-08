using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Globalization;
using WuDada.Core.Generic.Mail;
using WuDada.Core.Generic.Util;
using FTIS.Service;
using FTIS;
using FTIS.Domain.Impl;

namespace FOTIS.Test.Service
{
    [TestFixture]
    public class InitWebDataService
    {
        private FTISFactory m_FTISFactory { get; set; }
        private ITemplateService m_TemplateService { get; set; }

        [TestFixtureSetUp]
        public void TestCaseInit()
        {
            m_FTISFactory = new FTISFactory();
            m_TemplateService = m_FTISFactory.GetTemplateService();
        }

        [Test]
        public void InitWebData()
        {
            InitTemplate();
        }        

        [Test]
        public void TestDay()
        {
            TaiwanLunisolarCalendar tlc = new TaiwanLunisolarCalendar();

            int year = tlc.GetYear(DateTime.Now);
            int month = tlc.GetMonth(DateTime.Now);
            int day = tlc.GetDayOfMonth(DateTime.Now);

            string aa = string.Format("{0}/{1}/{2}", year, month, day);
            Console.WriteLine("day = " + aa);

            DateTime date2 = tlc.ToDateTime(year, month, day, 0, 0, 0, 0);
            Console.WriteLine("day2 = " + date2.ToShortDateString());
        }

        [Test]
        public void TestCurrentTemplate()
        {
            TemplateVO templateVO = m_TemplateService.GetCurrentTemplate(7);
            Console.WriteLine("TestCurrentTemplate = " + templateVO.Name);
        }

        [Test]
        public void TestFilterHtml()
        {
            string testStr = "<style type=\"text/css\">.txt01 {</style> ddd <script>abc {</script>";
            string newStr = ConvertUtil.FilterHtml(testStr);
            Console.WriteLine("newStr = " + newStr);
        }        

        private void InitTemplate()
        {
            TemplateVO templateVO1 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Season,
                Name = "春",
                //CSS = "<link href='season/season01.css' rel='stylesheet' type='text/css' />",
                //FileName = "season01.txt",
                FileName2 = "/Scripts/ckfinder/userfiles/flash/flash.swf",
                StartDate = "0301",
                EndDate = "0531"
            };
            m_TemplateService.CreateTemplate(templateVO1);

            TemplateVO templateVO2 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Season,
                Name = "夏",
                //CSS = "<link href='season/season02.css' rel='stylesheet' type='text/css' />",
                //FileName = "season02.txt",
                FileName2 = "/Scripts/ckfinder/userfiles/flash/flash.swf",
                StartDate = "0601",
                EndDate = "0831"
            };
            m_TemplateService.CreateTemplate(templateVO2);

            TemplateVO templateVO3 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Season,
                Name = "秋",
                //CSS = "<link href='season/season03.css' rel='stylesheet' type='text/css' />",
                //FileName = "season03.txt",
                FileName2 = "/Scripts/ckfinder/userfiles/flash/flash.swf",
                StartDate = "0901",
                EndDate = "1131"
            };
            m_TemplateService.CreateTemplate(templateVO3);

            TemplateVO templateVO4 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Season,
                Name = "冬",
                //CSS = "",
                //FileName = "season04.txt",
                FileName2 = "/Scripts/ckfinder/userfiles/flash/flash.swf",
                StartDate = "1201",
                EndDate = "0229"
            };
            m_TemplateService.CreateTemplate(templateVO4);

            TemplateVO templateVO5 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "中秋節",
                //CSS = "<link href='festival01.css' rel='stylesheet' type='text/css' />",
                //FileName = "festival01.txt",
                FileName2 = "/Scripts/ckfinder/userfiles/flash/flash.swf",
                StartDate = "0815",
                EndDate = "0815"
            };
            m_TemplateService.CreateTemplate(templateVO5);

            TemplateVO templateVO6 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "元宵節",
                //CSS = "",
                //FileName = "festival02.txt",
                FileName2 = "/Scripts/ckfinder/userfiles/flash/flash.swf",
                StartDate = "0115",
                EndDate = "0115"
            };
            m_TemplateService.CreateTemplate(templateVO6);

            TemplateVO templateVO7 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "端午節",
                //CSS = "",
                //FileName = "festival03.txt",
                FileName2 = "/Scripts/ckfinder/userfiles/flash/flash.swf",
                StartDate = "0505",
                EndDate = "0505"
            };
            m_TemplateService.CreateTemplate(templateVO7);

            TemplateVO templateVO8 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "中國情人節",
                //CSS = "",
                //FileName = "festival04.txt",
                FileName2 = "/Scripts/ckfinder/userfiles/flash/flash.swf",
                StartDate = "0707",
                EndDate = "0707"
            };
            m_TemplateService.CreateTemplate(templateVO8);

            TemplateVO templateVO9 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "聖誕節",
                //CSS = "",
                //FileName = "festival05.txt",
                FileName2 = "/Scripts/ckfinder/userfiles/flash/flash.swf",
                StartDate = "1225",
                EndDate = "1225"
            };
            m_TemplateService.CreateTemplate(templateVO9);

            TemplateVO templateVO10 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "春節",
                //CSS = "",
                //FileName = "festival06.txt",
                FileName2 = "/Scripts/ckfinder/userfiles/flash/flash.swf",
                StartDate = "0101",
                EndDate = "0101"
            };
            m_TemplateService.CreateTemplate(templateVO10);

            TemplateVO templateVO11 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "西洋情人節",
                //CSS = "",
                //FileName = "festival04.txt",
                FileName2 = "/Scripts/ckfinder/userfiles/flash/flash.swf",
                StartDate = "0214",
                EndDate = "0214"
            };
            m_TemplateService.CreateTemplate(templateVO11);
        }        
    }
}

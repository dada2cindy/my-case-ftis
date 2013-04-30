using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Service;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Auth.Container;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.SystemApplications.Domain;
using WuDada.Core.SystemApplications;
using WuDada.Core.SystemApplications.Service;
using System.Globalization;
using WuDada.Core.Generic.Mail;
using WuDada.Core.Generic.Util;

namespace FOTIS.Test.Service
{
    [TestFixture]
    public class InitWebDataService
    {
        private AuthFactory m_AuthFactory { get; set; }
        private PostFactory m_PostFactory { get; set; }
        private SystemFactory m_SystemFactory { get; set; }
        private IAuthService m_AuthService { get; set; }
        private IPostService m_PostService { get; set; }
        private ITemplateService m_TemplateService { get; set; }
        private ISystemService m_SystemService { get; set; }
        private IMessageService m_MessageService { get; set; }

        [TestFixtureSetUp]
        public void TestCaseInit()
        {
            m_AuthFactory = new AuthFactory();
            m_PostFactory = new PostFactory();
            m_SystemFactory = new SystemFactory();
            m_AuthService = m_AuthFactory.GetAuthService();
            m_PostService = m_PostFactory.GetPostService();
            m_TemplateService = m_SystemFactory.GetTemplateService();
            m_SystemService = m_SystemFactory.GetSystemService();
            m_MessageService = m_PostFactory.GetMessageService();
        }

        [Test]
        public void InitWebData()
        {
            InitMenu();
            InitLoginRoleAndUser();
            InitNode();
            InitTemplate();
            InitSystemParam();
        }

        [Test]
        public void Test_UserMenuFuncContainer()
        {
            //清除快取
            UserMenuFuncContainer.GetInstance().ReloadAllMenu();

            UserMenuFuncContainer.GetInstance().GetUser("admin");
            UserMenuFuncContainer.GetInstance().GetUser("admin");
        }

        [Test]
        public void Test_InitContactorMail()
        {
            string classify = "聯絡我們收件者";
            ItemParamVO contactor = new ItemParamVO(classify, "吳信達", "dada2cindy@hotmail.com", false);
            m_SystemService.CreateItemParam(contactor);

            ItemParamVO contactor2 = new ItemParamVO(classify, "王小小", "dada@pro2e.com.tw", false);
            m_SystemService.CreateItemParam(contactor2);

            IList<ItemParamVO> contactorList = m_SystemService.GetAllItemParamByNoDel(classify);
            Console.WriteLine("contactorList count =" + contactorList.Count);
        }

        [Test]
        public void Test_SendMessageMail()
        {
            //建立一篇訊息
            MessageVO messageVO = new MessageVO();
            messageVO.Content = "意見";
            messageVO.CreateName = "張大保";
            messageVO.EMail = "dada2338@yahoo.com.tw";
            messageVO.Fax = "23223333";
            messageVO.Phone = "22234563";
            messageVO.Mobile = "0912333444";
            messageVO.CreatedDate = DateTime.Now;
            messageVO.CreateIP = "127.0.0.1";

            messageVO = m_MessageService.CreateMessage(messageVO);

            string classify = "聯絡我們收件者";
            IList<ItemParamVO> contactorList = m_SystemService.GetAllItemParamByNoDel(classify);

            if (contactorList != null && contactorList.Count > 0)
            {
                SystemParamVO mailVO = m_SystemService.GetSystemParamByRoot();
                MailService mailService = new MailService(mailVO.MailSmtp, int.Parse(mailVO.MailPort), mailVO.EnableSSL, mailVO.Account, mailVO.Password);

                StringBuilder sbMailList = new StringBuilder();
                foreach (ItemParamVO contactor in contactorList)
                {
                    sbMailList.Append(string.Format("{0};", contactor.Value));
                }

                string mailTitle = string.Format("收到一封由【{0}】從產基會網站提出的意見信。", messageVO.CreateName);
                string mailContent = GenMailContent(messageVO);

                mailService.SendMail(mailVO.SendEmail, sbMailList.ToString(), mailTitle, mailContent);
            }
        }

        private string GenMailContent(MessageVO messageVO)
        {
            StringBuilder sbContent = new StringBuilder();

            sbContent.Append(string.Format("時　　間：{0}<br />", messageVO.CreatedDate.Value.ToString()));
            sbContent.Append(string.Format("姓　　名：{0}<br />", messageVO.CreateName));
            sbContent.Append(string.Format("電　　話：{0}<br />", messageVO.Phone));
            sbContent.Append(string.Format("手　　機：{0}<br />", messageVO.Mobile));
            sbContent.Append(string.Format("傳　　真：{0}<br />", messageVO.Fax));
            sbContent.Append(string.Format("電子信箱：{0}<br />", messageVO.EMail));
            sbContent.Append(string.Format("意　　見：{0}<br />", messageVO.Content.Replace("\n", "<br />")));

            return sbContent.ToString();
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
        public void TestGetPostListByNodeId()
        {
            int nodeId = 22;

            //搜尋條件
            DateTime? startDate = DateTime.Today;
            string sortField = "ShowDate";
            bool sortDesc = true;
            int pageSize = 2;

            IList<PostVO> postList = m_PostService.GetPostListByNodeId(nodeId, true, startDate, 0, pageSize, sortField, sortDesc);

            int count = postList.Count;
            Console.WriteLine("postList count = " + count);
        }

        [Test]
        public void TestFilterHtml()
        {
            string testStr = "<style type=\"text/css\">.txt01 {</style> ddd <script>abc {</script>";
            string newStr = ConvertUtil.FilterHtml(testStr);
            Console.WriteLine("newStr = " + newStr);
        }

        private void InitMenu()
        {
            MenuFuncVO parentMenu7 = CreateParentMenu("網站分類管理", 1000);
            CreateSubMenu("活動與服務分類編輯", parentMenu7, 1001, "admin/UC01/0101ActNodeMamager.aspx");

            MenuFuncVO parentMenu1 = CreateParentMenu("網站內容管理", 2000);
            CreateSubMenu("關於本會", parentMenu1, 2001, "admin/UC02/0201AboutUs.aspx");
            CreateSubMenu("最新消息", parentMenu1, 2002, "admin/UC02/0202NewsPost.aspx");
            CreateSubMenu("專案報導", parentMenu1, 2003, "admin/UC02/0210Project.aspx");
            CreateSubMenu("專業服務", parentMenu1, 2004, "admin/UC02/0203Service.aspx");
            CreateSubMenu("講習訓練", parentMenu1, 2005, "admin/UC02/0204Training.aspx");
            CreateSubMenu("活動與服務", parentMenu1, 2006, "admin/UC02/0205Act.aspx");
            CreateSubMenu("人力資源", parentMenu1, 2007, "admin/UC02/0206Resources.aspx");
            CreateSubMenu("聯絡我們", parentMenu1, 2008, "admin/UC02/0207ContactUs.aspx");
            CreateSubMenu("版權宣言", parentMenu1, 2009, "admin/UC02/0208Copyright.aspx");
            CreateSubMenu("隱私權保護政策", parentMenu1, 2010, "admin/UC02/0209Privacy.aspx");
            CreateSubMenu("交通資訊", parentMenu1, 2011, "admin/UC02/0211Map.aspx");

            MenuFuncVO parentMenu4 = CreateParentMenu("網站樣式管理", 3000);
            CreateSubMenu("網站主視覺", parentMenu4, 3002, "admin/UC03/0301Template.aspx");
            CreateSubMenu("四季樣板", parentMenu4, 3002, "admin/UC03/0302Template.aspx");
            CreateSubMenu("節慶樣板", parentMenu4, 3003, "admin/UC03/0303Template.aspx");

            MenuFuncVO parentMenu5 = CreateParentMenu("網站廣告管理", 4000);
            CreateSubMenu("首頁中間廣告", parentMenu5, 4001, "admin/UC04/0401Adv.aspx");
            CreateSubMenu("首頁下方廣告", parentMenu5, 4002, "admin/UC04/0402Adv.aspx");

            MenuFuncVO parentMenu6 = CreateParentMenu("聯絡我們管理", 5000);
            CreateSubMenu("聯絡我們紀錄", parentMenu6, 5001, "admin/UC05/0501ContactUS.aspx");
            CreateSubMenu("信箱設定", parentMenu6, 5002, "admin/UC05/0502Email.aspx");

            MenuFuncVO parentMenu2 = CreateParentMenu("個人設定", 9998);
            CreateSubMenu("登入密碼變更", parentMenu2, 1, "admin/UC30/3001Personal.aspx");

            MenuFuncVO parentMenu3 = CreateParentMenu("權限管理", 9999);
            CreateSubMenu("帳號管理", parentMenu3, 1, "admin/UC14/UserAdd.aspx");
            CreateSubMenu("群組管理", parentMenu3, 2, "admin/UC14/RoleAdd.aspx");
            CreateSubMenu("帳號群組設定", parentMenu3, 3, "admin/UC14/UserRoleSet.aspx");
            CreateSubMenu("群組權限設定", parentMenu3, 4, "admin/UC14/RoleFuncSet.aspx");
            CreateSubMenu("使用紀錄", parentMenu3, 5, "admin/UC14/QueryLog.aspx");
        }

        private void InitLoginRoleAndUser()
        {
            //建立後台角色
            LoginRoleVO loginRoleVO = new LoginRoleVO("系統管理員");
            loginRoleVO.MenuFuncList = m_AuthService.GetNotTopMenuFunc(); //角色功能權限
            m_AuthService.CreateLoginRole(loginRoleVO);

            LoginUserVO loginUserVO = new LoginUserVO();
            loginUserVO.UserId = "admin";
            loginUserVO.Password = "1234";
            loginUserVO.FullNameInChinese = "系統管理者";
            loginUserVO.FullNameInEnglish = "Administrator";
            loginUserVO.LoginRoleList = new List<LoginRoleVO>();
            loginUserVO.LoginRoleList.Add(loginRoleVO);
            loginUserVO.CreateDate = DateTime.Now;
            m_AuthService.CreateLoginUser(loginUserVO);
        }

        private void InitNode()
        {
            NodeVO rootNodeVO = CreateNode("Root", null, 0);
            NodeVO nodeVO1 = CreateNode("關於本會", rootNodeVO, 1);
            CreatePost("本會簡介", nodeVO1, 1);
            CreatePost("組織架構", nodeVO1, 2);
            CreatePost("本會延革", nodeVO1, 3);
            CreatePost("本會會員", nodeVO1, 4);
            //CreatePost("本會簡介", nodeVO1, 5);
            CreateNode("最新消息", rootNodeVO, 2);
            NodeVO nodeVO3 = CreateNode("專業服務", rootNodeVO, 4);
            CreatePost("研發專案組", nodeVO3, 1);
            CreatePost("工業減廢組", nodeVO3, 2);
            CreatePost("工業安全", nodeVO3, 3);
            CreatePost("講習訓練組", nodeVO3, 4);
            CreatePost("公共關係組", nodeVO3, 5);
            CreatePost("環境與資源服務中心", nodeVO3, 6);
            CreatePost("綠色技術發展中心", nodeVO3, 7);
            NodeVO nodeVO4 = CreateNode("講習訓練", rootNodeVO, 5);
            CreatePost("講習訓練", nodeVO4, 1);
            NodeVO nodeVO5 = CreateNode("活動與服務", rootNodeVO, 6);
            CreateNode("研發專案組", nodeVO5, 1);
            CreateNode("工業減廢組", nodeVO5, 2);
            CreateNode("工業安全", nodeVO5, 3);
            CreateNode("講習訓練組", nodeVO5, 4);
            CreateNode("公共關係組", nodeVO5, 5);
            CreateNode("環境與資源服務中心", nodeVO5, 6);
            CreateNode("綠色技術發展中心", nodeVO5, 7);
            NodeVO nodeVO6 = CreateNode("人力資源", rootNodeVO, 7);
            CreatePost("招募項目", nodeVO6, 1);
            CreatePost("福利制度", nodeVO6, 2);
            CreatePost("本會延革", nodeVO6, 3);
            CreatePost("本會會員", nodeVO6, 4);
            NodeVO nodeVO7 = CreateNode("聯絡我們", rootNodeVO, 8);
            CreatePost("聯絡我們", nodeVO7, 1);
            NodeVO nodeVO8 = CreateNode("版權宣言", rootNodeVO, 9);
            CreatePost("版權宣言", nodeVO8, 1);
            NodeVO nodeVO9 = CreateNode("隱私權保護政策", rootNodeVO, 10);
            CreatePost("隱私權保護政策", nodeVO9, 1);            

            NodeVO nodeVO10 = CreateNode("四季樣板", rootNodeVO, 11);
            //補四季樣板四個Template資料
            NodeVO nodeVO11 = CreateNode("節慶樣板", rootNodeVO, 12);
            //補節慶樣板六個Template資料

            NodeVO nodeVO12 = CreateNode("首頁中間廣告", rootNodeVO, 13);
            CreatePost("好書推薦", nodeVO12, 1, string.Empty, "adv01.png");

            NodeVO nodeVO13 = CreateNode("首頁下方廣告", rootNodeVO, 14);
            CreatePost("稽核資訊網站", nodeVO13, 1, "http://db.ftis.org.tw/env_center/", "adv02.gif");
            CreatePost("產業永續發展整合資訊網", nodeVO13, 2, "http://proj.ftis.org.tw/isdn/", "adv03.gif");
            CreatePost("愛水人之屋", nodeVO13, 3, "http://www.ftis.org.tw/water/", "adv04.gif");
            CreatePost("採購專業人員訓練", nodeVO13, 4, "http://train.ftis.org.tw/Ftis/", "adv05.gif");
            CreatePost("職業安全衛生法規檢所網站", nodeVO13, 5, "http://db.ftis.org.tw/shlaw/", "adv06.gif");
            CreatePost("產業製程清潔生產與綠色技術", nodeVO13, 6, "http://proj.moeaidb.gov.tw/eta/", "adv07.gif");
            CreatePost("製造業節能減碳服務團", nodeVO13, 7, "http://www.ftis.org.tw/tigers/", "adv08.gif");
            CreatePost("再生能源發電設備認定及查核", nodeVO13, 8, "http://60.251.181.242/about01.html", "adv09.gif");
            CreatePost("綠色議題訓練課程", nodeVO13, 9, "http://www.ftis.org.tw/active/rd2012class.htm", "adv10.gif");
            CreatePost("高雄市生物固炭技術整合研究計畫", nodeVO13, 10, "http://www.ema.org.tw/microalgae/", "adv11.gif");

            CreateNode("專案報導", rootNodeVO, 3);

            NodeVO nodeVO15 = CreateNode("主視覺管理", rootNodeVO, 15);
            CreateNode("首頁主視覺", nodeVO15, 1, NodeVO.UnitType.Flash, "c-swf2b.swf");
            CreateNode("關於本會主視覺", nodeVO15, 2, NodeVO.UnitType.Pic, "inadv01.jpg");
            CreateNode("最新消息主視覺", nodeVO15, 3, NodeVO.UnitType.Pic, "inadv03.jpg");
            CreateNode("專業服務主視覺", nodeVO15, 4, NodeVO.UnitType.Pic, "inadv04.jpg");
            CreateNode("講習訓練主視覺", nodeVO15, 5, NodeVO.UnitType.Pic, "inadv03.jpg");
            CreateNode("活動與服務主視覺", nodeVO15, 6, NodeVO.UnitType.Pic, "inadv05.jpg");
            CreateNode("人力資源主視覺", nodeVO15, 7, NodeVO.UnitType.Pic, "inadv05.jpg");
            CreateNode("專案報導主視覺", nodeVO15, 8, NodeVO.UnitType.Pic, "inadv03.jpg");

            NodeVO nodeVO16 = CreateNode("交通資訊", rootNodeVO, 16);
            CreatePost("交通資訊", nodeVO16, 1);
        }

        private void InitTemplate()
        {
            TemplateVO templateVO1 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Season,
                Name = "春",
                CSS = "<link href='season/season01.css' rel='stylesheet' type='text/css' />",
                FileName = "season01.txt",
                FileName2 = "c-swf2a.swf",
                StartDate = "0301",
                EndDate = "0531"
            };
            m_TemplateService.CreateTemplate(templateVO1);

            TemplateVO templateVO2 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Season,
                Name = "夏",
                CSS = "<link href='season/season02.css' rel='stylesheet' type='text/css' />",
                FileName = "season02.txt",
                FileName2 = "c-swf2b.swf",
                StartDate = "0601",
                EndDate = "0831"
            };
            m_TemplateService.CreateTemplate(templateVO2);

            TemplateVO templateVO3 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Season,
                Name = "秋",
                CSS = "<link href='season/season03.css' rel='stylesheet' type='text/css' />",
                FileName = "season03.txt",
                FileName2 = "c-swf2c.swf",
                StartDate = "0901",
                EndDate = "1131"
            };
            m_TemplateService.CreateTemplate(templateVO3);

            TemplateVO templateVO4 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Season,
                Name = "冬",
                CSS = "",
                FileName = "season04.txt",
                FileName2 = "c-swf2d.swf",
                StartDate = "1201",
                EndDate = "0229"
            };
            m_TemplateService.CreateTemplate(templateVO4);

            TemplateVO templateVO5 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "中秋節",
                CSS = "<link href='festival01.css' rel='stylesheet' type='text/css' />",
                FileName = "festival01.txt",
                FileName2 = "c-swf2-01.swf",
                StartDate = "0815",
                EndDate = "0815"
            };
            m_TemplateService.CreateTemplate(templateVO5);

            TemplateVO templateVO6 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "元宵節",
                CSS = "",
                FileName = "festival02.txt",
                FileName2 = "c-swf2-06.swf",
                StartDate = "0115",
                EndDate = "0115"
            };
            m_TemplateService.CreateTemplate(templateVO6);

            TemplateVO templateVO7 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "端午節",
                CSS = "",
                FileName = "festival03.txt",
                FileName2 = "c-swf2-03.swf",
                StartDate = "0505",
                EndDate = "0505"
            };
            m_TemplateService.CreateTemplate(templateVO7);

            TemplateVO templateVO8 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "中國情人節",
                CSS = "",
                FileName = "festival04.txt",
                FileName2 = "festival05.txt",
                StartDate = "0707",
                EndDate = "0707"
            };
            m_TemplateService.CreateTemplate(templateVO8);

            TemplateVO templateVO9 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "聖誕節",
                CSS = "",
                FileName = "festival05.txt",
                FileName2 = "festival02.txt",
                StartDate = "1225",
                EndDate = "1225"
            };
            m_TemplateService.CreateTemplate(templateVO9);

            TemplateVO templateVO10 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "春節",
                CSS = "",
                FileName = "festival06.txt",
                FileName2 = "festival04.txt",
                StartDate = "0101",
                EndDate = "0101"
            };
            m_TemplateService.CreateTemplate(templateVO10);

            TemplateVO templateVO11 = new TemplateVO()
            {
                TemplateType = TemplateVO.Type.Festival,
                Name = "西洋情人節",
                CSS = "",
                FileName = "festival04.txt",
                FileName2 = "festival05.txt",
                StartDate = "0214",
                EndDate = "0214"
            };
            m_TemplateService.CreateTemplate(templateVO11);
        }

        private PostVO CreatePost(string title, NodeVO nodeVO, int sort)
        {
            PostVO postVO = new PostVO();
            postVO.Node = nodeVO;
            postVO.Title = title;
            postVO.SortNo = sort;
            postVO.Flag = 1;
            postVO.CreatedBy = "admin";
            postVO.UpdatedBy = "admin";
            postVO.CreatedDate = DateTime.Now;
            postVO.UpdatedDate = DateTime.Now;
            postVO.ShowDate = DateTime.Today;

            return m_PostService.CreatePost(postVO);
        }

        private PostVO CreatePost(string title, NodeVO nodeVO, int sort, string linkUrl, string picFileName)
        {
            PostVO postVO = new PostVO();
            postVO.Node = nodeVO;
            postVO.Title = title;
            postVO.SortNo = sort;
            postVO.Flag = 1;
            postVO.CreatedBy = "admin";
            postVO.UpdatedBy = "admin";
            postVO.CreatedDate = DateTime.Now;
            postVO.UpdatedDate = DateTime.Now;
            postVO.ShowDate = DateTime.Today;
            postVO.LinkUrl = linkUrl;
            postVO.PicFileName = picFileName;

            return m_PostService.CreatePost(postVO);
        }

        private NodeVO CreateNode(string name, NodeVO parentNode, int sort)
        {
            NodeVO rootNodeVO = new NodeVO();
            rootNodeVO.Name = name;
            rootNodeVO.ParentNode = parentNode;
            rootNodeVO.SortNo = sort;
            rootNodeVO.Flag = 1;
            rootNodeVO.CreatedBy = "admin";
            rootNodeVO.UpdatedBy = "admin";
            rootNodeVO.CreatedDate = DateTime.Now;
            rootNodeVO.UpdatedDate = DateTime.Now;

            return m_PostService.CreateNode(rootNodeVO);
        }

        private NodeVO CreateNode(string name, NodeVO parentNode, int sort, NodeVO.UnitType unitType, string filePath)
        {
            NodeVO rootNodeVO = new NodeVO();
            rootNodeVO.Name = name;
            rootNodeVO.ParentNode = parentNode;
            rootNodeVO.SortNo = sort;
            rootNodeVO.Flag = 1;
            rootNodeVO.CreatedBy = "admin";
            rootNodeVO.UpdatedBy = "admin";
            rootNodeVO.CreatedDate = DateTime.Now;
            rootNodeVO.UpdatedDate = DateTime.Now;
            rootNodeVO.UType = unitType;
            rootNodeVO.PicFileName = filePath;

            return m_PostService.CreateNode(rootNodeVO);
        }

        private MenuFuncVO CreateParentMenu(string menuName, int sort)
        {
            MenuFuncVO menuFuncVO = new MenuFuncVO(menuName, null);
            menuFuncVO.ListOrder = sort;

            return m_AuthService.CreateMenuFunc(menuFuncVO);
        }

        private MenuFuncVO CreateSubMenu(string menuName, MenuFuncVO parentMenu, int sort, string path)
        {
            MenuFuncVO menuFuncVO = new MenuFuncVO(menuName, parentMenu);
            menuFuncVO.ListOrder = sort;
            menuFuncVO.MainPath = path;

            return m_AuthService.CreateMenuFunc(menuFuncVO);
        }

        private void InitSystemParam()
        {
            //系統設定
            SystemParamVO systemParamVO = new SystemParamVO();
            //systemParamVO.MailSmtp = "smtp.gmail.com";
            //systemParamVO.Account = "test@pro2e.com.tw";
            //systemParamVO.SendEmail = "test@pro2e.com.tw";
            //systemParamVO.MailPort = "587";
            //systemParamVO.EnableSSL = true;
            //systemParamVO.Password = "28005786";
            systemParamVO.MailSmtp = "60.248.19.168";
            systemParamVO.Account = "SmtpUser";
            systemParamVO.SendEmail = "test@test.com";
            systemParamVO.MailPort = "25";
            systemParamVO.EnableSSL = false;
            systemParamVO.Password = "abc+1234";

            m_SystemService.CreateSystemParam(systemParamVO);
        }
    }
}

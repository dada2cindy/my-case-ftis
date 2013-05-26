namespace WuDada.Accessibility.FreeGO
{
    using HtmlAgilityPack;
    using Microsoft.VisualBasic;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Xml;

    public class Accessibility
    {
        private string m_Checklevel;
        private string m_ContentEncoding;
        private string m_ErrorMessage = string.Empty;
        private bool m_isVerified = true;
        private HtmlDocument m_objHtmlDocument = new HtmlDocument();
        private string m_Report_Desc;
        private string m_Result;
        private string m_strMediaCheck = ".cda;.aif;.aifc;.aiff;.asf;.asx;.wax;.wm;.wma;.wmd;.wmp;.wmv;.wmx;.wpl;.wvx;.avi;.wav;.wmz;.mpeg;.mpg;.m1v;.mp2;.mpa;.mpe;.mp2v;.mpv2;.mid;.midi;.rmi;.au;.snd;.mp3;.m3u;.vob;.swf;.rm;.ram;.rmvb;.mov;";
        private string m_strNewContents;
        private string m_strTagFormat = "<a href='#{0}'>{0}</a>、";
        private XmlDocument m_XDoc;

        public Accessibility(string src, ContnetTypeEnum contentType, ContentEncodingEnum ContentEncoding, ckdegreeEnum checklevel)
        {
            string requestUriString = src;
            StreamReader reader = null;
            string s = null;
            string str3 = string.Empty;
            int num = 1;
            this.m_ContentEncoding = System.Enum.Format(typeof(ContentEncodingEnum), ContentEncoding, "G");
            if ((this.m_ContentEncoding == null) | this.m_ContentEncoding.Equals("UTF8"))
            {
                this.m_ContentEncoding = "UTF-8";
            }
            Encoding encoding = Encoding.GetEncoding(this.m_ContentEncoding);
            switch (contentType)
            {
                case ContnetTypeEnum.web:
                    try
                    {
                        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestUriString);
                        request.Timeout = 0xea60;
                        reader = new StreamReader(request.GetResponse().GetResponseStream(), encoding);
                    }
                    catch (WebException exception)
                    {
                        this.m_ErrorMessage = exception.Message;
                    }
                    catch (IOException exception5)
                    {
                        IOException exception2 = exception5;
                        this.m_ErrorMessage = exception2.Message;
                    }
                    catch (Exception exception6)
                    {
                        Exception exception3 = exception6;
                        this.m_ErrorMessage = exception3.Message;
                    }
                    break;

                case ContnetTypeEnum.cont:
                    try
                    {
                        reader = new StreamReader(new MemoryStream(Encoding.GetEncoding(this.m_ContentEncoding).GetBytes(requestUriString)));
                    }
                    catch (IOException exception4)
                    {
                        this.m_ErrorMessage = exception4.ToString();
                    }
                    break;

                case ContnetTypeEnum.file:
                    try
                    {
                        reader = new StreamReader(requestUriString, encoding);
                    }
                    catch (IOException exception8)
                    {
                        this.m_ErrorMessage = exception8.ToString();
                    }
                    catch (Exception exception9)
                    {
                        this.m_ErrorMessage = exception9.ToString();
                    }
                    break;
            }
            this.m_objHtmlDocument.Load(reader.BaseStream, encoding);
            StringReader reader2 = new StringReader(this.m_objHtmlDocument.DocumentNode.OuterHtml);
            while (reader2.Peek() >= 0)
            {
                s = reader2.ReadLine();
                str3 = str3 + string.Format("<a name='#{0}'>第{0}行</a>{1}", num, HttpUtility.HtmlEncode(s) + "<br />");
                num++;
            }
            this.m_strNewContents = str3;
            this.m_Checklevel = System.Enum.Format(typeof(ckdegreeEnum), checklevel, "G");
            this.m_XDoc = this.WriteXML();
            string nodeAttributeValue = string.Empty;
            string str5 = this.m_Checklevel;
            if (str5 != null)
            {
                if (!(str5 == "A"))
                {
                    if (str5 == "APlus")
                    {
                        this.m_Report_Desc = "受測網頁尚未達到A+等級無障礙檢測標準，一個網頁必須通過A+等級的全部檢測要點才能達到這個等級的無障礙網頁標準。";
                        nodeAttributeValue = "第一優先等級";
                        this.CreateXmlNode(this.m_XDoc, "//RESULT/DETAIL", "TITLE_DEGREE", "", "name", nodeAttributeValue);
                        this.CreateXmlNode(this.m_XDoc, string.Format("//RESULT/DETAIL/TITLE_DEGREE[@name='{0}']", nodeAttributeValue), "DESCRIPTION", "受測網頁未達此等級無障礙標準，以下是檢測時發現的錯誤:", "", "");
                        nodeAttributeValue = "A+等級檢測項目";
                        this.CreateXmlNode(this.m_XDoc, "//RESULT/DETAIL", "TITLE_DEGREE", "", "name", nodeAttributeValue);
                        this.CreateXmlNode(this.m_XDoc, string.Format("//RESULT/DETAIL/TITLE_DEGREE[@name='{0}']", nodeAttributeValue), "DESCRIPTION", "受測網頁未達此等級無障礙標準，以下是檢測時發現的錯誤:", "", "");
                        this.CreateXmlNode(this.m_XDoc, "/RESULT", "REPORT_DESC", this.m_Report_Desc, "", "");
                        this.MachineTesting(this.m_objHtmlDocument);
                        this.MachineAndHumanTesting(this.m_objHtmlDocument);
                        this.HumanTesting(this.m_objHtmlDocument);
                        this.AplusMachineTesting(this.m_objHtmlDocument);
                        this.AplusHumanTesting(this.m_objHtmlDocument);
                    }
                    else if (str5 == "AAPlus")
                    {
                        this.m_Report_Desc = "受測網頁尚未達到第二優先等級無障礙檢測標準，一個網頁必須通過第二優先等級的全部檢測要點才能達到這個等級的無障礙網頁標準。";
                    }
                    else if (str5 == "AAAPlus")
                    {
                        this.m_Report_Desc = "受測網頁尚未達到第三優先等級無障礙檢測標準，一個網頁必須通過第三優先等級的全部檢測要點才能達到這個等級的無障礙網頁標準。";
                    }
                }
                else
                {
                    this.m_Report_Desc = "受測網頁尚未達到第一優先等級無障礙檢測標準，一個網頁必須通過第一優先等級的全部檢測要點才能達到這個等級的無障礙網頁標準。";
                    nodeAttributeValue = "第一優先等級";
                    this.CreateXmlNode(this.m_XDoc, "//RESULT/DETAIL", "TITLE_DEGREE", "", "name", nodeAttributeValue);
                    this.CreateXmlNode(this.m_XDoc, string.Format("//RESULT/DETAIL/TITLE_DEGREE[@name='{0}']", nodeAttributeValue), "DESCRIPTION", "受測網頁未達此等級無障礙標準，以下是檢測時發現的錯誤:", "", "");
                    this.CreateXmlNode(this.m_XDoc, "/RESULT", "REPORT_DESC", this.m_Report_Desc, "", "");
                    this.MachineTesting(this.m_objHtmlDocument);
                    this.MachineAndHumanTesting(this.m_objHtmlDocument);
                    this.HumanTesting(this.m_objHtmlDocument);
                }
            }
            this.CreateXmlNode(this.m_XDoc, "/RESULT", "IS_PASS", this.m_isVerified.ToString(), "", "");
            this.m_Result = this.m_XDoc.OuterXml;
        }

        private void AplusHumanTesting(HtmlDocument objHtmlDocument)
        {
            this.CreateXmlNode(this.m_XDoc, "/RESULT/DETAIL/TITLE_DEGREE[@name='A+等級檢測項目']", "MAN", "", "name", "A+/人工檢測項目");
            this.RuleTesting("H206104", "6.5", "若網頁物件使用事件驅動時，確定勿僅使用滑鼠操作", objHtmlDocument.DocumentNode.SelectNodes("//script|//applet"), 0, string.Empty, "A+等級檢測項目", "MAN");
            this.RuleTesting("H209201", "9.2", "對所有網頁內容元素，確保有滑鼠以外的操作介面", null, 3, string.Empty, "A+等級檢測項目", "MAN");
            this.RuleTesting("H309204", "9.5", "對經常使用的超連結，增加快速鍵的操作", null, 3, string.Empty, "A+等級檢測項目", "MAN");
            this.RuleTesting("H213205", "13.6", "為你的網站提供網站地圖或整體性的簡介", null, 3, string.Empty, "A+等級檢測項目", "MAN");
        }

        private void AplusMachineTesting(HtmlDocument objHtmlDocument)
        {
            this.CreateXmlNode(this.m_XDoc, "/RESULT/DETAIL/TITLE_DEGREE[@name='A+等級檢測項目']", "MACHINE", "", "name", "A+/機器檢測項目");
            HtmlNodeCollection nodes = null;
            nodes = objHtmlDocument.DocumentNode.SelectNodes("//*[@onclick|@oncontextmenu|@ondragend|@ondragleave|@ondrop|@onlosecapture|@onmousedown|@onmouseenter|@onmouseleave|@onmousemove|@onmouseout|@onmouseover|@onmouseup|@onmousewheel]");
            if (((nodes != null) && (nodes.Count > 0)) && ((objHtmlDocument.DocumentNode.SelectNodes("//*[@onkeydown|@onkeypress|@onkeyup]") == null) && !this.RuleTesting("H209002", "9.3", "確保事件的啟發不要求一定得使用滑鼠", objHtmlDocument.DocumentNode.SelectNodes("//*[@onclick|@oncontextmenu|@ondragend|@ondragleave|@ondrop|@onlosecapture|@onmousedown|@onmouseenter|@onmouseleave|@onmousemove|@onmouseout|@onmouseover|@onmouseup|@onmousewheel]"), 0, string.Empty, "A+等級檢測項目", "MACHINE")))
            {
                this.m_isVerified = false;
            }
        }

        private XmlDocument CreateXmlDoc(string Root)
        {
            XmlDocument document = new XmlDocument();
            XmlNode newChild = document.CreateXmlDeclaration("1.0", "UTF-8", "");
            document.AppendChild(newChild);
            XmlNode node2 = document.CreateElement(Root);
            document.AppendChild(node2);
            return document;
        }

        private void CreateXmlNode(XmlDocument srcXmlDocument, string parentXPath, string NodeName, string innerText, string NodeAttribute, string NodeAttributeValue)
        {
            XmlNode node = srcXmlDocument.SelectSingleNode(parentXPath);
            if ((node != null) | !string.IsNullOrEmpty(NodeName))
            {
                XmlNode newChild = srcXmlDocument.CreateElement(NodeName);
                if (!string.IsNullOrEmpty(innerText))
                {
                    newChild.InnerText = innerText;
                }
                if (!string.IsNullOrEmpty(NodeAttribute))
                {
                    XmlAttribute attribute = srcXmlDocument.CreateAttribute(NodeAttribute);
                    attribute.Value = NodeAttributeValue;
                    newChild.Attributes.Append(attribute);
                }
                node.AppendChild(newChild);
            }
        }

        private void HumanTesting(HtmlDocument objHtmlDocument)
        {
            this.RuleTesting("H101210", "1.11", "以可及性的影像來替代ASCII文字藝術", null, 3, string.Empty, "第一優先等級", "MAN");
            this.RuleTesting("H101213", "1.14", "多媒體視覺影像呈現時，必須提供聽覺說明", null, 3, string.Empty, "第一優先等級", "MAN");
            this.RuleTesting("H101214", "1.15", "多媒體呈現時，必須同步產生相對應替代的語音或文字說明", null, 3, string.Empty, "第一優先等級", "MAN");
            this.RuleTesting("H104200", "4.1", "明確地指出網頁內容中語言的轉換", null, 3, string.Empty, "第一優先等級", "MAN");
            this.RuleTesting("H107200", "7.1", "確保網頁設計不會致使螢幕快速閃爍", null, 3, string.Empty, "第一優先等級", "MAN");
            this.RuleTesting("H111200", "11.1", "如果不能使這個網頁無障礙化，應提供另一個相等的無障礙網頁", null, 3, string.Empty, "第一優先等級", "MAN");
            this.RuleTesting("H114200", "14.1", "網頁內容要使用簡單易懂的文字", null, 3, string.Empty, "第一優先等級", "MAN");
        }

        private void MachineAndHumanTesting(HtmlDocument objHtmlDocument)
        {
            this.CreateXmlNode(this.m_XDoc, "/RESULT/DETAIL/TITLE_DEGREE[@name='第一優先等級']", "MAN", "", "name", "第一優先等級人工檢測項目");
            this.RuleTesting("H101105", "1.6", "當影像地圖使用為上傳按鈕時，每一作用區域必須分別使用不同的按鈕", objHtmlDocument.DocumentNode.SelectNodes("//input[@type='image' and @usemap!='']"), 0, string.Empty, "第一優先等級", "MAN");
            this.RuleTesting("H101106", "1.7", "當ALT屬性的文字陳述大於150個英文字元時，考慮另外提供文字敘述", objHtmlDocument.DocumentNode.SelectNodes("//*[@alt]"), 6, "alt>150", "第一優先等級", "MAN");
            this.RuleTesting("H101108", "1.9", "圖形替代文字陳述不夠清晰時，提供更多的文字描述(如使用longdesc屬性)", objHtmlDocument.DocumentNode.SelectNodes("//img[@alt!='']"), 1, "longdesc", "第一優先等級", "MAN");
            this.RuleTesting("H101109", "1.10", "所有語音檔案必須有文字旁白", objHtmlDocument.DocumentNode.SelectNodes("//a[@href!='']"), 4, "href", "第一優先等級", "MAN");
            this.RuleTesting("H101111", "1.12", "視訊中的聲音必須提供同步文字型態的旁白", objHtmlDocument.DocumentNode.SelectNodes("//embed[@src!='']"), 4, "src", "第一優先等級", "MAN");
            this.RuleTesting("H101112", "1.13", "伺服器端影像地圖中的超連結必須在網頁中有額外對應的文字超連結", objHtmlDocument.DocumentNode.SelectNodes("//img[@ismap]"), 0, string.Empty, "第一優先等級", "MAN");
            this.RuleTesting("H102100", "2.1", "確保所有藉由顏色所傳達出來的訊息，在沒有顏色後仍然能夠傳達出來", objHtmlDocument.DocumentNode.SelectNodes("//img|//*"), 5, "color", "第一優先等級", "MAN");
            this.RuleTesting("H105100", "5.1", "對於每一個存放資料的表格（不是用來排版），標示出行和列的標題", objHtmlDocument.DocumentNode.SelectNodes("//table"), 0, string.Empty, "第一優先等級", "MAN");
            this.RuleTesting("H105101", "5.2", "表格中超過二行/列以上的標題，須以結構化的標記確認彼此間的結構與關係", objHtmlDocument.DocumentNode.SelectNodes("//table"), 0, string.Empty, "第一優先等級", "MAN");
            this.RuleTesting("H106100", "6.1", "使用 CSS 樣式表編排的文件需確保在除去樣式表後仍然能夠閱讀", objHtmlDocument.DocumentNode.SelectNodes("//link"), 0, string.Empty, "第一優先等級", "MAN");
            this.RuleTesting("H106102", "6.3", "使用Script語言需指定不支援Script時的辦法", objHtmlDocument.DocumentNode.SelectNodes("//script"), 0, string.Empty, "第一優先等級", "MAN");
            this.RuleTesting("H106103", "6.4", "若網頁內的程式物件沒有作用時，確保網頁內容仍然可以傳達", objHtmlDocument.DocumentNode.SelectNodes("//applet"), 1, "alt", "第一優先等級", "MAN");
            this.RuleTesting("H108100", "8.1", "對由scripts、applets及objects所產生之資訊，提供可及性替代方式", objHtmlDocument.DocumentNode.SelectNodes("//script|//applet|//object"), 0, string.Empty, "第一優先等級", "MAN");
            this.RuleTesting("H109100", "9.1", "盡量使用客戶端影像地圖替代伺服器端影像地圖連結", objHtmlDocument.DocumentNode.SelectNodes("//img[@ismap]"), 0, string.Empty, "第一優先等級", "MAN");
        }

        private void MachineTesting(HtmlDocument objHtmlDocument)
        {
            this.CreateXmlNode(this.m_XDoc, "/RESULT/DETAIL/TITLE_DEGREE[@name='第一優先等級']", "MACHINE", "", "name", "第一優先等級機器檢測項目");
            if (!this.RuleTesting("H101000", "1.1", "圖片需要加上替代文字說明", objHtmlDocument.DocumentNode.SelectNodes("//img"), 1, "alt", "第一優先等級", "MACHINE"))
            {
                this.m_isVerified = false;
            }
            if (!this.RuleTesting("H101001", "1.2", "對於applet提供替代文字說明", objHtmlDocument.DocumentNode.SelectNodes("//applet"), 1, "alt", "第一優先等級", "MACHINE"))
            {
                this.m_isVerified = false;
            }
            if (!this.RuleTesting("H101002", "1.3", "對於object提供替代文字說明", objHtmlDocument.DocumentNode.SelectNodes("//object"), 2, string.Empty, "第一優先等級", "MACHINE"))
            {
                this.m_isVerified = false;
            }
            if (!this.RuleTesting("H101003", "1.4", "對於表單中的圖形按鍵提供替代文字說明", objHtmlDocument.DocumentNode.SelectNodes("//input[@type='image']"), 1, "alt", "第一優先等級", "MACHINE"))
            {
                this.m_isVerified = false;
            }
            if (!this.RuleTesting("H101004", "1.5", "影像地圖區域需要加上替代文字說明", objHtmlDocument.DocumentNode.SelectNodes("//map/area"), 1, "alt", "第一優先等級", "MACHINE"))
            {
                this.m_isVerified = false;
            }
            if (!this.RuleTesting("H101007", "1.8", "提供LONGDESC以外的描述性超連結(如使用以D為提示的超連結)，來描述LONGDESC的內容", objHtmlDocument.DocumentNode.SelectNodes("//*/@longdesc"), 7, "longdesc", "第一優先等級", "MACHINE"))
            {
                this.m_isVerified = false;
            }
            if (!this.RuleTesting("H106001", "6.2", "頁框連結必須是HTML檔案", objHtmlDocument.DocumentNode.SelectNodes("//frame|iframe"), 1, "src", "第一優先等級", "MACHINE"))
            {
                this.m_isVerified = false;
            }
            if (!this.RuleTesting("H112000", "12.1", "需要定義每個頁框的名稱", objHtmlDocument.DocumentNode.SelectNodes("//frame|iframe"), 1, "title", "第一優先等級", "MACHINE"))
            {
                this.m_isVerified = false;
            }
        }

        private bool RuleTesting(string strCode, string strSerial, string strDescript, HtmlNodeCollection nodeList, short intMode, string strAttributeName, string CheckLevel, string type)
        {
            string str = "";
            int num = 0;
            bool flag = true;
            if (nodeList != null)
            {
                foreach (HtmlNode node in nodeList)
                {
                    string[] strArray;
                    int length;
                    switch (intMode)
                    {
                        case 0:
                        {
                            str = str + string.Format(this.m_strTagFormat, node.Line);
                            num++;
                            flag = false;
                            continue;
                        }
                        case 1:
                        {
                            if (string.IsNullOrEmpty(node.GetAttributeValue(strAttributeName, string.Empty)))
                            {
                                str = str + string.Format(this.m_strTagFormat, node.Line);
                                num++;
                                flag = false;
                            }
                            continue;
                        }
                        case 2:
                        {
                            if (string.IsNullOrEmpty(Regex.Replace(node.InnerText, @"\n\r|\s", string.Empty)))
                            {
                                str = str + string.Format(this.m_strTagFormat, node.Line);
                                num++;
                                flag = false;
                            }
                            continue;
                        }
                        case 3:
                        {
                            continue;
                        }
                        case 4:
                        {
                            string attributeValue = node.GetAttributeValue(strAttributeName, string.Empty);
                            if ((attributeValue.IndexOf(".") >= 0) && (this.m_strMediaCheck.IndexOf(attributeValue.Substring(attributeValue.LastIndexOf("."))) >= 0))
                            {
                                str = str + string.Format(this.m_strTagFormat, node.Line);
                                flag = false;
                            }
                            continue;
                        }
                        case 5:
                        {
                            if ((node.Name == "img") | (node.OuterHtml.IndexOf(strAttributeName) >= 0))
                            {
                                str = str + string.Format(this.m_strTagFormat, node.Line);
                                num++;
                                flag = false;
                            }
                            continue;
                        }
                        case 6:
                        {
                            strArray = strAttributeName.Split(new char[] { '<', '>' });
                            length = node.GetAttributeValue(strArray[0], string.Empty).Length;
                            if (strAttributeName.IndexOf('<') <= 0)
                            {
                                break;
                            }
                            if (length < int.Parse(strArray[1]))
                            {
                                str = str + string.Format(this.m_strTagFormat, node.Line);
                                num++;
                                flag = false;
                            }
                            continue;
                        }
                        case 7:
                        {
                            string str3 = node.GetAttributeValue(strAttributeName, string.Empty);
                            if (this.m_objHtmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@href='{0}' or @src='{0}']", str3)) == null)
                            {
                                str = str + string.Format(this.m_strTagFormat, node.Line);
                                num++;
                                flag = false;
                            }
                            continue;
                        }
                        default:
                        {
                            continue;
                        }
                    }
                    if ((strAttributeName.IndexOf('>') > 0) && (length > int.Parse(strArray[1])))
                    {
                        str = str + string.Format(this.m_strTagFormat, node.Line);
                        num++;
                        flag = false;
                    }
                }
            }
            else if (intMode == 3)
            {
                this.CreateXmlNode(this.m_XDoc, string.Format("/RESULT/DETAIL/TITLE_DEGREE[@name='{0}']/{1}", CheckLevel, type), "LIST", string.Format("{0}:<a href='sample/{1}.html' target='_blank'>{1}</a>&nbsp;{2}&nbsp;", strSerial, strCode, strDescript), string.Empty, string.Empty);
                this.CreateXmlNode(this.m_XDoc, string.Format("/RESULT/DETAIL/TITLE_DEGREE[@name='{0}']/{1}", CheckLevel, type), "CONTENT", "<br/>", string.Empty, string.Empty);
            }
            if (str.Length > 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            if (!string.IsNullOrEmpty(str))
            {
                this.CreateXmlNode(this.m_XDoc, string.Format("/RESULT/DETAIL/TITLE_DEGREE[@name='{0}']/{1}", CheckLevel, type), "LIST", string.Format("{0}:<a href='sample/{1}.html' target='_blank'>{1}</a>&nbsp;{2}&nbsp;({3}個)", new object[] { strSerial, strCode, strDescript, num }), string.Empty, string.Empty);
                this.CreateXmlNode(this.m_XDoc, string.Format("/RESULT/DETAIL/TITLE_DEGREE[@name='{0}']/{1}", CheckLevel, type), "CONTENT", " --第" + str + "行 <br />", string.Empty, string.Empty);
            }
            return flag;
        }

        private XmlDocument WriteXML()
        {
            XmlDocument srcXmlDocument = this.CreateXmlDoc("RESULT");
            this.CreateXmlNode(srcXmlDocument, "/RESULT", "CHECK_DEGREE", this.m_Checklevel, "", "");
            this.CreateXmlNode(srcXmlDocument, "/RESULT", "CHECK_DATETIME", Strings.Format(DateTime.Now, "yyyy/MM/dd hh:mm:ss"), "", "");
            this.CreateXmlNode(srcXmlDocument, "/RESULT", "DETAIL", "", "", "");
            this.CreateXmlNode(srcXmlDocument, "/RESULT", "ORIGINAL_SRC", this.m_strNewContents, "", "");
            return srcXmlDocument;
        }

        public string CheckDegree
        {
            get
            {
                return this.m_Checklevel;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return this.m_ErrorMessage;
            }
        }

        public bool isPass
        {
            get
            {
                return this.m_isVerified;
            }
        }

        public string Result
        {
            get
            {
                return this.m_Result;
            }
        }

        public enum ckdegreeEnum
        {
            A,
            APlus,
            AAPlus,
            AAAPlus
        }

        public enum ContentEncodingEnum
        {
            UTF8,
            big5
        }

        public enum ContnetTypeEnum
        {
            web,
            cont,
            file
        }
    }
}


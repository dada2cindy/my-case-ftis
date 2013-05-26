using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using WuDada.Accessibility.FreeGO;

namespace FTISWeb.Utility
{
    public static class AccessibilityUtil
    {
        ////預設檢測A+
        private static Accessibility.ckdegreeEnum m_Type = Accessibility.ckdegreeEnum.APlus;

        /// <summary>
        /// 檢測無障礙
        /// </summary>
        /// <param name="htmlValue"></param>
        /// <param name="type">1.APlus</param>
        /// <returns></returns>
        public static bool CheckFreeGO(string htmlValue)
        {
            Accessibility accessibility = new Accessibility(htmlValue, Accessibility.ContnetTypeEnum.cont, Accessibility.ContentEncodingEnum.UTF8, m_Type);
            return accessibility.isPass;
        }

        public static string CheckFreeGOAndReturnMsg(string htmlValue)
        {
            StringBuilder sbMsg = new StringBuilder();

            Accessibility accessibility = new Accessibility(htmlValue, Accessibility.ContnetTypeEnum.cont, Accessibility.ContentEncodingEnum.UTF8, m_Type);

            if (accessibility.isPass)
            {
                sbMsg.Append("您已通過{自動}檢測的等級 : " + accessibility.CheckDegree + "<br />");
                return sbMsg.ToString();
            }
            else
            {
                sbMsg.Append("您未通過{自動}檢測的等級 : " + accessibility.CheckDegree + "<br />");
            }

            //建立XmlDocument物件
            XmlDocument Xdoc = new XmlDocument();
            //載入元件所回傳的字串
            Xdoc.LoadXml(accessibility.Result);
            //篩選出檢測等級的詳細內容
            XmlNodeList DETAIL = Xdoc.SelectNodes("/RESULT/DETAIL/TITLE_DEGREE");
            XmlDocument subXDoc = new XmlDocument();
            foreach (XmlNode thisObject in DETAIL)
            {
                subXDoc.LoadXml(thisObject.OuterXml);
                //機器檢測
                if (subXDoc.SelectSingleNode("//MACHINE").HasChildNodes)
                {
                    XmlNode MACHINEAttribute = subXDoc.SelectSingleNode("//MACHINE");
                    sbMsg.Append("<br />");
                    sbMsg.Append(MACHINEAttribute.Attributes[0].InnerText + "<br />");

                    XmlNodeList MACHINE = subXDoc.SelectNodes("//MACHINE");
                    foreach (XmlNode thisObject2 in MACHINE)
                    {
                        sbMsg.Append(thisObject2.InnerText);
                    }
                }
            }

            sbMsg.Append("<br />");

            //人工檢測
            //if (subXDoc.SelectSingleNode("//MAN").HasChildNodes)
            //{
            //    XmlNode MANAttribute = subXDoc.SelectSingleNode("//MAN");
            //    sbMsg.Append("<br />");
            //    sbMsg.Append(MANAttribute.Attributes[0].InnerText + "<br />");

            //    XmlNodeList MAN = subXDoc.SelectNodes("//MAN");
            //    foreach (XmlNode thisObject2 in MAN)
            //    {
            //        sbMsg.Append(thisObject2.InnerText);                    
            //    }
            //}

            //受檢測的網頁內容+行號
            XmlNode ORIGINAL_SRC_Node = Xdoc.SelectSingleNode("/RESULT/ORIGINAL_SRC");
            sbMsg.Append(ORIGINAL_SRC_Node.InnerText);

            return sbMsg.ToString().Replace("sample/", "/Content/freego/sample/");
        }
    }
}
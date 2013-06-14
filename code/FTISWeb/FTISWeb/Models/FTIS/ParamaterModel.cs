using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FTISWeb.Models
{
    public class ParamaterModel
    {
        #region Constructor
        public ParamaterModel(string actionName, string controllerName)
        {
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.EntityId = controllerName + "Id";
            if (controllerName == "SubNormClass")
            {
                this.EntityId = "NormClassId";
            }
            this.EntityName = "Name";
        }

        public ParamaterModel(string actionName, string controllerName, string conditions)
            : this(actionName, controllerName)
        {
            this.Conditions = conditions;

            SetColumnsParamater();
        }
        #endregion

        #region Property
        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string Conditions { get; set; }

        public string EntityId { get; set; }

        public string EntityName { get; set; }

        private string[] m_VisibleColumns = new string[] { };

        #endregion

        private void SetColumnsParamater()
        {
            switch (ControllerName)
            {
                case "NewsClass":
                    m_VisibleColumns = new string[] { "Name", "SortId", "GetStr_Status", "NameENG" };
                    break;
                case "NewsType":
                    m_VisibleColumns = new string[] { "Name", "Content", "SortId", "GetStr_Status" };
                    break;
                case "HomeNews":
                    m_VisibleColumns = new string[] { "Name", "AUrl", "SortId", "GetStr_Status" };
                    break;
                case "Industry":
                    m_VisibleColumns = new string[] { "Name", "SortId", "GetStr_Status" };
                    break;
                case "PublicationClass":
                    m_VisibleColumns = new string[] { "Name", "SortId", "GetStr_Status" };
                    break;
                case "NormClass":
                    m_VisibleColumns = new string[] { "Name", "SortId", "GetStr_Status", "ParentEntity" };
                    break;
                case "Activity":
                    m_VisibleColumns = new string[] { "Name", "ArticleName", "SortId", "GetStr_Status", "ActDate" };
                    break;
                case "Download":
                    m_VisibleColumns = new string[] { "Name", "SortId", "GetStr_Status", "Vister", "Downer", "GetStr_IsOut" };
                    break;
                case "Curriculum":
                    m_VisibleColumns = new string[] { "Name", "ArticleName", "SortId", "GetStr_Status", "Vister" };
                    break;
                case "Brief":
                    m_VisibleColumns = new string[] { "Name", "ArticleName", "SortId", "GetStr_Status", "AYear", "GetStr_PostDate" };
                    break;
                case "Publication":
                    m_VisibleColumns = new string[] { "Name", "SortId", "PublicationClass.Name", "PubNo" };
                    break;
                case "News":
                    m_VisibleColumns = new string[] { "Name", "ArticleName", "NewsType.Name", "NewsClass.Name", "SortId", "GetStr_Status", "Vister", "Printer", "Emailer", "GetStr_IsHome", "GetStr_IsNew", "GetStr_IsOut", "GetStr_PostDate" };
                    break;
                case "MNorm":
                    m_VisibleColumns = new string[] { "Name", "ArticleName", "SortId", "GetStr_Status", "GetStr_PostDate" };
                    break;
                case "ApplicationClass":
                    m_VisibleColumns = new string[] { "Name", "SortId", "GetStr_Status" };
                    break;
                case "Application":
                    m_VisibleColumns = new string[] { "Name", "ArticleName", "SortId", "ApplicationClass.Name", "GetStr_PostDate" };
                    break;
                case "LinksClass":
                    m_VisibleColumns = new string[] { "Name", "SortId", "GetStr_Status" };
                    break;
                case "Links":
                    m_VisibleColumns = new string[] { "Name", "ArticleName", "SortId", "LinksClass.Name", "GetStr_Status", "GetStr_LangId" };
                    break;
                case "Epaper":
                    m_VisibleColumns = new string[] { "Name", "ArticleName", "ENo", "SortId", "GetStr_Status", "Vister", "GetStr_PostDate" };
                    break;
                case "EpaperEmail":
                    m_VisibleColumns = new string[] { "Company", "Name", "EpaperEmailName", "Email", "GetStr_Status", "GetStr_RegDate" };
                    break;
                case "Technology":
                    m_VisibleColumns = new string[] { "Company", "CompanyENG", "Charge", "Contact", "Tel", "Email", "GetStr_Status", "GetStr_ExpiredDate", "Node.Name" };
                    break;
                case "QuestionClass":
                    m_VisibleColumns = new string[] { "Name", "SortId", "GetStr_Status" };
                    break;
                case "Question":
                    m_VisibleColumns = new string[] { "Name", "ArticleName", "SortId", "QuestionClass.Name", "GetStr_PostDate", "GetStr_Status" };
                    break;
                case "GreenFactoryClass":
                    m_VisibleColumns = new string[] { "Name", "SortId", "GetStr_Status" };
                    break;
                case "GreenFactory":
                    m_VisibleColumns = new string[] { "Name", "ArticleName", "SortId", "GreenFactoryClass.Name", "GetStr_PostDate", "GetStr_Status" };
                    break;
                case "Green":
                    m_VisibleColumns = new string[] { "Name", "GetStr_Status" };
                    break;
                case "Report":
                    m_VisibleColumns = new string[] { "Company", "GetStr_CompanyTrade", "SortId", "GetStr_Status", "GetStr_PostDate", "ReportName", "PostYear", "ReportYear", "ReportName" };
                    break;
                case "SubNormClass":
                    m_VisibleColumns = new string[] { "Name", "SortId", "GetStr_Status" };
                    break;
                case "Norm":
                    m_VisibleColumns = new string[] { "Name", "ArticleName", "NormClass.Name", "NormClassParent.Name", "SortId", "GetStr_Status" };
                    break;
                case "EpaperExamination":
                    m_VisibleColumns = new string[] { "Name", "ExaminationName", "Industry.Name", "GetStr_PostDate" };
                    break;
                case "Examination":
                    m_VisibleColumns = new string[] { "Name", "ExaminationName", "Industry.Name", "GetStr_PostDate" };
                    break;
                case "Member":
                    m_VisibleColumns = new string[] { "Name", "Company", "LoginId", "MemberName", "Industry.Name", "GetStr_RegDate", "GetStr_Status" };
                    break;
            }
        }

        public string Hidden(string columnName)
        {
            if (!m_VisibleColumns.Contains(columnName))
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        public string GetColumnName(string columnName)
        {
            if (!m_VisibleColumns.Contains(columnName))
            {
                return m_VisibleColumns.ElementAtOrDefault(0);
            }
            else
            {
                return columnName;
            }
        }

        public string GetDisplayName(string columnName)
        {
            string displayName = string.Empty;

            switch (columnName)
            {
                case "Name":
                    if (m_VisibleColumns.Contains("ArticleName"))
                    {
                        displayName = "標題";
                    }
                    else if (m_VisibleColumns.Contains("EpaperEmailName"))
                    {
                        displayName = "姓名";
                    }
                    else if (m_VisibleColumns.Contains("ExaminationName"))
                    {
                        displayName = "姓名";
                    }
                    else if (m_VisibleColumns.Contains("MemberName"))
                    {
                        displayName = "姓名";
                    }
                    else
                    {
                        displayName = "名稱";
                    }
                    break;
                case "NameENG":
                    if (m_VisibleColumns.Contains("ArticleName"))
                    {
                        displayName = "標題(英)";
                    }
                    else
                    {
                        displayName = "名稱(英)";
                    }
                    break;
                case "GetStr_PostDate":
                    if (m_VisibleColumns.Contains("ExaminationName"))
                    {
                        displayName = "填寫日期";
                    }
                    else
                    {
                        displayName = "刊登日期";
                    }                    
                    break;
                case "GetStr_RegDate":
                    if (m_VisibleColumns.Contains("MemberName"))
                    {
                        displayName = "申請日期";
                    }
                    else
                    {
                        displayName = "訂/退閱時間";
                    }
                    break;
            }

            return displayName;
        }
    }
}
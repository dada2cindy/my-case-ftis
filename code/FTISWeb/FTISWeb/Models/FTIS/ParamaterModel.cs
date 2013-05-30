﻿using System;
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
    }
}
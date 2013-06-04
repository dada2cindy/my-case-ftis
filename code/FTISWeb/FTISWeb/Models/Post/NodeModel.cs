using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using FTIS;
using FTIS.Domain.Impl;
using FTIS.Service;
using FTISWeb.Security;
using FTISWeb.Helper;
using WuDada.Core.Generic.Util;
using System.ComponentModel;

namespace FTISWeb.Models
{
    public class NodeModel : AbstractNodeModel, ICheckFreeGO
    {
        public NodeModel()
        {
        }

        public NodeModel(int id)
        {
            LoadNode(id, false);
        }

        public NodeModel(int id, bool noLazy)
        {
            LoadNode(id, noLazy);
        }

        #region ICheckFreeGO 成員

        public bool ShowFreeGOMsg { get; set; }

        public string FreeGOColumnName { get; set; }

        #endregion
    }
}

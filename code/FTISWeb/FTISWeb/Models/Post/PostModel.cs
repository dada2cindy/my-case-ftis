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
    public class PostModel : AbstractPostModel, ICheckFreeGO
    {
        public PostModel()
        {
        }

        public PostModel(string id, bool noLazy)
        {
            LoadPost(int.Parse(DecryptId(id)), noLazy);
        }

        public PostModel(string id)
        {
            LoadPost(int.Parse(DecryptId(id)));
        }

        public PostModel(int id)
        {
            LoadPost(id, false);
        }

        public PostModel(int id, bool noLazy)
        {
            LoadPost(id, noLazy);
        }

        #region ICheckFreeGO 成員

        public bool ShowFreeGOMsg { get; set; }

        public string FreeGOColumnName { get; set; }

        #endregion
    }
}

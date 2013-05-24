using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTIS.Domain
{
    public enum SiteEntities
    {
        /// <summary>
        /// 無
        /// </summary>
        None,

        /// <summary>
        /// 網站管理
        /// </summary>
        Master,

        /// <summary>
        /// 關於我們
        /// </summary>
        AboutUs,

        /// <summary>
        /// 新聞動態
        /// </summary>
        News,

        /// <summary>
        /// 即時訊息
        /// </summary>
        HomeNews,

        /// <summary>
        /// 活動訊息
        /// </summary>
        Activity,

        /// <summary>
        /// 會員專區
        /// </summary>
        Member,

        /// <summary>
        /// 下載專區
        /// </summary>
        Download,

        /// <summary>
        /// 標準/規範管理
        /// </summary>
        Norm,

        /// <summary>
        /// 環境宣告/碳足跡設置
        /// </summary>
        Carbon,

        /// <summary>
        /// 發展應用管理/企業社會責任
        /// </summary>
        Application,

        /// <summary>
        /// 評比介紹管理
        /// </summary>
        Grade,

        /// <summary>
        /// 產業服務專區設置
        /// </summary>
        Question,

        /// <summary>
        /// 網路資源管理
        /// </summary>
        Links,

        /// <summary>
        /// 電子報管理
        /// </summary>
        Epaper,

        /// <summary>
        /// 首頁四季設置
        /// </summary>
        Season,

        /// <summary>
        /// 滿意度問卷管理
        /// </summary>
        Examination,

        /// <summary>
        /// 綠色工廠
        /// </summary>
        GreenFactory
    }

    public enum SiteOperations
    {
        None = 0,
        Read = 1,
        Create = 2,
        Edit = 4,
        Delete = 8
    }
}

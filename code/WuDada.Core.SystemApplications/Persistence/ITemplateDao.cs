using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.SystemApplications.Domain;

namespace WuDada.Core.SystemApplications.Persistence
{
    public interface ITemplateDao
    {
        /// <summary>
        /// 新增樣板
        /// </summary>
        /// <param name="templateVO">被新增的樣板</param>
        /// <returns>新增後的樣板</returns>
        TemplateVO CreateTemplate(TemplateVO templateVO);

        /// <summary>
        /// 更新樣板
        /// </summary>
        /// <param name="templateVO">被更新的樣板</param>
        /// <returns>更新後的樣板</returns>
        TemplateVO UpdateTemplate(TemplateVO templateVO);

        /// <summary>
        /// 取得樣板 By 識別碼
        /// </summary>
        /// <param name="templateId">識別碼</param>
        /// <returns>樣板</returns>
        TemplateVO GetTemplateById(int templateId);

        /// <summary>
        /// 取得樣板清單
        /// </summary>
        /// <param name="type">樣板類別</param>
        /// <returns>樣板清單</returns>
        IList<TemplateVO> GetTemplateList(TemplateVO.Type type);
    }
}

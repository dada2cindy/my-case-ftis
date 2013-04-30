using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Persistence.Ado;
using WuDada.Core.Persistence;
using WuDada.Core.SystemApplications.Domain;
using NHibernate.Criterion;

namespace WuDada.Core.SystemApplications.Persistence
{
    public class TemplateDao : AdoDao, ITemplateDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        /// <summary>
        /// 新增樣板
        /// </summary>
        /// <param name="templateVO">被新增的樣板</param>
        /// <returns>新增後的樣板</returns>
        public TemplateVO CreateTemplate(TemplateVO templateVO)
        {
            NHibernateDao.Insert(templateVO);

            return templateVO;
        }

        /// <summary>
        /// 更新樣板
        /// </summary>
        /// <param name="templateVO">被更新的樣板</param>
        /// <returns>更新後的樣板</returns>
        public TemplateVO UpdateTemplate(TemplateVO templateVO)
        {
            NHibernateDao.Update(templateVO);

            return templateVO;
        }

        /// <summary>
        /// 取得樣板 By 識別碼
        /// </summary>
        /// <param name="templateId">識別碼</param>
        /// <returns>樣板</returns>
        public TemplateVO GetTemplateById(int templateId)
        {
            return NHibernateDao.GetVOById<TemplateVO>(templateId);
        }

        /// <summary>
        /// 取得樣板清單
        /// </summary>
        /// <param name="type">樣板類別</param>
        /// <returns>樣板清單</returns>
        public IList<TemplateVO> GetTemplateList(TemplateVO.Type type)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<TemplateVO>();
            dCriteria.Add(Expression.Eq("TemplateType", (int)type));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<TemplateVO>(dCriteria);
        }
    }
}

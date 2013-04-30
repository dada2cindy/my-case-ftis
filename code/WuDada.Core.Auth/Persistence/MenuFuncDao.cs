using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Persistence.Ado;
using WuDada.Core.Persistence;
using NHibernate.Criterion;
using WuDada.Core.Auth.Domain;

namespace WuDada.Core.Auth.Persistence
{
    public class MenuFuncDao : AdoDao, IMenuFuncDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        /// <summary>
        /// 取得全部的後台角色清單
        /// </summary>
        /// <returns>全部的後台角色清單</returns>
        public IList<MenuFuncVO> GetTopMenuFunc()
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<MenuFuncVO>();
            dCriteria.Add(Expression.IsNull("ParentMenu"));
            dCriteria.AddOrder(Order.Asc("ListOrder"));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<MenuFuncVO>(dCriteria);
        }

        /// <summary>
        /// 新增後台功能
        /// </summary>
        /// <param name="menuFuncVO">被新增的後台功能</param>
        /// <returns>新增後的後台功能</returns>
        public MenuFuncVO CreateMenuFunc(MenuFuncVO menuFuncVO)
        {
            NHibernateDao.Insert(menuFuncVO);

            return menuFuncVO;
        }

        /// <summary>
        /// 取出不為第一層的後台功能
        /// </summary>
        /// <returns>不為第一層的後台功能</returns>
        public IList<MenuFuncVO> GetNotTopMenuFunc()
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<MenuFuncVO>();
            dCriteria.Add(Expression.IsNotNull("ParentMenu"));
            dCriteria.AddOrder(Order.Asc("ListOrder"));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<MenuFuncVO>(dCriteria);
        }

        /// <summary>
        /// 取出子後台功能清單 By 父層Id
        /// </summary>
        /// <param name="parentId">父層Id</param>
        /// <returns>子後台功能清單</returns>
        public IList<MenuFuncVO> GetMenuFuncByParentId(int parentId)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<MenuFuncVO>();
            dCriteria.CreateCriteria("ParentMenu").Add(Expression.Eq("MenuFuncId", parentId));
            dCriteria.AddOrder(Order.Asc("ListOrder"));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<MenuFuncVO>(dCriteria);
        }

        /// <summary>
        /// 取得後台功能 By 功能Id
        /// </summary>
        /// <param name="menuFuncId">功能Id</param>
        /// <returns>後台功能</returns>
        public MenuFuncVO GetMenuFuncById(int menuFuncId)
        {
            return NHibernateDao.GetVOById<MenuFuncVO>(menuFuncId);
        }

        /// <summary>
        /// 取得後台功能清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        public IList<MenuFuncVO> GetMenuFuncList(string queryString, int pageIndex, int pageSize)
        {
            return NHibernateDao.SearchByWhere<MenuFuncVO>(queryString, pageIndex, pageSize);
        }
    }
}

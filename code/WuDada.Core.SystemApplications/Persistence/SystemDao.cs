using System.Collections.Generic;
using WuDada.Core.Persistence;
using WuDada.Core.Persistence.Ado;
using WuDada.Core.SystemApplications.Domain;
using NHibernate.Criterion;

namespace WuDada.Core.SystemApplications.Persistence
{
    public class SystemDao : AdoDao, ISystemDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        /// <summary>
        /// 新增項目參數
        /// </summary>
        /// <param name="itemParamVO">被新增的項目參數</param>
        /// <returns>新增後的項目參數</returns>
        public ItemParamVO CreateItemParam(ItemParamVO itemParamVO)
        {
            //            string cmdText = @"INSERT INTO CORE_ITEM_PARAM(
            //    Classify, Name, Value, Deleted)
            //VALUES(
            //    @Classify, @Name, @Value, @Deleted) ";

            //            //抓取新增後的id
            //            cmdText += " SELECT SCOPE_IDENTITY() "; 
            //            IDbParameters dbParameters = CreateDbParameters();
            //            dbParameters.Add("Classify", DbType.String).Value = itemParamVO.Classify;
            //            dbParameters.Add("Name", DbType.String).Value = itemParamVO.Name;
            //            dbParameters.Add("Value", DbType.String).Value = itemParamVO.Value;
            //            dbParameters.Add("Deleted", DbType.Boolean).Value = itemParamVO.Deleted;

            //            int id = Convert.ToInt32(AdoTemplate.ExecuteScalar(CommandType.Text, cmdText, dbParameters));
            //            itemParamVO.ItemParamId = id;

            //            return itemParamVO;

            NHibernateDao.Insert(itemParamVO);

            return itemParamVO;
        }

        /// <summary>
        /// 更新項目參數
        /// </summary>
        /// <param name="itemParamVO">被更新的項目參數</param>
        /// <returns>更新後的項目參數</returns>
        public ItemParamVO UpdateItemParam(ItemParamVO itemParamVO)
        {
            NHibernateDao.Update(itemParamVO);

            return itemParamVO;
        }

        /// <summary>
        /// 刪除項目參數
        /// </summary>
        /// <param name="itemParamVO">被刪除的項目參數</param>
        public void DeleteItemParam(ItemParamVO itemParamVO)
        {
            NHibernateDao.Delete(itemParamVO);
        }

        /// <summary>
        /// 取得項目參數 By 識別碼
        /// </summary>
        /// <param name="itemParamId">識別碼</param>
        /// <returns>項目參數</returns>
        public ItemParamVO GetItemParamById(int itemParamId)
        {
            return NHibernateDao.GetVOById<ItemParamVO>(itemParamId);
        }

        /// <summary>
        /// 取得項目參數清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <returns>項目參數清單</returns>
        public IList<ItemParamVO> GetItemParamList(string queryString)
        {
            return NHibernateDao.SearchByWhere<ItemParamVO>(queryString);
        }

        /// <summary>
        /// 取得項目參數清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <returns>項目參數清單</returns>
        public IList<ItemParamVO> GetItemParamList(string queryString, int pageIndex, int pageSize)
        {
            return NHibernateDao.SearchByWhere<ItemParamVO>(queryString, pageIndex, pageSize);
        }

        /// <summary>
        /// 取得全部的項目參數清單
        /// </summary>
        /// <returns>全部的項目參數清單</returns>
        public IList<ItemParamVO> GetAllItemParamList()
        {
            return NHibernateDao.GetAllVO<ItemParamVO>();
        }

        /// <summary>
        /// 取得全部的項目參數清單
        /// </summary>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <returns>全部的項目參數清單</returns>
        public IList<ItemParamVO> GetAllItemParamList(int pageIndex, int pageSize)
        {
            return NHibernateDao.GetAllVO<ItemParamVO>(pageIndex, pageSize);
        }

        /// <summary>
        /// 取得全部的項目參數清單
        /// </summary>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>全部的項目參數清單</returns>
        public IList<ItemParamVO> GetAllItemParamList(int pageIndex, int pageSize, string sortField, bool sortDesc)
        {
            if (sortDesc)
            {
                return NHibernateDao.GetAllVO<ItemParamVO>(pageIndex, pageSize, Order.Desc(sortField));
            }
            else
            {
                return NHibernateDao.GetAllVO<ItemParamVO>(pageIndex, pageSize, Order.Asc(sortField));
            }
        }

        /// <summary>
        /// 新增系統參數
        /// </summary>
        /// <param name="systemParamVO">被新增的系統參數</param>
        /// <returns>新增後的系統參數</returns>
        public SystemParamVO CreateSystemParam(SystemParamVO systemParamVO)
        {
            NHibernateDao.Insert(systemParamVO);

            return systemParamVO;
        }

        /// <summary>
        /// 更新系統參數
        /// </summary>
        /// <param name="systemParamVO">被更新的系統參數</param>
        /// <returns>更新後的系統參數</returns>
        public SystemParamVO UpdateSystemParam(SystemParamVO systemParamVO)
        {
            NHibernateDao.Update(systemParamVO);

            return systemParamVO;
        }

        /// <summary>
        /// 刪除系統參數
        /// </summary>
        /// <param name="systemParamVO">被刪除的系統參數</param>
        public void DeleteSystemParam(SystemParamVO systemParamVO)
        {
            NHibernateDao.Delete(systemParamVO);
        }

        /// <summary>
        /// 取得系統參數 By 識別碼
        /// </summary>
        /// <param name="systemParamId">識別碼</param>
        /// <returns>系統參數</returns>
        public SystemParamVO GetSystemParamById(int systemParamId)
        {
            return NHibernateDao.GetVOById<SystemParamVO>(systemParamId);
        }

        /// <summary>
        /// 取得不含刪除註記的所有參數清單
        /// </summary>
        /// <returns></returns>
        public IList<ItemParamVO> GetAllItemParamByNoDel()
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<ItemParamVO>();
            dCriteria.Add(Expression.Eq("Deleted", false));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<ItemParamVO>(dCriteria);
           
        }

        /// <summary>
        /// 取得不含刪除註記的所有參數清單
        /// </summary>
        /// <param name="classify">選項分類</param>
        /// <returns>不含刪除註記的所有參數清單</returns>
        public IList<ItemParamVO> GetAllItemParamByNoDel(string classify)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<ItemParamVO>();
            dCriteria.Add(Expression.Eq("Classify", classify));
            dCriteria.Add(Expression.Eq("Deleted", false));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<ItemParamVO>(dCriteria);
        }
    }
}

using System.Collections.Generic;
using WuDada.Core.SystemApplications.Domain;
using WuDada.Core.SystemApplications.Persistence;


namespace WuDada.Core.SystemApplications.Service.Impl
{
    public class SystemService : ISystemService
    {       
        public ISystemDao SystemDao { get; set; }

        public int RootSystemParamId { get; set; }

        /// <summary>
        /// 新增項目參數
        /// </summary>
        /// <param name="itemParamVO">被新增的項目參數</param>
        /// <returns>新增後的項目參數</returns>
        public ItemParamVO CreateItemParam(ItemParamVO itemParamVO)
        {
            return SystemDao.CreateItemParam(itemParamVO);                    
        }

        /// <summary>
        /// 更新項目參數
        /// </summary>
        /// <param name="itemParamVO">被更新的項目參數</param>
        /// <returns>更新後的項目參數</returns>
        public ItemParamVO UpdateItemParam(ItemParamVO itemParamVO)
        {
            return SystemDao.UpdateItemParam(itemParamVO);
        }

        /// <summary>
        /// 刪除項目參數
        /// </summary>
        /// <param name="itemParamVO">被刪除的項目參數</param>
        public void DeleteItemParam(ItemParamVO itemParamVO)
        {
            SystemDao.DeleteItemParam(itemParamVO);
        }

        /// <summary>
        /// 取得項目參數 By 識別碼
        /// </summary>
        /// <param name="itemParamId">識別碼</param>
        /// <returns>項目參數</returns>
        public ItemParamVO GetItemParamById(int itemParamId)
        {
            return SystemDao.GetItemParamById(itemParamId);
        }

        /// <summary>
        /// 取得項目參數清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <returns>項目參數清單</returns>
        public IList<ItemParamVO> GetItemParamList(string queryString)
        {
            return SystemDao.GetItemParamList(queryString);
        }

        /// <summary>
        /// 取得不含刪除註記的所有參數清單
        /// </summary>
        /// <returns>不含刪除註記的所有參數清單</returns>
        public IList<ItemParamVO> GetAllItemParamByNoDel()
        {
            return SystemDao.GetAllItemParamByNoDel();
        }

        /// <summary>
        /// 取得不含刪除註記的所有參數清單
        /// </summary>
        /// <param name="classify">選項分類</param>
        /// <returns>不含刪除註記的所有參數清單</returns>
        public IList<ItemParamVO> GetAllItemParamByNoDel(string classify)
        {
            return SystemDao.GetAllItemParamByNoDel(classify);
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
            return SystemDao.GetItemParamList(queryString, pageIndex, pageSize);
        }

        /// <summary>
        /// 取得全部的項目參數清單
        /// </summary>
        /// <returns>全部的項目參數清單</returns>
        public IList<ItemParamVO> GetAllItemParamList()
        {
            return SystemDao.GetAllItemParamList();
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
            if (string.IsNullOrEmpty(sortField))
            {
                return SystemDao.GetAllItemParamList(pageIndex, pageSize);                
            }

            return SystemDao.GetAllItemParamList(pageIndex, pageSize, sortField, sortDesc);
        }

        /// <summary>
        /// 新增系統參數
        /// </summary>
        /// <param name="systemParamVO">被新增的系統參數</param>
        /// <returns>新增後的系統參數</returns>
        public SystemParamVO CreateSystemParam(SystemParamVO systemParamVO)
        {
            return SystemDao.CreateSystemParam(systemParamVO);     
        }

        /// <summary>
        /// 更新系統參數
        /// </summary>
        /// <param name="systemParamVO">被更新的系統參數</param>
        /// <returns>更新後的系統參數</returns>
        public SystemParamVO UpdateSystemParam(SystemParamVO systemParamVO)
        {
            return SystemDao.UpdateSystemParam(systemParamVO);
        }

        /// <summary>
        /// 刪除系統參數
        /// </summary>
        /// <param name="systemParamVO">被刪除的系統參數</param>
        public void DeleteSystemParam(SystemParamVO systemParamVO)
        {
            SystemDao.DeleteSystemParam(systemParamVO);
        }

        /// <summary>
        /// 取得系統參數 By 識別碼
        /// </summary>
        /// <param name="systemParamId">識別碼</param>
        /// <returns>系統參數</returns>
        public SystemParamVO GetSystemParamById(int systemParamId)
        {
            return SystemDao.GetSystemParamById(systemParamId);
        }

        /// <summary>
        /// 取得系統參數 By Root
        /// </summary>
        /// <returns>系統參數</returns>
        public SystemParamVO GetSystemParamByRoot()
        {
            return SystemDao.GetSystemParamById(this.RootSystemParamId);
        }
    }
}

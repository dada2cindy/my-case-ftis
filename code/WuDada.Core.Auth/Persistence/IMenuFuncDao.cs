using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Auth.Domain;

namespace WuDada.Core.Auth.Persistence
{
    public interface IMenuFuncDao
    {
        /// <summary>
        /// 取得全部的後台角色清單
        /// </summary>
        /// <returns>全部的後台角色清單</returns>
        IList<MenuFuncVO> GetTopMenuFunc();

        /// <summary>
        /// 新增後台功能
        /// </summary>
        /// <param name="menuFuncVO">被新增的後台功能</param>
        /// <returns>新增後的後台功能</returns>
        MenuFuncVO CreateMenuFunc(MenuFuncVO menuFuncVO);

        /// <summary>
        /// 取出不為第一層的後台功能
        /// </summary>
        /// <returns>不為第一層的後台功能</returns>
        IList<MenuFuncVO> GetNotTopMenuFunc();

        /// <summary>
        /// 取出子後台功能清單 By 父層Id
        /// </summary>
        /// <param name="parentId">父層Id</param>
        /// <returns>子後台功能清單</returns>
        IList<MenuFuncVO> GetMenuFuncByParentId(int parentId);

        /// <summary>
        /// 取得後台功能 By 功能Id
        /// </summary>
        /// <param name="menuFuncId">功能Id</param>
        /// <returns>後台功能</returns>
        MenuFuncVO GetMenuFuncById(int menuFuncId);

        /// <summary>
        /// 取得後台功能清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        IList<MenuFuncVO> GetMenuFuncList(string queryString, int pageIndex, int pageSize);
    }
}

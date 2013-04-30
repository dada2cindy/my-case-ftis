using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NHibernate;

namespace WuDada.Core.Generic.Extension
{
    public static class QueryExtension    
    {
        /// <summary>
        /// 傳入資料筆數與hql欲置換的參數,回傳查詢結果
        /// </summary>
        /// <param name="query"></param>
        /// <param name="maxNumber"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList SetMaxResultsAndGetData(this IQuery query, int maxNumber, ArrayList param)
        {
            for (int i = 0; i < param.Count; i++)
            {
                query.SetParameter(i, param[i]);
            }

            return query.SetFirstResult(0).SetMaxResults(maxNumber).List();
        }

        /// <summary>
        /// 傳入條件,設定查詢取回的資料筆數
        /// </summary>
        /// <param name="query"></param>
        /// <param name="conditions"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IQuery SetMaxResults(this IQuery query, IDictionary<string, string> conditions, string key)
        {
            if (conditions.IsContainsValue(key))
            {
                query.SetMaxResults(Convert.ToInt32(conditions[key]));
            }
            return query;
        }

        /// <summary>
        /// 傳入條件,設定資料從第n筆開始取回
        /// </summary>
        /// <param name="query"></param>
        /// <param name="conditions"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IQuery SetFirstResult(this IQuery query, IDictionary<string, string> conditions, string key)
        {
            if (conditions.IsContainsValue(key))
            {
                query.SetFirstResult(Convert.ToInt32(conditions[key]));
            }
            return query;
        }

        /// <summary>
        /// Check if contains a key with not-empty value.
        /// </summary>
        /// <param name="conditions">The query conditions.</param>
        /// <param name="keyName">The key name.</param>
        /// <returns>True for contain.</returns>
        public static bool IsContainsValue(this IDictionary<string, string> conditions, string keyName)
        {
            return conditions.ContainsKey(keyName) && !string.IsNullOrEmpty(conditions[keyName]);
        }
    }
}

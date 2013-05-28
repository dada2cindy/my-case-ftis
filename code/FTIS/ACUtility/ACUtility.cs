using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTIS.Domain;
using FTIS.Domain.Impl;

namespace FTIS.ACUtility
{
    /// <summary>
    /// Access control utility
    /// </summary>
    public static class ACUtility
    {
        /// <summary>
        /// 根據目前登入者檢查是否有權限存取功能
        /// </summary>
        /// <param name="appFunctionId"></param>
        /// <returns></returns>
        public static bool CheckAuthorization(MasterMember loginUser, int appFunctionId, int operations)
        {
            if (loginUser == null)
            {
                return false;
            }

            if (loginUser != null && operations == 0 && appFunctionId == 0)
            {
                return true;
            }

            bool returnValue = false;

            //// 判斷使用者是否具有可以存取功能的角色
            var roles = from userRole in loginUser.AdminRoles
                        where userRole.AdminBar.AdminBarId == appFunctionId
                        && (userRole.AdminValue & operations) == operations
                        select userRole;

            if (roles.Count() > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// 根據目前登入者檢查是否有權限存取功能
        /// </summary>
        /// <param name="appFunctionId"></param>
        /// <returns></returns>
        public static bool CheckAuthorization(MasterMember loginUser, SiteEntities appFunction, SiteOperations operation)
        {
            return CheckAuthorization(loginUser, (int)appFunction, (int)operation);
        }

        /// <summary>
        /// 根據目前登入者檢查是否有權限存取功能
        /// </summary>
        /// <param name="appFunctionId"></param>
        /// <returns></returns>
        public static bool CheckAuthorization(MasterMember loginUser, SiteEntities appFunction)
        {
            int appFunctionId = (int)appFunction;

            if (loginUser == null)
            {
                return false;
            }

            if (loginUser != null && appFunctionId == 0)
            {
                return true;
            }

            bool returnValue = false;

            //// 判斷使用者是否具有可以存取功能的角色，只要AdminValue大於0表示有此Menu
            var roles = from userRole in loginUser.AdminRoles
                        where userRole.AdminBar.AdminBarId == appFunctionId
                        && userRole.AdminValue > 0
                        select userRole;

            if (roles.Count() > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }
    }
}

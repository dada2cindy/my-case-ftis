using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WuDada.Core.Common.Domain
{
    public interface IBaseDateTimeVO
    {
        /// <summary>
        /// 建立時間
        /// </summary>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新時間
        /// </summary>
        DateTime UpdateTime { get; set; }
    }
}

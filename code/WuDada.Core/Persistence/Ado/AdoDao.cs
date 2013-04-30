using System;
using System.Data;
using System.Text;
using WuDada.Core.Generic.Util;
using Spring.Data.Core;

namespace WuDada.Core.Persistence.Ado
{
    public class AdoDao : AdoDaoSupport
    {
        public int ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            return AdoTemplate.ExecuteNonQuery(cmdType, cmdText);
        }

        public object ExecuteScalar(CommandType cmdType, string cmdText)
        {
            return AdoTemplate.ExecuteScalar(cmdType, cmdText);
        }

        public int ExecuteScalarInt(CommandType cmdType, string cmdText)
        {
            object result = ExecuteScalar(cmdType, cmdText);

            if (result == null || String.IsNullOrEmpty(result.ToString()))
            {
                return 0;
            }
            else
            {
                return ConvertUtil.ToInt32(result.ToString());
            }
        }

        public DataTable GetDataTable(CommandType cmdType, string cmdText)
        {
            DataTable dt = new DataTable();

            dt = AdoTemplate.DataTableCreate(cmdType, cmdText);

            return dt;
        }

        public DataTable GetDataTable(CommandType cmdType, string cmdText, string orderBy, int pageIndex, int pageSize)
        {
            int startIndex = ConvertUtil.GetStartIndex(pageIndex, pageSize) + 1;
            int endIndex = ConvertUtil.GetEndIndex(pageIndex, pageSize);

            DataTable dt = new DataTable();

            int selectIndex = cmdText.IndexOf("SELECT", StringComparison.CurrentCultureIgnoreCase);
            if (selectIndex > -1 && selectIndex <= 6)
            {
                cmdText = cmdText.Substring(selectIndex + "SELECT".Length);
            }

            StringBuilder cmd = new StringBuilder();
            cmd.Append(" SELECT * FROM( ");
            cmd.Append(" SELECT ROW_NUMBER() OVER(ORDER BY {3}) AS RowNumber, ");
            cmd.Append(" {2} ");
            cmd.Append(" ) as Result WHERE Result.RowNumber Between {0} AND {1}");

            dt = GetDataTable(cmdType, string.Format(cmd.ToString(), startIndex, endIndex, cmdText, orderBy));

            return dt;
        }
    }
}

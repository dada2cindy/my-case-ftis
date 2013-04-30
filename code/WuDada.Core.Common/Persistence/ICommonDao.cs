using WuDada.Core.Common.Domain;

namespace WuDada.Core.Common.Persistence
{
    public interface ICommonDao
    {
        /// <summary>
        /// 新增序號紀錄
        /// </summary>
        /// <param name="serialVO">被新增的序號紀錄</param>
        /// <returns>新增後的序號紀錄</returns>
        SerialVO CreateSerial(SerialVO serialVO);

        /// <summary>
        /// 更新序號紀錄
        /// </summary>
        /// <param name="serialVO">被更新的序號紀錄</param>
        /// <returns>更新後的序號紀錄</returns>
        SerialVO UpdateSerial(SerialVO serialVO);

        /// <summary>
        /// 取得序號紀錄
        /// </summary>
        /// <param name="itemParamId">序號代碼</param>
        /// <returns>序號紀錄</returns>
        SerialVO GetSerialById(string serialId);
    }
}

using WuDada.Core.Common.Domain;
using WuDada.Core.Common.Persistence;
using WuDada.Core.Persistence;
using WuDada.Core.Persistence.Ado;

namespace WuDada.Core.Common.Persistence
{
    public class CommonDao : AdoDao, ICommonDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        /// <summary>
        /// 新增序號紀錄
        /// </summary>
        /// <param name="serialVO">被新增的序號紀錄</param>
        /// <returns>新增後的序號紀錄</returns>
        public SerialVO CreateSerial(SerialVO serialVO)
        {
            NHibernateDao.Insert(serialVO);

            return serialVO;
        }

        /// <summary>
        /// 更新序號紀錄
        /// </summary>
        /// <param name="serialVO">被更新的序號紀錄</param>
        /// <returns>更新後的序號紀錄</returns>
        public SerialVO UpdateSerial(SerialVO serialVO)
        {
            NHibernateDao.Update(serialVO);

            return serialVO;
        }

        /// <summary>
        /// 取得序號紀錄
        /// </summary>
        /// <param name="itemParamId">序號代碼</param>
        /// <returns>序號紀錄</returns>
        public SerialVO GetSerialById(string serialId)
        {
            return NHibernateDao.GetVOById<SerialVO>(serialId);
        }      
    }
}

using System;
using WuDada.Core.Common.Domain;
using WuDada.Core.Common.Persistence;

namespace WuDada.Core.Common.Service.Impl
{
    public class CommonService : ICommonService
    {
        public ICommonDao CommonDao { get; set; }

        /// <summary>
        /// 取得編號 (代碼+數字)
        /// </summary>
        /// <param name="code">代碼</param>
        /// <param name="numLength">數字長度</param>
        /// <returns>編號</returns>
        public string CreateSer_Code_And_Num(string code, int numLength)
        {
            lock (this)
            {
                string result = "";
                string codeSearch = code;

                SerialVO serialVO = CommonDao.GetSerialById(codeSearch);

                int count = 1;
                if (serialVO != null)
                {
                    count += serialVO.Count;
                    serialVO.Count = count;
                    serialVO.Date = DateTime.Now;
                    CommonDao.UpdateSerial(serialVO);
                }
                else
                {
                    serialVO = new SerialVO();
                    serialVO.SerialId = codeSearch;
                    serialVO.Count = count;
                    serialVO.Date = DateTime.Now;
                    CommonDao.CreateSerial(serialVO);
                }

                result = string.Format("{0}{1}", code, count.ToString().PadLeft(numLength, '0'));
                return result;
            }
        }        
    }
}

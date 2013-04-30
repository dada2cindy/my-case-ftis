

namespace WuDada.Core.Common.Service
{
    public interface ICommonService
    {
        /// <summary>
        /// 取得編號 (代碼+數字)
        /// </summary>
        /// <param name="code">代碼</param>
        /// <param name="numLength">數字長度</param>
        /// <returns>編號</returns>
        string CreateSer_Code_And_Num(string code, int numLength);
    }
}

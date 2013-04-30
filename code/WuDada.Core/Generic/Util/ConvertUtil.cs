using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using Common.Logging;
using Microsoft.VisualBasic;
using Spring.Collections.Generic;
using Spring.Core;
using Spring.Objects;

namespace WuDada.Core.Generic.Util
{
    public class ConvertUtil
    {
        /// <summary>
        /// 轉換字串yyyy/mm/dd日期至日期格式，若字串為空則傳回null
        /// </summary>
        /// <param name="dateStr">日期的字串型態</param>
        /// <returns>回傳日期</returns>
        public static DateTime? ConvertStringToDate(string dateStr)
        {
            if (string.IsNullOrEmpty(dateStr))
            {
                return null;
            }
            else
            {
                return ToDateTime(dateStr);
            }
        }

        /// <summary>
        /// 複製資料夾
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        public static void CopyDirectory(string sourceDirName, string destDirName)
        {
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
                File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
            }

            if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                destDirName = destDirName + Path.DirectorySeparatorChar;

            string[] files = Directory.GetFiles(sourceDirName);
            foreach (string file in files)
            {
                File.Copy(file, destDirName + Path.GetFileName(file), true);
                File.SetAttributes(destDirName + Path.GetFileName(file), FileAttributes.Normal);
                //total++;
                //if (FileNumber == 0)
                //{
                //    lblStatus.Text = "已完成   100% ";
                //}
                //else
                //{
                //    lblStatus.Text = "已完成   " + (Math.Round((double)(100 * total / FileNumber), 0)).ToString() + "% ";
                //}
            }
            string[] dirs = Directory.GetDirectories(sourceDirName);
            foreach (string dir in dirs)
            {
                CopyDirectory(dir, destDirName + Path.GetFileName(dir));
            }
        }



        public static int ConvertDecimalToInt(Decimal d)
        {
            int a = (int)d;

            return a;
        }

        public static string ConvertDecimalToIntStr(Decimal d)
        {
            int a = (int)d;

            return a.ToString();
        }


        /// <summary>
        /// 將字串轉為數字，若null則傳回0
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int ConvertStringToInt(string p)
        {
            return (string.IsNullOrEmpty(p) ? 0 : int.Parse(p));
        }


        public static bool IsNumber(string strNumber)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^-?\d+(\.)?\d*$");
            return r.IsMatch(strNumber);
        }


        /// <summary>
        /// 將日期轉為yyyy/mm/dd的格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ConvertDateToStr(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }
            else
            {
                return (ConvertUtil.DateToShortDateStr(date));
            }
        }

        /// <summary>
        /// 將日期轉為yyyy/mm/dd hh:mm:ss的格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ConvertDateToAllStr(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }
            else
            {
                return (DateToShortDateStr(date.Value) + " " + DateToShortTimeStr(date.Value));
            }
        }

        /// <summary>
        /// 將日期轉為yyyy/mm/dd的字串
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateToShortDateStr(DateTime? date)
        {
            return DateToShortDateStr(date, "/");
        }

        /// <summary>
        /// 將日期轉為yyyy/mm/dd的字串
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateToShortDateStr(DateTime? date, string separator)
        {
            if (date == null)
            {
                return "";
            }
            else
            {
                return (string.Format(date.Value.ToString("yyyy{0}MM{1}dd"), separator, separator));
            }
        }

        /// <summary>
        /// 將日期轉為hh/mm/ss的時間
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateToShortTimeStr(DateTime? date)
        {
            if (date == null || date.Value == null)
            {
                return "";
            }
            else
            {
                return (date.Value.ToString("HH:mm:ss"));
            }
        }


        public static object CloneObject(object obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter(null,
            new StreamingContext(StreamingContextStates.Clone));

            bf.Serialize(ms, obj);
            ms.Position = 0;
            Object res = bf.Deserialize(ms);
            ms.Close();
            return res;
        }


        /// <summary>
        /// 無條件捨去取整個
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int Floor(double p)
        {
            return ((int)Math.Floor(Convert.ToDouble(p.ToString())));
        }


        /// <summary>
        /// 將yyyy/mm/dd轉換為Datetime格式
        /// </summary>
        /// <param name="dateTimeStr"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string dateTimeStr)
        {
            return ToDateTime(dateTimeStr, '/');
        }

        /// <summary>
        /// 將yyyy/mm/dd轉換為Datetime格式
        /// </summary>
        /// <param name="dateTimeStr"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string dateTimeStr, char separator)
        {
            string[] strArray = dateTimeStr.Split(separator);

            DateTime d = new DateTime(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), Convert.ToInt32(strArray[2]));
            return d;
        }


        /// <summary>
        /// 轉換成完整的日期字串 yyyy/mm/dd hh:MM:ss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToShortDateTimeStr(DateTime? date)
        {
            if (date.HasValue)
            {
                DateTime tmp = date.Value;
                return DateToShortDateStr(tmp) + " " + DateToShortTimeStr(tmp);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 傳回當天日期最後的時間例如1月1日23:59:59
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeMax(DateTime dateTime)
        {
            DateTime tmp = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
            return tmp;
        }

        /// <summary>
        /// 傳回當天日期最早的時間例如1月1日00:00:00
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeMin(DateTime dateTime)
        {
            DateTime tmp = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
            return tmp;
        }



        /// <summary>
        /// 轉換為整數
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int ToInt32(string p)
        {
            if (!string.IsNullOrEmpty(p) && IsNumber(p))
            {
                return Convert.ToInt32(p);
            }
            else
            {
                return 0;
            }
        }

        public static double ToDouble(string p)
        {
            if (!string.IsNullOrEmpty(p) && IsNumber(p))
            {
                return Convert.ToDouble(p);
            }
            else
            {
                return 0;
            }
        }

        public static string ToIntNumberStr(decimal p)
        {
            return (ToIntNumberStr(Convert.ToInt32(p)));
        }

        /// <summary>
        /// 四捨五入取至整數
        /// </summary>
        /// <param name="noTaxNum"></param>
        /// <returns></returns>
        public static int ToRoundInt(double num)
        {

            return (Convert.ToInt32(Math.Round(num, 0, MidpointRounding.AwayFromZero)));
        }


        /// <summary>
        /// 四捨五入取至整數然後以#,##0格式呈現
        /// </summary>
        /// <param name="noTaxNum"></param>
        /// <returns></returns>
        public static string ToRoundIntStr(double num)
        {
            return ToRoundInt(num).ToString("#,##0");
        }


        /// <summary>
        /// 將null轉換為空字串
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string ConvertNull(string p)
        {
            if (p == null)
            {
                return "";
            }
            else
            {
                return p;
            }
        }

        public static T[] SetToArray<T>(Set<T> set)
        {
            return (set.ToArray<T>());
        }

        /// <summary>
        /// 將來源物件的所有欄位值複製一份至目標物件
        /// </summary>
        /// <param name="sourceObj">來源物件</param>
        /// <param name="targetObj">目標物件</param>
        public static void CopyFieldValue(Object sourceObj, Object targetObj)
        {
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            if (sourceObj == null || targetObj == null)
            {
                return;
            }

            ObjectWrapper owSource = new ObjectWrapper(sourceObj);
            ObjectWrapper owTarget = new ObjectWrapper(targetObj);

            foreach (PropertyInfo proInfo in sourceObj.GetType().GetProperties())
            {
                string fieldName = proInfo.GetGetMethod().Name.Replace("get_", "");
                if (HasProperty(owTarget, fieldName))
                {
                    owTarget.SetPropertyValue(fieldName, owSource.GetPropertyValue(fieldName));
                }
            }
        }


        /// <summary>
        /// 轉全形
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToHolomorphic(string str)
        {
            return Strings.StrConv(str, VbStrConv.Wide, 0);
        }

        /// <summary>
        /// 轉半形
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToHalf(string str)
        {
            return Strings.StrConv(str, VbStrConv.Narrow, 0);
        }

        /// <summary>
        /// 取得附檔名
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GetExtName(string p)
        {
            if (!string.IsNullOrEmpty(p))
            {
                int index = p.LastIndexOf(".");
                if (index != -1)
                {
                    return (p.Substring(index, p.Length - index));
                }
            }
            return "";
        }

        #region private method
        private static bool HasProperty(ObjectWrapper ow, string fieldName)
        {
            try
            {
                object value = ow.GetPropertyValue(fieldName);
                return true;
            }
            catch (InvalidPropertyException)
            {
                return false;
            }
        }
        #endregion



        /// <summary>
        /// 將物件toString
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string ConvertNULL(object p)
        {
            if (p == null)
            {
                return "";
            }
            else
            {
                return p.ToString().Trim();
            }
        }

        /// <summary>
        /// 取出含html語法中只有文字的部份
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string FilterHtml(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                //html = Regex.Replace(p, "<[^>]+>", "");
                //html = html.Replace(";&nbsp", "");
                //html = html.Replace("&nbsp;", "");
                //html = html.Replace("&amp;", "");
                //html = html.Replace("nbsp;", "");
                //return p;
                System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex10 = new System.Text.RegularExpressions.Regex(@"<style[\s\S]+</style *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                html = regex10.Replace(html, ""); //过滤<style></style>标记 
                html = regex1.Replace(html, ""); //过滤<script></script>标记 
                html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性 
                html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
                html = regex4.Replace(html, ""); //过滤iframe 
                html = regex5.Replace(html, ""); //过滤frameset 
                html = regex6.Replace(html, ""); //过滤img 
                html = regex7.Replace(html, ""); //过滤p 
                html = regex8.Replace(html, ""); //过滤p 
                html = regex9.Replace(html, "");                
                html = html.Replace(" ", "");
                html = html.Replace("</strong>", "");
                html = html.Replace("<strong>", "");
                return html;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 轉換星期成數字
        /// </summary>
        /// <param name="dateStart"></param>
        /// <returns></returns>
        public static int DateToDayOfWeek(DateTime dateStart)
        {
            int result = 0; ;

            switch (dateStart.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    result = 1;
                    break;
                case DayOfWeek.Tuesday:
                    result = 2;
                    break;
                case DayOfWeek.Wednesday:
                    result = 3;
                    break;
                case DayOfWeek.Thursday:
                    result = 4;
                    break;
                case DayOfWeek.Friday:
                    result = 5;
                    break;
                case DayOfWeek.Saturday:
                    result = 6;
                    break;
                case DayOfWeek.Sunday:
                    result = 7;
                    break;
            }

            return result;
        }

        /// <summary>
        /// 傳回當月份最小的日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeMonthMin(DateTime dateTime)
        {
            DateTime tmp = new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0);
            return tmp;
        }

        /// <summary>
        /// 傳回當月份最大的日期
        /// </summary>
        /// <param name="dateStart"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeMonthMax(DateTime dateTime)
        {
            //取當月最後一天
            DateTime tmp = ToDateTimeMonthMin(dateTime).AddMonths(1).AddDays(-1);
            tmp = ToDateTimeMax(tmp);
            return tmp;
        }

        /// <summary>
        /// 將Dictionary的Key轉成SqlIn裡面的語法 ex '123','abc'
        /// </summary>
        /// <param name="dicItem"></param>
        /// <returns></returns>
        public static string ConvetSqlInStr(Dictionary<string, string> dic)
        {
            string result = "''";

            if (dic != null & dic.Count > 0)
            {
                result = "";
                foreach (KeyValuePair<string, string> item in dic)
                {
                    if (!string.IsNullOrEmpty(item.Key))
                    {
                        result += string.Format("'{0}',", item.Key);
                    }
                }

                if (!string.IsNullOrEmpty(result)) //移除最後一個,
                {
                    result = result.Remove(result.LastIndexOf(','));
                }
            }

            return result;
        }

        /// <summary>
        /// 將String[]轉成SqlIn裡面的語法 ex '123','abc'
        /// </summary>
        /// <param name="dicItem"></param>
        /// <returns></returns>
        public static string ConvetSqlInStr(String[] dic)
        {
            string result = "''";

            if (dic != null & dic.Length > 0)
            {
                result = "";
                foreach (String item in dic)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        result += string.Format("'{0}',", item);
                    }
                }

                if (!string.IsNullOrEmpty(result)) //移除最後一個,
                {
                    result = result.Remove(result.LastIndexOf(','));
                }
            }

            return result;
        }

        /// <summary>
        /// 產生sqlIN語法 (假如sqlIn 為'' 沒有任何可比對目標，則回傳空白sql，此條件不比對)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlIn"></param>
        /// <returns></returns>
        public static string Gen_SqlIn(string sql, string sqlIn)
        {
            string result = "";

            //

            if (!"''".Equals(sqlIn))
            {
                result = string.Format(sql, sqlIn);
            }

            return result;
        }

        public static long ToInt64(string p)
        {
            if (!string.IsNullOrEmpty(p) && IsNumber(p))
            {
                return Convert.ToInt64(p);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 轉換為字串數字格式(#,##0)
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToIntNumberStr(double num)
        {
            NumberFormatInfo myNfi = new NumberFormatInfo();
            myNfi.NumberNegativePattern = 1;
            myNfi.NumberDecimalDigits = 0;

            return num.ToString("N", myNfi);
            //return num.ToString("#,##0");
        }

        /// <summary>
        /// 轉換為字串數字格式(#,##0)
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToIntNumberStr(int num)
        {
            NumberFormatInfo myNfi = new NumberFormatInfo();
            myNfi.NumberNegativePattern = 1;
            myNfi.NumberDecimalDigits = 0;

            return num.ToString("N", myNfi);
            //return num.ToString("#,##0");
        }

        /// <summary>
        /// 取得資料起始索引
        /// </summary>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <returns>資料起始索引</returns>
        public static int GetStartIndex(int pageIndex, int pageSize)
        {
            return pageIndex * pageSize;
        }

        /// <summary>
        /// 取得資料結束索引
        /// </summary>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <returns>資料結束索引</returns>
        public static int GetEndIndex(int pageIndex, int pageSize)
        {
            return (pageIndex * pageSize) + pageSize;
        }
    }
}

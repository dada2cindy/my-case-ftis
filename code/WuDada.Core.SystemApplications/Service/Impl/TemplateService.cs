using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.SystemApplications.Persistence;
using WuDada.Core.SystemApplications.Domain;
using System.Globalization;

namespace WuDada.Core.SystemApplications.Service.Impl
{
    public class TemplateService : ITemplateService
    {
        public ITemplateDao TemplateDao { get; set; }

        /// <summary>
        /// 新增樣板
        /// </summary>
        /// <param name="templateVO">被新增的樣板</param>
        /// <returns>新增後的樣板</returns>
        public TemplateVO CreateTemplate(TemplateVO templateVO)
        {
            return TemplateDao.CreateTemplate(templateVO);
        }

        /// <summary>
        /// 更新樣板
        /// </summary>
        /// <param name="templateVO">被更新的樣板</param>
        /// <returns>更新後的樣板</returns>
        public TemplateVO UpdateTemplate(TemplateVO templateVO)
        {
            return TemplateDao.UpdateTemplate(templateVO);
        }

        /// <summary>
        /// 取得樣板 By 識別碼
        /// </summary>
        /// <param name="templateId">識別碼</param>
        /// <returns>樣板</returns>
        public TemplateVO GetTemplateById(int templateId)
        {
            return TemplateDao.GetTemplateById(templateId);
        }

        /// <summary>
        /// 取得樣板清單
        /// </summary>
        /// <param name="type">樣板類別</param>
        /// <returns>樣板清單</returns>
        public IList<TemplateVO> GetTemplateList(TemplateVO.Type type)
        {
            return TemplateDao.GetTemplateList(type);
        }

        /// <summary>
        /// 取得目前要使用的樣板
        /// </summary>
        /// <param name="beforeDay">前幾天(節慶)</param>
        /// <returns>目前要使用的樣板</returns>
        public TemplateVO GetCurrentTemplate(int beforeDay)
        {
            //先抓節日的 (要存農曆日還是西元要再確認，存西元必須每年度去調整)
            IList<TemplateVO> festivalTemplates = SortTemplateByDate(GetTemplateList(TemplateVO.Type.Festival), beforeDay);
            if (festivalTemplates != null && festivalTemplates.Count > 0)
            {
                foreach (TemplateVO templateVO in festivalTemplates)
                {
                    if (templateVO.Name.Equals("聖誕節") || templateVO.Name.Equals("西洋情人節"))
                    {
                        //此兩個節日用西元算
                        //DateTime today = DateTime.Today;
                        //int year = DateTime.Today.Year;
                        //int month = int.Parse(templateVO.StartDate.Substring(0, 2));
                        //int day = int.Parse(templateVO.StartDate.Substring(2, 2));
                        //DateTime endDate = new DateTime(DateTime.Today.Year, month, day);
                        //DateTime startDate = endDate.AddDays(((-1) * beforeDay));

                        //if (today >= startDate && today <= endDate)
                        //{
                        //    return templateVO;
                        //}

                        DateTime today = DateTime.Today;
                        if (today >= templateVO.BeginDate && today <= templateVO.LastDate)
                        {
                            return templateVO;
                        }
                    }
                    else
                    {
                        //此其他的節日用農曆算

                        //先從農曆轉成西元
                        //TaiwanLunisolarCalendar tlc = new TaiwanLunisolarCalendar();
                        //DateTime today = DateTime.Today;
                        //int year = tlc.GetYear(today);
                        //int month = int.Parse(templateVO.StartDate.Substring(0, 2));
                        //int day = int.Parse(templateVO.StartDate.Substring(2, 2));
                        //int era = tlc.GetEra(today);                        

                        ////判斷今年是否為閏年，閏年的話抓出正確的日期
                        //if (tlc.IsLeapYear(year, era))
                        //{
                        //    //閏月月份 
                        //    int leapMonth = tlc.GetLeapMonth(year, era);
                        //    //閏月天數
                        //    //int monthDays = tlc.GetDaysInMonth(year, leapMonth, era);

                        //    //閏月有13個月，例如leapMonth=5，代表閏四月，則農曆5月開始要多加1個月
                        //    if (month >= leapMonth)
                        //    {
                        //        month += 1;
                        //    }
                        //}

                        //DateTime endDate = tlc.ToDateTime(year, month, day, 0, 0, 0, 0, era);

                        //DateTime startDate = endDate.AddDays(((-1) * beforeDay));

                        //if (today >= startDate && today <= endDate)
                        //{
                        //    return templateVO;
                        //}

                        DateTime today = DateTime.Today;
                        if (today >= templateVO.BeginDate && today <= templateVO.LastDate)
                        {
                            return templateVO;
                        }
                    }
                }
            }

            //季節
            IList<TemplateVO> seasonTemplates = GetTemplateList(TemplateVO.Type.Season);
            if (seasonTemplates != null && seasonTemplates.Count > 0)
            {
                //DateTime date = DateTime.Today.AddDays(beforeDay);
                DateTime date = DateTime.Today;
                string showDate = string.Format("{0}{1}", date.Month.ToString().PadLeft(2, '0'), date.Day.ToString().PadLeft(2, '0'));
                int count = 0;
                foreach (TemplateVO templateVO in seasonTemplates)
                {
                    count += 1;
                    if (count < seasonTemplates.Count)
                    {
                        if (int.Parse(showDate) >= int.Parse(templateVO.StartDate) && int.Parse(showDate) <= int.Parse(templateVO.EndDate))
                        {
                            return templateVO;
                        }
                    }
                    else
                    {
                        //最後一季的算法不一樣，可能有跨年(起始日期>結束時 則表示有跨年)
                        if (int.Parse(templateVO.StartDate) > int.Parse(templateVO.EndDate))
                        {
                            if (int.Parse(showDate) >= int.Parse(templateVO.StartDate) && int.Parse(showDate) <= int.Parse("1231"))
                            {
                                return templateVO;
                            }
                            if (int.Parse(showDate) >= int.Parse("0101") && int.Parse(showDate) <= int.Parse(templateVO.EndDate))
                            {
                                return templateVO;
                            }
                        }
                        else
                        {
                            if (int.Parse(showDate) >= int.Parse(templateVO.StartDate) && int.Parse(showDate) <= int.Parse(templateVO.EndDate))
                            {
                                return templateVO;
                            }
                        }
                    }
                }
            }

            //都沒有的話回傳季節的第一個樣板
            return seasonTemplates[0];
        }

        /// <summary>
        /// 排序樣板 By 日期順序，先來的要先排序在前面被檢測
        /// </summary>
        /// <param name="festivalTemplates"></param>
        /// <param name="beforeDay">前幾天(節慶)</param>
        /// <returns></returns>
        private IList<TemplateVO> SortTemplateByDate(IList<TemplateVO> festivalTemplates, int beforeDay)
        {
            IList<TemplateVO> result = new List<TemplateVO>();

            if (festivalTemplates != null && festivalTemplates.Count > 0)
            {
                foreach (TemplateVO templateVO in festivalTemplates)
                {
                    if (templateVO.Name.Equals("聖誕節") || templateVO.Name.Equals("西洋情人節"))
                    {
                        //此兩個節日用西元算
                        DateTime today = DateTime.Today;
                        int year = DateTime.Today.Year;
                        int month = int.Parse(templateVO.StartDate.Substring(0, 2));
                        int day = int.Parse(templateVO.StartDate.Substring(2, 2));
                        DateTime endDate = new DateTime(DateTime.Today.Year, month, day);
                        DateTime startDate = endDate.AddDays(((-1) * beforeDay));
                        templateVO.BeginDate = startDate;
                        templateVO.LastDate = endDate;
                    }
                    else
                    {
                        //此其他的節日用農曆算

                        //先從農曆轉成西元
                        TaiwanLunisolarCalendar tlc = new TaiwanLunisolarCalendar();
                        DateTime today = DateTime.Today;
                        int year = tlc.GetYear(today);
                        int month = int.Parse(templateVO.StartDate.Substring(0, 2));
                        int day = int.Parse(templateVO.StartDate.Substring(2, 2));
                        int era = tlc.GetEra(today);
                        //春節的話因為還沒到這個年，所以要加1個月
                        if (templateVO.Name.Equals("春節"))
                        {
                            year = tlc.GetYear(DateTime.Today.AddMonths(1));
                            era = tlc.GetEra(DateTime.Today.AddMonths(1));
                        }

                        //判斷今年是否為閏年，閏年的話抓出正確的日期
                        if (tlc.IsLeapYear(year, era))
                        {
                            //閏月月份 
                            int leapMonth = tlc.GetLeapMonth(year, era);
                            //閏月天數
                            //int monthDays = tlc.GetDaysInMonth(year, leapMonth, era);

                            //閏月有13個月，例如leapMonth=5，代表閏四月，則農曆5月開始要多加1個月
                            if (month >= leapMonth)
                            {
                                month += 1;
                            }
                        }

                        DateTime endDate = tlc.ToDateTime(year, month, day, 0, 0, 0, 0, era);

                        DateTime startDate = endDate.AddDays(((-1) * beforeDay));

                        templateVO.BeginDate = startDate;
                        templateVO.LastDate = endDate;
                    }
                }

                result = festivalTemplates.OrderBy(p => p.BeginDate).ToList();
            }

            return result;
        }
    }
}

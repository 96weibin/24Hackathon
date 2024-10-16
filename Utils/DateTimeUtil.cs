using System;

namespace knowledgeBase.Utils
{
    public class DateTimeUtil
    {
        public static string FormatShortDate(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        public static DateTime Today()
        {
            return DateTime.Parse(DateTimeUtil.FormatShortDate(DateTime.Now));
        }

        public static bool IsToday(DateTime dt)
        {
            return dt.Equals(Today());
        }

        public static DateTime GetMonday(DateTime dt)
        {
            int dayOfWeek = (int)dt.DayOfWeek; //0: Sunday
            int diff = dayOfWeek == 0 ? -6 : 1 - dayOfWeek;
            return dt.AddDays(diff);
        }

        public static int GetMonthDays(DateTime dt)
        {
            int dayOfMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
            return dayOfMonth;
        }

        public static bool IsThisWeek(DateTime dt)
        {
            return GetMonday(Today()) == GetMonday(dt);
        }

    }
}
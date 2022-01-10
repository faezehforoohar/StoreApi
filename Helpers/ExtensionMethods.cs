using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace StoreApi.Helpers
{
    public static class ExtensionMethods
    {
        public static string ToDescription(this Enum en)
        {
            if (en == null)
                return "null";
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(
                                              typeof(DescriptionAttribute),
                                              false);

                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }
        public static long ToLong(this DateTime p)
        {
            long dt_long = 0;

            //Year
            dt_long = p.Year;
            //Month
            dt_long = dt_long * 100 + p.Month;
            //Day
            dt_long = dt_long * 100 + p.Day;
            //Hour
            dt_long = dt_long * 100 + p.Hour;
            //Minute
            dt_long = dt_long * 100 + p.Minute;
            //Second
            dt_long = dt_long * 100 + p.Second;
            //MiliSecond
            dt_long = dt_long * 1000 + p.Millisecond;

            return dt_long;
        }
        public static byte ToID(this Enum en)
        {
            return (byte)en.GetHashCode();
        }
        public static double GetDiffFromNow(this DateTime? dt)
        {
            if (!dt.HasValue)
                return 0;
            TimeSpan ts = DateTime.Now - dt.Value;

            return ts.TotalMinutes;
        }
        public static string ToPersianDate(this DateTime dt)
        {
            PersianCalendar calender = new PersianCalendar();
            string date = string.Empty;

            if (dt.Year == 1)
                return ("");

            date += calender.GetYear(dt) + "/";
            if (calender.GetMonth(dt) > 9)
                date += calender.GetMonth(dt) + "/";
            else
                date += "0" + calender.GetMonth(dt) + "/";
            if (calender.GetDayOfMonth(dt) > 9)
                date += calender.GetDayOfMonth(dt);
            else
                date += "0" + calender.GetDayOfMonth(dt);
            return date;
        }
        public static DateTime ToDateTime(this string dt)
        {
            PersianCalendar calender = new PersianCalendar();

            if (string.IsNullOrEmpty(dt))
                return new DateTime();
            var strArr = dt.Split('/');
            if(strArr.Length!=3)
                return new DateTime();
            int year = int.Parse(strArr[0]);
            int month = int.Parse(strArr[1]);
            int day = int.Parse(strArr[2]);

            return calender.ToDateTime(year, month, day, 0, 0, 0, 0);

        }
    }
}

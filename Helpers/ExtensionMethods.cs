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
        public static bool CheckDate(this string PersianDate, ref string error)
        {
            int year, month, day;

            try
            {
                PersianDate = PersianDate.Replace(" ", "");
                year = int.Parse(PersianDate.Substring(0, 4));
                month = int.Parse(PersianDate.Substring(5, 2));
                day = int.Parse(PersianDate.Substring(8, 2));
            }
            catch
            {
                error = "فرمت تاریخ اشتباه است";
                return false;
            }
            if (month > 12 || month < 01)
            {
                error = "ماه اشتباه است";
                return false;
            }
            if (day > 31 || day < 01)
            {
                error = " روز اشتباه است";
                return false;
            }
            System.Globalization.PersianCalendar PCalendar = new System.Globalization.PersianCalendar();

            if (!PCalendar.IsLeapYear(year))
            {
                if (month == 12 && day >= 30)
                {
                    error = " روز اشتباه است";
                    return false;
                }
            }
            ///

            ///Year is  Leap.

            ///
            if (PCalendar.IsLeapYear(year))
            {
                if (month == 12 && day > 30)
                {
                    error = " روز اشتباه است";
                    return false;
                }
            }
            if (((month >= 1 && month <= 6 && day >= 1 && day <= 31) ||

                (month >= 7 && month <= 12 && day >= 1 && day <= 30)) == false)
            {

                error = " تعداد روز اشتباه است";
                return false;
            }
            return true;
        }
    }
}

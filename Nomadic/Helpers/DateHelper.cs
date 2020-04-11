using System;
using System.Collections.Generic;
using System.Linq;

namespace DateHelper
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Enums for different date formats
        /// </summary>
        public enum Format
        {
            DayDateMonthHour,
            Day,
            DayHour,
            Month,
            Year,
            DayMonth,
            Time,
            Time24Hour,
            FullDate,
            FullDateGMT,
            MonthYear,
            DayDateMonth,
            DayMonthYearTime
        }

        static readonly string DayDateMonthHour = "ddd, dd MMM h:mm tt";
        static readonly string Day = "ddd";
        static readonly string DayHour = "ddd, hh:mm tt";
        static readonly string Year = "yyyy";
        static readonly string FullDate = "dddd, dd MMMM yyyy";
        static readonly string DayMonth = "dd MMMM";
        static readonly string Time = "hh:mm tt";
        static readonly string Time24Hour = "HH:mm";
        static readonly string FullDateGMT = "ddd, dd MMM yyy HH':'mm':'ss 'GMT'";
        static readonly string MonthYear = "MMM yyyy";
        static readonly string DayDateMonth = "ddd, dd MMM";
        static readonly string DayMonthYearTime = "dd, MMM yyyy hh:mm tt";

        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Helper function to return current date in long
        /// </summary>
        /// <returns>Current Date in long</returns>
        public static long ReturnCurrentTimeInLong()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// Takes a date in long, adds days and return the new 
        /// date in long
        /// </summary>
        /// <param name="date">date in long</param>
        /// <param name="days">days in int</param>
        /// <returns>Return Added Days in long</returns>
        public static long ReturnAddedDaysInLongFromLong(long date, int days)
        {
            DateTimeOffset oDate = DateTimeOffset.FromUnixTimeMilliseconds(date);
            return oDate.AddDays(days).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// Takes in a Date string and returns the date as a long
        /// </summary>
        /// <param name="date">Takes in Date as a string</param>
        /// <returns>Returns Date string as long</returns>
        public static long ReturnDateLongFromString(string date)
        {
            DateTime oDate = Convert.ToDateTime(date);
            return ((DateTimeOffset)oDate).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// Takes in DateTime and returns the date as a long
        /// </summary>
        /// <param name="date">Takes in a DateTime</param>
        /// <returns>Returns DatetTime as a long</returns>
        public static long ReturnLongFromDateTime(DateTime date)
        {
            return (long)(date - UnixEpoch).TotalMilliseconds;
        }

        /// <summary>
        /// Takes in a Date as DateTime and returns the date as a long
        /// </summary>
        /// <param name="date">Takes in a long</param>
        /// <returns>Returns DateTime from a long</returns>
        public static DateTime ReturnDateTimeFromlong(long date)
        {
            return UnixEpoch.AddMilliseconds(date);
        }

        /// <summary>
        /// Takes in TimesSpan to be added, adds the TimeSpan and returns 
        /// the new date as a long
        /// </summary>
        /// <param name="time">Takes in TimeSpan</param>
        /// <param name="date">Takes in a long</param>
        /// <returns>Adds TimeSpan to date in long and returns long</returns>
        public static long ReturnLongAfterAddingTimeSpan(TimeSpan time, long date)
        {
            var datetime = ReturnDateTimeFromlong(date);
            var newdatetime = datetime.Add(time);
            return ReturnLongFromDateTime(newdatetime);
        }

        /// <summary>
        /// Takes in enum Format, and returns a Date string formatted
        /// as the enum Format intended
        /// </summary>
        /// <param name="format">Takes in enum Format</param>
        /// <param name="date">Takes in a Date as long</param>
        /// <returns>Returns a formatted Date string</returns>
        public static string ReturnDateStringFromLong(Format format, long date)
        {
            DateTime datetime = UnixEpoch.AddMilliseconds(date).ToLocalTime();
            string dateString = "";

            switch (format)
            {
                case Format.DayDateMonthHour:
                    dateString = datetime.ToString(DayDateMonthHour);
                    break;
                case Format.Day:
                    dateString = datetime.ToString(Day);
                    break;
                case Format.DayHour:
                    dateString = datetime.ToString(DayHour);
                    break;
                case Format.Year:
                    dateString = datetime.ToString(Year);
                    break;
                case Format.FullDate:
                    dateString = datetime.ToString(FullDate);
                    break;
                case Format.DayMonth:
                    dateString = datetime.ToString(DayMonth);
                    break;
                case Format.Time:
                    dateString = datetime.ToString(Time);
                    break;
                case Format.Time24Hour:
                    dateString = datetime.ToString(Time24Hour);
                    break;
                case Format.FullDateGMT:
                    dateString = datetime.ToString(FullDateGMT);
                    break;
                case Format.MonthYear:
                    dateString = datetime.ToString(MonthYear);
                    break;
                case Format.DayDateMonth:
                    dateString = datetime.ToString(DayDateMonth);
                    break;
                case Format.DayMonthYearTime:
                    dateString = datetime.ToString(DayMonthYearTime);
                    break;
            }

            return dateString;
        }

        public static List<string> ReturnYears()
        {
            List<string> YearsList = new List<string>();

            DateTime StartDate = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            var Years = Enumerable.Range(0, DateTime.Now.Year - StartDate.Year)
                .Select(StartDate.AddYears)
                .ToList();

            foreach (var item in Years)
            {
                YearsList.Add(item.Year.ToString());
            }

            return YearsList;
        }

        public static List<string> ReturnComingYears()
        {
            List<string> YearsList = new List<string>();

            var endyear = (DateTime.Now.Year + 15);

            DateTime EndDate = new DateTime(endyear, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime StartDate = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            var Years = Enumerable.Range(0, EndDate.Year - DateTime.Now.Year)
                .Select(StartDate.AddYears)
                .ToList();

            foreach (var item in Years)
            {
                YearsList.Add(item.Year.ToString());
            }

            return YearsList;
        }

        public static int ReturnNumberOfDays(long startDate, long endDate)
        {
            int days = (int)Math.Ceiling((ReturnDateTimeFromlong(endDate) - ReturnDateTimeFromlong(startDate)).TotalDays);

            int _days;

            if (days < 1)
            {
                _days = 1;
            }
            else
            {
                _days = days;
            }

            return _days;
        }

        /// <summary>
        /// Takes in time as a long and returns a Date string
        /// Formatted for chats
        /// </summary>
        /// <param name="startDate">Takes in StartDate as a long</param>
        /// <returns>Returns a chat formatted Date string</returns>
        public static string ReturnChatFormatedDate(long startDate)
        {
            string date;

            var difference = ReturnDateTimeFromlong(ReturnCurrentTimeInLong()) - ReturnDateTimeFromlong(startDate);

            if (difference.TotalSeconds < 120)
            {
                date = $"seconds ago";
            }
            else if (difference.TotalMinutes < 5)
            {
                date = $"{(int)Math.Ceiling(difference.TotalMinutes)} mins ago";
            }
            else if (difference.TotalDays < 1)
            {
                date = ReturnDateStringFromLong(Format.Time24Hour, startDate);
            }
            else if (difference.TotalDays < 7)
            {
                date = ReturnDateStringFromLong(Format.DayHour, startDate);
            }
            else if (difference.TotalDays < 365)
            {
                date = ReturnDateStringFromLong(Format.DayDateMonthHour, startDate);
            }
            else
            {
                date = ReturnDateStringFromLong(Format.DayMonthYearTime, startDate);
            }

            return date;
        }

        /// <summary>
        /// Takes in time as a long and returns a Date string
        /// Formatted for Feed (Such as Social Feed or News Feed)
        /// </summary>
        /// <param name="startDate">Takes in StartDate as a long</param>
        /// <returns>Returns a feed formatted Date string</returns>
        public static string ReturnFeedFormatedDate(long startDate)
        {
            string date;

            var difference = ReturnDateTimeFromlong(ReturnCurrentTimeInLong()) - ReturnDateTimeFromlong(startDate);

            if (difference.TotalSeconds < 120)
            {
                date = $"seconds ago";
            }
            else if (difference.TotalMinutes < 5)
            {
                date = $"{(int)Math.Ceiling(difference.TotalMinutes)} mins ago";
            }
            else if (difference.TotalDays < 1)
            {
                date = $"{(int)Math.Ceiling(difference.TotalHours)}h";
            }
            else if (difference.TotalDays < 7)
            {
                date = ReturnDateStringFromLong(Format.DayHour, startDate);
            }
            else if (difference.TotalDays < 31)
            {
                date = $"{(int)Math.Ceiling(difference.TotalDays)}d";
            }
            else if (difference.TotalDays < 365)
            {
                date = ReturnDateStringFromLong(Format.DayMonth, startDate);
            }
            else
            {
                date = ReturnDateStringFromLong(Format.DayMonthYearTime, startDate);
            }

            return date;
        }
    }
}

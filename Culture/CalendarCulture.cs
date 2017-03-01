using System;

namespace KCalendar.Culture
{
    public abstract class CalendarCulture
    {
        protected IMonth[] Months { get; set; }
        protected IWeek[] Weeks { get; set; }
        protected string AMName { get; set; }
        protected string PMName { get; set; }
        public virtual IMonth this[int i] => Months[i];

        public virtual int MonthCount => Months.GetLength(0);

        public virtual void SetLeap(bool isLeapYear)
        {
            foreach (var item in Months)
                item.SetLeap(isLeapYear);
        }

        public virtual IMonth GetMonth(int month)
        {
            if (month <= 0 || month > MonthCount) throw new CalendarExceptions();
            foreach (var item in Months)
                if (item.Index == month)
                    return item;
            throw new CalendarExceptions();
        }

        public virtual IWeek GetWeekDay(int day)
        {
            if (day < 1 || day > 7)
                throw new CalendarExceptions();
            foreach (var item in Weeks)
            {
                if (item.DayIndex == day)
                {
                    return item;
                }
            }
            throw new CalendarExceptions();
        }



        /// <summary>
        ///<para>&#160;</para> d	Represents the day of the month as a number from 1 through 31. A single-digit day is formatted without a leading zero
        ///<para>&#160;</para>dd	Represents the day of the month as a number from 01 through 31. A single-digit day is formatted with a leading zero
        ///<para>&#160;</para>ddd	Represents the abbreviated name of the day of the week (Mon, Tues, Wed etc)
        ///<para>&#160;</para>dddd	Represents the full name of the day of the week (Monday, Tuesday etc)
        ///<para>&#160;</para>h	12-hour clock hour (e.g. 7)
        ///<para>&#160;</para>hh	12-hour clock, with a leading 0 (e.g. 07)
        ///<para>&#160;</para>H	24-hour clock hour (e.g. 19)
        ///<para>&#160;</para>HH	24-hour clock hour, with a leading 0 (e.g. 19)
        ///<para>&#160;</para>m	Minutes
        ///<para>&#160;</para>mm	Minutes with a leading zero
        ///<para>&#160;</para>M	Month number
        ///<para>&#160;</para>MM	Month number with leading zero
        ///<para>&#160;</para>MMM	Abbreviated Month Name (e.g. Dec)
        ///<para>&#160;</para>MMMM	Full month name (e.g. December)
        ///<para>&#160;</para>s	Seconds
        ///<para>&#160;</para>ss	Seconds with leading zero
        ///<para>&#160;</para>t	Abbreviated AM / PM (e.g. A or P)
        ///<para>&#160;</para>y	Year, no leading zero (e.g. 2001 would be 1)
        ///<para>&#160;</para>yy	Year, leadin zero (e.g. 2001 would be 01)
        ///<para>&#160;</para>yyy	Year, (e.g. 2001 would be 2001)
        ///<para>&#160;</para>yyyy	Year, (e.g. 2001 would be 2001)
        ///<para>&#160;</para>f	Represents the most significant digit of the seconds fraction; that is, it represents the tenths of a second in a date and time value.
        ///<para>&#160;</para>ff	Represents the two most significant digits of the seconds fraction; that is, it represents the hundredths of a second in a date and time value.
        ///<para>&#160;</para>fff	Represents the three most significant digits of the seconds fraction; that is, it represents the milliseconds in a date and time value.
        /// </summary>
        /// <param name="calendar"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(ICalendar calendar, string format)
        {
            if (format == null || format.Length <= 1)
            {
                return format;
            }
            if (format.Length == 1)
            {
                if (format.Contains("d"))
                    return format.Replace("d", ToString(calendar, "MM/dd/yyyy"));
                if (format.Contains("D"))
                    return format.Replace("D", ToString(calendar, "dddd, dd MMMM yyyy"));
                if (format.Contains("f"))
                    return format.Replace("f", ToString(calendar, "dddd, dd MMMM yyyy HH:mm"));
                if (format.Contains("F"))
                    return format.Replace("F", ToString(calendar, "dddd, dd MMMM yyyy HH:mm:ss"));
                if (format.Contains("g"))
                    return format.Replace("g", ToString(calendar, "MM/dd/yyyy HH:mm"));
                if (format.Contains("G"))
                    return format.Replace("G", ToString(calendar, "MM/dd/yyyy HH:mm:ss"));
                if (format.Contains("m"))
                    return format.Replace("m", ToString(calendar, "MMMM dd"));
                if (format.Contains("r"))
                    return format.Replace("r", ToString(calendar, "ddd, dd MMM yyyy HH':'mm':'ss 'GMT"));
                if (format.Contains("s"))
                    return format.Replace("s", ToString(calendar, "yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                if (format.Contains("u"))
                    return format.Replace("u", ToString(calendar, "yyyy'-'MM'-'dd HH':'mm':'ss'Z'"));
                if (format.Contains("U"))
                    return format.Replace("U", ToString(calendar, "dddd, dd MMMM yyyy HH:mm:ss"));
                if (format.Contains("y"))
                    return format.Replace("y", ToString(calendar, "yyyy MMMM"));
            }
            if (format.Length > 1)
            {
                return format
                .Replace("yyyy", calendar.Year.ToString())
                .Replace("yyy", calendar.Year.ToString())
                .Replace("yy", calendar.Year.ToString().PadLeft(2, '0'))
                .Replace("y", (calendar.Year % 100).ToString())
                .Replace("MMMM", calendar.Month.Name)
                .Replace("MMM", calendar.Month.ShortName)
                .Replace("MM", calendar.Month.Index.ToString().PadLeft(2, '0'))
                .Replace("M", calendar.Month.Index.ToString())
                .Replace("dddd", calendar.DayofWeek.Name)
                .Replace("ddd", calendar.DayofWeek.ShortName)
                .Replace("dd", calendar.Day.ToString("D2"))
                .Replace("d", calendar.Day.ToString())
                .Replace("tt", calendar.Hour > 12 ? calendar.CalendarCulture.PMName : calendar.CalendarCulture.AMName)
                .Replace("HH", calendar.Hour.ToString("D2"))
                .Replace("H", calendar.Hour.ToString())
                .Replace("hh", calendar.Hour > 12 ? (calendar.Hour - 1).ToString() : calendar.Hour.ToString())
                .Replace("h", calendar.Hour > 12 ? (calendar.Hour - 1).ToString("D2") : calendar.Hour.ToString("D2"))
                .Replace("mm", calendar.Minute.ToString("D2"))
                .Replace("m", calendar.Minute.ToString())
                .Replace("ss", calendar.Second.ToString("D2"))
                .Replace("s", calendar.Second.ToString())
                .Replace("f", (calendar.Millisecond / 100).ToString())
                .Replace("ff", (calendar.Millisecond / 10).ToString())
                .Replace("fff", calendar.Millisecond.ToString());
            }
            return format;
        }

        /// <summary>
        /// Converts the value of the current ICalendar object to its equivalent string representation using the specified format.
        /// </summary>
        /// <param name="calendar"></param>
        /// <param name="format">A persian date and time format string.</param>
        /// <returns>A string representation of value of the current ICalendar object as specified by format.</returns>
        public string ToString(ICalendar calendar, DateFormat format)
        {
            switch (format)
            {
                case DateFormat.Date:
                    return calendar.Year + "/" + calendar.Month.Index.ToString().PadLeft(2, '0') + "/" + calendar.Day.ToString().PadLeft(2, '0');

                case DateFormat.FullDate:
                    return calendar.DayofWeek.Name + " " + calendar.Day + " " + calendar.Month.Name + " " + calendar.Year;

                case DateFormat.LongDate:
                    return calendar.DayofWeek.Name + " " + calendar.Day + " " + calendar.Month.Name;

                case DateFormat.ShortDate:
                    return calendar.Day + " " + calendar.Month.Index;
                default:
                    throw new NotImplementedException(format.ToString());
            }
        }


    }
}
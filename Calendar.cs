using System;
using KCalendar.Culture;

namespace KCalendar
{
    public abstract class Calendar : PositionalAstronomy, ICalendar
    {
        protected Calendar()
        {
            Init();
        }
        protected Calendar(int year, int month, int day)
            : this()
        {
            Year = year;
            Month = CalendarCulture.GetMonth(month);
            Day = day;
        }
        protected Calendar(double julianNumber)
            : this()
        {
            JulianToDate(julianNumber);
        }

        protected Calendar(ICalendar iCalendar)
            : this(iCalendar.DateToJulian()) { }

        protected Calendar(DateTime dateTime) : this(dateTime.Year, dateTime.Month, dateTime.Day) { }
        public virtual int Year { get; set; }
        public virtual IMonth Month { get; set; }

        public virtual int Day { get; set; }
        public virtual int MonthCount
        {
            get { return CalendarCulture.MonthCount; }
        }
        public abstract double Epoch { get; }

        public virtual double JulianDay
        {
            get
            {
                return DateToJulian(this);
            }
        }

        public bool IsLeap
        {
            get
            {
                var isLeap = LeapAlgorithm.IsLeap(this);
                CalendarCulture.SetLeap(isLeapYear: isLeap);
                return isLeap;
            }
        }

        public abstract int DayOfYear { get; }

        public IWeek DayofWeek
        {
            get
            {
                return CalendarCulture.GetWeekDay((int)(Math.Ceiling(Math.Floor((JulianDay + 1.5)) % 7)) + 1);
                //return (int) ((Math.Ceiling(j + 1.5)) % 7 )  ;
            }
        }

        public abstract ICalendarLeap LeapAlgorithm { get; set; }
        public abstract ICalendar JulianToDate(double julianNumber);
        public ICalendar AddDay(int day)
        {
            var j = JulianDay + day;
            return JulianToDate(j);
        }

        public IMonth ChangeMonth(int amountOfChangeMonth)
        {
            if (amountOfChangeMonth >= MonthCount || amountOfChangeMonth < 1)
                throw new CalendarExceptions();
            Month = CalendarCulture.GetMonth(Month.Index + amountOfChangeMonth);
            return CalendarCulture.GetMonth(Month);
        }

        public IMonth ChangeMonth(IMonth month, int amountOfChangeMonth)
        {
            if (amountOfChangeMonth >= MonthCount || amountOfChangeMonth < 1)
                throw new CalendarExceptions();
            month = CalendarCulture.GetMonth(month + amountOfChangeMonth);
            return month;
        }

        #region addMonth
        /// <summary>
        /// Add or minus some month to this instant 
        /// </summary>
        /// <param name="month">can be variable int number ; negative for minus month and positiv for add month</param>
        /// <returns>MemberwiseClone of this instant after calculate</returns>
        public virtual ICalendar AddMonth(int month)
        {
            int r;
            while (true)
            {
                if (month == 0) break;
                if (month <= MonthCount)
                {
                    Month = ChangeMonth(month);
                    break;
                }
                r = month / MonthCount;
                Year += r;
                month -= (r * MonthCount);
            }
            return (ICalendar)MemberwiseClone();
        }
        #endregion addMonth

        public virtual ICalendar AddYear(int year)
        {
            Year += year;
            return (ICalendar)MemberwiseClone();
        }

        public abstract CalendarCulture CalendarCulture { get; set; }

        public virtual double DateToJulian()
        {
            return DateToJulian(this);
        }

        public abstract double DateToJulian(ICalendar calendarDate);
        public virtual ICalendar CastTo(ICalendar iCalendar)
        {
            if (iCalendar == null) throw new Exception("");
            return iCalendar.JulianToDate(JulianDay);
        }

        public IMonth GetMonthInfo(int month)
        {
            if (month < 1 && month > MonthCount)
                throw new Exception("");
            return CalendarCulture.GetMonth(month);//-1
        }
        /// <summary>
        /// Converts the string representation of a date format to its Calendar date equivalent.
        /// </summary>
        /// <param name="date">A string containing a date format to convert.</param>
        /// <returns>Calendar equivalent to the  date contained in date.</returns>
        public ICalendar Parse(string date)
        {
            if (date == null) throw new ArgumentNullException(nameof(date));
            date = date.ToLower().Trim();
            if (date.Length < 6)
            {
                throw new Exception("Format of string is not a date format!");
            }
            var tmp = date.Replace('\\', '/').Split('/');
            if (tmp == null) throw new Exception("Date Not in correct format!");
            if (tmp[2].Length > tmp[0].Length)
            {
                Year = Convert.ToInt32(tmp[2]);
                Month = GetMonthInfo(Convert.ToInt32(tmp[1]));
                Day = Convert.ToInt32(tmp[0]);
            }
            else
            {
                Year = Convert.ToInt32(tmp[0]);
                Month = GetMonthInfo(Convert.ToInt32(tmp[1]));
                Day = Convert.ToInt32(tmp[2]);
            }
            return (ICalendar)MemberwiseClone();
        }
        public DateTime ToDateTime()
        {
            var gd = (GregorianDate)CastTo(new GregorianDate());
            var dt = new DateTime(gd.Year, gd.Month, gd.Day);
            return dt;
        }
        public static DateTime ToDateTime(ICalendar calendarDate)
        {
            if (calendarDate == null) throw new ArgumentNullException(nameof(calendarDate));
            var gd = (GregorianDate)calendarDate.CastTo(new GregorianDate());
            var dt = new DateTime(gd.Year, gd.Month, gd.Day);
            return dt;
        }

        /// <summary>
        /// Converts the string representation of a date format to its Calendar date equivalent.
        /// </summary>
        /// <param name="date">A string containing a date format to convert.</param>
        /// <param name="calendarType">a instance of calendar that date string based on it</param>
        /// <returns>Calendar equivalent to the  date contained in date.</returns>
        public static ICalendar Parse(string date, ICalendar calendarType)
        {
            if (date == null) throw new ArgumentNullException(nameof(date));
            if (calendarType == null) throw new ArgumentNullException(nameof(calendarType));
            date = date.ToLower().Trim();
            if (date.Length < 6)
            {
                throw new Exception("Format of string is not a date format!");
            }
            var tmp = date.Replace('\\', '/').Split('/');
            if (tmp == null) throw new Exception("Date Not in correct format!");
            if (tmp[2].Length > tmp[0].Length)
            {
                calendarType.Year = Convert.ToInt32(tmp[2]);
                calendarType.Month = calendarType.GetMonthInfo(Convert.ToInt32(tmp[1]));
                calendarType.Day = Convert.ToInt32(tmp[0]);
            }
            else
            {
                calendarType.Year = Convert.ToInt32(tmp[0]);
                calendarType.Month = calendarType.GetMonthInfo(Convert.ToInt32(tmp[1]));
                calendarType.Day = Convert.ToInt32(tmp[2]);
            }
            return calendarType;
        }

        public string ToString(string format)
        {
            return CalendarCulture.ToString(this, format);
        }

        public string ToString(DateFormat format)
        {
            return CalendarCulture.ToString(this, format);
        }
        public static explicit operator DateTime(Calendar calendar)
        {
            return new DateTime(calendar.Year, calendar.Month, calendar.Day);
        }


        protected abstract void Init();
    }
}

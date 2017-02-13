using System;

namespace KCalendar.Culture {
    public abstract class CalendarCulture {
        protected IMonth[] Months { get; set; }
        protected IWeek[] Weeks { get; set; }
        public virtual IMonth this[int i] => Months[i];

        public virtual int MonthCount => Months.GetLength(0);

        public virtual void SetLeap(bool isLeapYear) {
            foreach (var item in Months)
                item.SetLeap(isLeapYear);
        }

        public virtual IMonth GetMonth(int month) {
            if(month <= 0 || month > MonthCount) throw new CalendarExceptions();
            foreach (var item in Months)
                if (item.Index == month)
                    return item;
            throw new CalendarExceptions();
        }

        public virtual IWeek GetWeekDay(int day) {
            if (day < 1 || day > 7)
                throw new CalendarExceptions();
            foreach (var item in Weeks) {
                if (item.DayIndex == day) {
                    return item;
                }
            }
            throw new CalendarExceptions();
        }

        public string ToString(ICalendar calendar, string format) {
            return format.Replace("yyyy", calendar.Year.ToString())
                .Replace("yy", calendar.Year.ToString().PadLeft(2, '0'))
                .Replace("y", (calendar.Year % 100).ToString())
                .Replace("MMMM", calendar.Month.Name)
                .Replace("MMM", calendar.Month.ShortName)
                .Replace("MM", calendar.Month.Index.ToString().PadLeft(2, '0'))
                .Replace("M", calendar.Month.Index.ToString())
                .Replace("dddd", calendar.DayofWeek.Name)
                .Replace("ddd", calendar.DayofWeek.ShortName)
                .Replace("dd", calendar.DayofWeek.DayIndex.ToString().PadLeft(2, '0'))
                .Replace("d", calendar.Day.ToString());
        }

        /// <summary>
        /// Converts the value of the current ICalendar object to its equivalent string representation using the specified format.
        /// </summary>
        /// <param name="calendar"></param>
        /// <param name="format">A persian date and time format string.</param>
        /// <returns>A string representation of value of the current ICalendar object as specified by format.</returns>
        public string ToString(ICalendar calendar, DateFormat format) {
            switch (format) {
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
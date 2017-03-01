using System;
using KCalendar.Culture;

namespace KCalendar
{
    public class IslamicDate : Calendar
    {
        public IslamicDate() { }
        public IslamicDate(int year, int month, int day)
            : base(year, month, day)
        { }

        public IslamicDate(double julianNumber)
            : base(julianNumber)
        { }

        public IslamicDate(ICalendar iCalendar)
            : base(iCalendar)
        { }

        public IslamicDate(DateTime dateTime)
            : base(new GregorianDate(dateTime).JulianDay)
        { }

        protected IslamicDate(int year, IMonth month, int day, int hour, int minute, int second, int millisecond) : base(year, month, day, hour, minute, second, millisecond) { }
        protected IslamicDate(int year, IMonth month, int day, int hour, int minute, int second) : base(year, month, day, hour, minute, second) { }

        public override double Epoch => 1948439.5;

        public static ICalendar Today => new IslamicDate(DateTime.Now);

        public override int DayOfYear => (int)(JulianDay - DateToJulian(new IslamicDate(Year, 1, 1)));

        public override ICalendarLeap LeapAlgorithm { get; set; }
        public override ICalendar JulianToDate(double julianNumber)
        {
            double year;
            double month;
            double day;

            julianNumber = Math.Floor(julianNumber) + 0.5;
            year = Math.Floor(((30 * (julianNumber - Epoch)) + 10646) / 10631);
            month = Math.Min(12, Math.Ceiling((julianNumber - (29 + DateToJulian(new IslamicDate((int)year, 1, 1)))) / 29.5) + 1);
            day = (julianNumber - DateToJulian(new IslamicDate((int)year, (int)month, 1))) + 1;
            Year = (int)year;
            Month = CalendarCulture.GetMonth((int)month);
            Day = (int)day;
            return new IslamicDate(Year, Month, Day);
        }

        public override CalendarCulture CalendarCulture { get; set; }
        public override double DateToJulian(ICalendar calendarDate)
        {
            return (calendarDate.Day +
                       Math.Ceiling(29.5 * (calendarDate.Month.Index - 1))
                      + (calendarDate.Year - 1) * 354
                      + Math.Floor(d: (3 + (11 * calendarDate.Year)) / 30.0) + Epoch) - 1; ;
        }

        protected override void Init()
        {
            CalendarCulture = new IslamicCalendarCulture();
            LeapAlgorithm = new IslamicLeap();
        }


        public static explicit operator IslamicDate(string date)
        {
            return (IslamicDate)Parse(date, new IslamicDate());
        }

        public static explicit operator IslamicDate(double jDay)
        {
            return new IslamicDate(jDay);
        }
    }
}

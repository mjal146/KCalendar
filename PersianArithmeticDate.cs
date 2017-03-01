using System;
using KCalendar.Culture;

namespace KCalendar
{
   public  class PersianArithmeticDate : Calendar
    {
        public PersianArithmeticDate() { }
        public override double Epoch => 1948320.5;

        protected override void Init()
        {
            CalendarCulture = new PersianCalendarCulture();
            LeapAlgorithm = new PersianBirashkLeap();
        }

        public PersianArithmeticDate(int year, int month, int day)
            : base(year, month, day)
        { }

        protected PersianArithmeticDate(int year, IMonth month, int day, int hour, int minute, int second, int millisecond) : base(year, month, day, hour, minute, second, millisecond) { }
        protected PersianArithmeticDate(int year, IMonth month, int day, int hour, int minute, int second) : base(year, month, day, hour, minute, second) { }
        public PersianArithmeticDate(double julianNumber)
            : base(julianNumber)
        { }

        public PersianArithmeticDate(ICalendar iCalendar)
            : base(iCalendar)
        { }

        public PersianArithmeticDate(DateTime dateTime)
            : base(new GregorianDate(dateTime).JulianDay)
        { }

        public static ICalendar Today => new PersianArithmeticDate(DateTime.Now);

        public override int DayOfYear => (int)(JulianDay - DateToJulian(new PersianArithmeticDate(Year, 1, 1)));

        public override ICalendarLeap LeapAlgorithm { get; set; }

        /// <summary>
        /// Convert Julian number to persian calendarDate 
        /// </summary>
        /// <param name="julianNumber"></param>
        /// <returns></returns>
        public override ICalendar JulianToDate(double julianNumber)
        {
            double year;
            double month;
            double day;
            double depoch;
            double cycle;
            double cyear;
            double ycycle;
            double aux1;
            double aux2;
            double yday;
            double jd;

            jd = Math.Floor(julianNumber) + 0.5;

            depoch = jd - DateToJulian(new PersianArithmeticDate(475, 1, 1));
            cycle = Math.Floor(depoch / 1029983);
            cyear = Mod(depoch, 1029983);
            if (Math.Abs(cyear - 1029982) < 0.5)
            {
                ycycle = 2820;
            }
            else
            {
                aux1 = Math.Floor(cyear / 366);
                aux2 = cyear % 366;
                ycycle = Math.Floor(((2134 * aux1) + (2816 * aux2) + 2815) / 1028522) + aux1 + 1;
            }
            year = ycycle + (2820 * cycle) + 474;
            year = year <= 0 ? year - 1 : year;
            if (year <= 0)
            {
                year--;
            }
            yday = (jd - DateToJulian(new PersianArithmeticDate((int)year, 1, 1))) + 1;
            month = (yday <= 186) ? Math.Ceiling(yday / 31) : Math.Ceiling((yday - 6) / 30);
            day = (jd - DateToJulian(new PersianArithmeticDate((int)year, (int)month, 1))) + 1;
            Year = (int)year;
            Month = CalendarCulture.GetMonth((int)month);//(int)month;
            Day = (int)day;
            return new PersianArithmeticDate(Year, Month, Day); ;
        }
        

        public override CalendarCulture CalendarCulture { get; set; }

        public override double DateToJulian(ICalendar calendarDate)
        {
            double epbase = calendarDate.Year - ((calendarDate.Year >= 0) ? 474 : 473);
            var epyear = 474 + (epbase % 2820);
            return (calendarDate.Day + ((calendarDate.Month <= 7) ? ((calendarDate.Month - 1) * 31) : (((calendarDate.Month - 1) * 30) + 6))
                + Math.Floor(((epyear * 682) - 110) / 2816) + (epyear - 1) * 365 + (Epoch - 1));
        }

        public static explicit operator PersianArithmeticDate(string date)
        {
            return (PersianArithmeticDate)Parse(date, new PersianArithmeticDate());
        }

        public static explicit operator PersianArithmeticDate(double jDay)
        {
            return new PersianArithmeticDate(jDay);
        }
    }
}

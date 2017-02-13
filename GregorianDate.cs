using System;
using KCalendar.Culture;

namespace KCalendar
{
    public class GregorianDate : Calendar
    {
        public GregorianDate() { }

        public GregorianDate(int year, int month, int day)
            : base(year, month, day)
        { }

        public GregorianDate(double julianNumber)
            : base(julianNumber)
        { }

        public GregorianDate(ICalendar iCalendar)
            : base(iCalendar)
        { }

        public GregorianDate(DateTime dateTime)
            : base(dateTime)
        { }
        public override double Epoch => 1721425.5;

        public static ICalendar Today => new GregorianDate(DateTime.Today);

        public override int DayOfYear => (int)(JulianDay - DateToJulian(new GregorianDate(Year, 1, 1)));

        public override ICalendarLeap LeapAlgorithm { get; set; }

        public sealed override ICalendar JulianToDate(double julianNumber)
        {
            double wjd;
            double depoch;
            double quadricent;
            double dqc;
            double cent;
            double dcent;
            double quad;
            double dquad;
            double yindex;
            double year;
            double yearday;
            double leapadj;

            wjd = Math.Floor(julianNumber - 0.5) + 0.5;
            depoch = wjd - Epoch;
            quadricent = Math.Floor(depoch / 146097);
            dqc = depoch % 146097;
            cent = Math.Floor(dqc / 36524);
            dcent = dqc % 36524;
            quad = Math.Floor(dcent / 1461);
            dquad = dcent % 1461;
            yindex = Math.Floor(dquad / 365);
            year = (quadricent * 400) + (cent * 100) + (quad * 4) + yindex;
            if (!((cent == 4) || (yindex == 4)))
            {
                year++;
            }
            yearday = wjd - DateToJulian(new GregorianDate((int)year, 1, 1));
            leapadj = ((wjd < DateToJulian(new GregorianDate((int)year, 3, 1))) ? 0
                                                          :
                          (LeapAlgorithm.IsLeap(new GregorianDate((int)year, 1, 1)) ? 1 : 2)
                      );
            var month = Math.Floor((((yearday + leapadj) * 12) + 373) / 367);
            var day = (wjd - DateToJulian(new GregorianDate((int)year, (int)month, 1))) + 1;
            Year = (int)year;
            Month = CalendarCulture.GetMonth((int)month);
            Day = (int)day;
            return new GregorianDate(Year, Month, Day);
        }


        public override CalendarCulture CalendarCulture { get; set; }

        public override double DateToJulian(ICalendar calendarDate)
        {
            if (calendarDate == null) throw new NotImplementedException();
            var julianDay =
                (
                    (Epoch - 1)
                    + (365 * (calendarDate.Year - 1)
                )
                + Math.Floor((double)((calendarDate.Year - 1) / 4))
                                + ((-1) * Math.Floor((double)(calendarDate.Year - 1) / 100))
                                + Math.Floor((double)(calendarDate.Year - 1) / 400)
                                + Math.Floor((((367 * (double)calendarDate.Month) - 362) / 12)
                                             + ((calendarDate.Month <= 2) ? 0 : (LeapAlgorithm.IsLeap(calendarDate) ? -1 : -2)) +
                                             calendarDate.Day));
            return julianDay;
        }

        protected override void Init()
        {
            CalendarCulture = new GregorianCalendarCulture();
            LeapAlgorithm = new GregorianLeap();
        }

        public static explicit operator GregorianDate(string date)
        {
            return (GregorianDate)Parse(date, new GregorianDate());
        }
        public static explicit operator GregorianDate(double jDay)
        {
            return new GregorianDate(jDay);
        }
    }
}
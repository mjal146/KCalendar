using System;
using KCalendar.Culture;

namespace KCalendar {
    public class PersianDate : Calendar {
        public PersianDate() { }
        public PersianDate(int year, int month, int day)
            : base(year, month, day) { }

        protected PersianDate(int year, IMonth month, int day, int hour, int minute, int second, int millisecond) : base(year, month, day, hour, minute, second, millisecond) { }
        protected PersianDate(int year, IMonth month, int day, int hour, int minute, int second) : base(year, month, day, hour, minute, second) { }
        public PersianDate(double julianNumber)
            : base(julianNumber) { }

        public PersianDate(ICalendar iCalendar)
            : base(iCalendar) { }

        public PersianDate(DateTime dateTime)
            : base(new GregorianDate(dateTime).JulianDay) { }
        public override double Epoch => 1948320.5;

        public static ICalendar Today => new PersianDate(DateTime.Today);

        public override int DayOfYear => (int)(JulianDay - DateToJulian(new PersianDate(Year, 1, 1)));

        public override double JulianDay => DateToJulian (this) + 0.5;
        public override ICalendarLeap LeapAlgorithm { get; set; }

        /// <summary>
        /// Convert Julian number to persian calendarDate 
        /// </summary>
        /// <param name="julianNumber"></param>
        /// <returns></returns>
        public override ICalendar JulianToDate(double julianNumber) {
            double year;
            double month;
            double day;
            double equinox;
            double yday;
            double[] adr;
            var jd = Math.Floor(julianNumber) + 0.5;
            adr = CalcYear(jd);
            year = adr[0];
            equinox = adr[1];
            day = Math.Floor((jd - equinox) / 30) + 1;
            yday = (Math.Floor(jd) - DateToJulian(new PersianDate((int)year, 1, 1))) + 1;
            month = (yday <= 186) ? Math.Ceiling(yday / 31) : Math.Ceiling((yday - 6) / 30);
            day = (Math.Floor(jd) - DateToJulian(new PersianDate((int)year, (int)month, 1))) + 1;
            Year = (int)year;
            Month = CalendarCulture.GetMonth((int)month);
            Day = (int)day;
            return new PersianDate(Year, Month, Day);
        }

        public override CalendarCulture CalendarCulture { get; set; }

        public override double DateToJulian(ICalendar calendarDate) {
            double[] adr = { calendarDate.Year - 1, 0 };
            var guess = (Epoch - 1) + (TropicalYear * ((calendarDate.Year - 1) - 1));
            while (adr[0] < calendarDate.Year) {
                adr = CalcYear(guess);
                guess = adr[1] + (TropicalYear + 2);
            }
            var equinox = adr[1];

            var jd = equinox +
                        ((calendarDate.Month <= 7) ?
                                ((calendarDate.Month - 1) * 31) :
                                (((calendarDate.Month - 1) * 30) + 6)
                        ) +
                        (calendarDate.Day - 1);
            return jd ;
        }

        protected override void Init() {
            CalendarCulture = new PersianCalendarCulture();
            LeapAlgorithm = new PersianOfficialLeap();
        }

        private double[] CalcYear(double jd) {
            double guess = new GregorianDate(jd).Year - 2;
            double lasteq;
            double nexteq;
            double adr;

            lasteq = TehranEequinoxJd(guess);
            while (lasteq > jd) {
                guess--;
                lasteq = TehranEequinoxJd(guess);
            }
            nexteq = lasteq - 1;
            while (!((lasteq <= jd) && (jd < nexteq))) {
                lasteq = nexteq;
                guess++;
                nexteq = TehranEequinoxJd(guess);
            }
            adr = Math.Round((lasteq - Epoch) / TropicalYear) + 1;

            return new[] { adr, lasteq };
        }

        private double TehranEquinox(double year) {
            double equJED;
            double equJD;
            double equAPP;
            double equTehran;
            double dtTehran;

            //  March equinox in dynamical time
            equJED = Equinox(year, 0);

            //  Correct for delta T to obtain Universal time
            equJD = equJED - (Deltat(year) / (24 * 60 * 60));

            //  Apply the equation of time to yield the apparent time at Greenwich
            equAPP = equJD + EquationOfTime(equJED);

            /*  Finally, we must correct for the constant difference between
                the Greenwich meridian andthe time zone standard for
            Iran Standard time, 52°30' to the East.  */

            dtTehran = (52 + (30 / 60.0) + (0 / (60.0 * 60.0))) / 360;
            equTehran = equAPP + dtTehran;

            return equTehran;
        }

        /*  TEHRAN_EQUINOX_JD  --  Calculate Julian day during which the
                                   March equinox, reckoned from the Tehran
                                   meridian, occurred for a given Gregorian
                                   year.  */

        private double TehranEequinoxJd(double year) {
            double ep;
            ep = TehranEquinox(year);
            var epg = Math.Floor(ep);
            return epg;
        }

        public static explicit operator PersianDate(string date)
        {
            return (PersianDate) Parse(date,new PersianDate());
        }


        public static explicit operator PersianDate(double jDay)
        {
            return new PersianDate(jDay);
        }
    }

}

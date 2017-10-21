using System;
using KCalendar.Culture;

namespace KCalendar
{
    public interface ICalendar
    {
        int Year { get; set; }
        IMonth Month { get; set; }
        int Day { get; set; }
        int Hour { get; set; }
        int Minute { get; set; }
        int Second { get; set; }
        int Millisecond { get; set; }
        double Epoch { get; }
        double JulianDay { get; }
        bool IsLeap { get; }
        int DayOfYear { get; }
        IWeek DayofWeek { get; }
        CalendarCulture CalendarCulture { get; set; }
        ICalendarLeap LeapAlgorithm { get; set; }
        ICalendar JulianToDate(double julianNumber);
        ICalendar AddDay(int day);
        ICalendar AddMonth(int month);
        ICalendar AddYear(int year);
        ICalendar CastTo(ICalendar iCalendar);
        double DateToJulian();
        double DateToJulian(ICalendar calendarDate);
        IMonth GetMonthInfo(int month);
        ICalendar Parse(string date);
        DateTime ToDateTime();
        string ToString(string format);
        string ToString(DateFormat format);
        ICalendar FirstNextMonth(ICalendar destinationCalendar, int month);
        ICalendar FirstWeekDayDate(ICalendar destinationCalendar);
        ICalendar LastWeekDayDate(ICalendar destinationCalendar);
        ICalendar FirstNextDay(ICalendar destinationCalendarType, int month,int day);
        ICalendar GotoDate(int year, int month, int day);
        ICalendar GotoDate(int month, int day);
    }
}

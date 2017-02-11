namespace KCalendar
{
    class IslamicLeap : ICalendarLeap
    {
        public bool IsLeap(ICalendar date)
        {
            return (((date.Year * 11) + 14) % 30) < 11;
        }
    }
}

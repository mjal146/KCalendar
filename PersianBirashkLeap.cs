namespace KCalendar
{
    class PersianBirashkLeap : ICalendarLeap
    {
        public bool IsLeap(ICalendar date)
        {
            return ((((((date.Year - ((date.Year > 0) ? 474 : 473)) % 2820) + 474) + 38) * 682) % 2816) < 682;
        }
    }
}

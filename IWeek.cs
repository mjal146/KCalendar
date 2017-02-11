namespace KCalendar
{
    public interface IWeek
    {
        string Name { get; }
        string  ShortName { get; }
        int DayIndex { get; }
        bool IsRestDay { get; }
    }
}

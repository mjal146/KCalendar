namespace KCalendar
{
    public class Week : IWeek
    {
        public Week(string name, string shortName, int dayIndex, bool isRestDay)
        {
            Name = name;
            ShortName = shortName;
            DayIndex = dayIndex;
            IsRestDay = isRestDay;
        }

        public Week(string name, string shortName, int dayIndex)
        {
            Name = name;
            ShortName = shortName;
            DayIndex = dayIndex;
            IsRestDay = false;
        }  
        public Week(string name,  int dayIndex)
        {
            Name = name;
            ShortName = name[0].ToString();
            DayIndex = dayIndex;
            IsRestDay = false;
        } 
        public Week(string name,int dayIndex, bool isRestDay)
        {
            Name = name;
            ShortName = name[0].ToString();
            DayIndex = dayIndex;
            IsRestDay = isRestDay;
        }

        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public int DayIndex { get; private set; }
        public bool IsRestDay { get; private set; }
    }
}
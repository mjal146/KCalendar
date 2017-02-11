namespace KCalendar
{
    public class IMonth
    {
        public IMonth(string name, string shortName, int index, bool isLeapMonth, int normalLength, int leapLength)
        {
            IsLeapMonth = isLeapMonth;
            NormalLength = normalLength;
            LeapLength = leapLength;
            Name = name;
            ShortName = shortName;
            Index = index;
        }
        public IMonth(string name, string shortName, int index, int normalLength)
        {
            IsLeapMonth = false;
            NormalLength = normalLength;
            LeapLength = normalLength;
            Name = name;
            ShortName = shortName;
            Index = index;
        }
        public IMonth(string name, int index, bool isLeapMonth, int normalLength, int leapLength)
        {
            IsLeapMonth = isLeapMonth;
            NormalLength = normalLength;
            LeapLength = leapLength;
            Name = name;
            ShortName = name[0].ToString();
            Index = index;
        }
        public IMonth(string name, int index, int normalLength)
        {
            IsLeapMonth = false;
            NormalLength = normalLength;
            LeapLength = normalLength;
            Name = name;
            ShortName = name[0].ToString();
            Index = index;
        }

        public bool IsLeapMonth { get; private set; }
        public int NormalLength { get; private set; }
        public int LeapLength { get; private set; }
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public int Index { get; private set; }

        internal void SetLeap(bool isLeapYear)
        {
            if (LeapLength == NormalLength) return;
            IsLeapMonth = isLeapYear;
        }
        public static implicit operator int(IMonth month)
        {
            return month.Index;
        } 
    }
}
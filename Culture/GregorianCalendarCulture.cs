namespace KCalendar.Culture
{
    public class GregorianCalendarCulture : CalendarCulture
    {
        public GregorianCalendarCulture()
        {
            Weeks = new IWeek[]
            {
                new Week("Saturday",7),
                new Week("Sunday",1), 
                new Week("Monday", 2), 
                new Week("Tuesday", 3), 
                new Week("Wednesday", 4), 
                new Week("Thursday", 5), 
                new Week("Friday", 6, isRestDay: true)
            };
            Months = new[]
            {
                new IMonth("January",1,31), 
                new IMonth("February",2, isLeapMonth: false, normalLength: 28, leapLength: 29), 
                new IMonth("March",3,31), 
                new IMonth("April",4,30), 
                new IMonth("May",5,31), 
                new IMonth("June",6,30), 
                new IMonth("July",7,31), 
                new IMonth("August",8,31), 
                new IMonth("September",9,30), 
                new IMonth("October",10,31), 
                new IMonth("November",11,30), 
                new IMonth("December",12, 31)
            };
        }
 
    }
}
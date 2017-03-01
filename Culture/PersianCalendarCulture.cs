namespace KCalendar.Culture
{
    public class PersianCalendarCulture : CalendarCulture
    {
        public PersianCalendarCulture()
        {
            Weeks = new IWeek[]
            {
                new Week("شنبه",7),
                new Week("یکشنبه",1), 
                new Week("دوشنبه", 2), 
                new Week("سه شنبه", 3), 
                new Week("چهارشنبه", 4), 
                new Week("پنجشنبه", 5), 
                new Week("جمعه", 6, isRestDay: true)
            };
            Months = new[]
            {
                new IMonth("فروردین",1,31), 
                new IMonth("اردیبهشت",2,31), 
                new IMonth("خرداد",3,31), 
                new IMonth("تیر",4,31), 
                new IMonth("مرداد",5,31), 
                new IMonth("شهریور",6,31), 
                new IMonth("مهر",7,30), 
                new IMonth("آبان",8,30), 
                new IMonth("آذر",9,30), 
                new IMonth("دی",10,30), 
                new IMonth("بهمن",11,30), 
                new IMonth("اسفند",12, isLeapMonth: false, normalLength: 29, leapLength: 30) 
            };
            AMName = "ق.ظ";
            PMName = "ب.ظ";
        }

 
    }
}
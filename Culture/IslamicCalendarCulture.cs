namespace KCalendar.Culture
{
    class IslamicCalendarCulture : CalendarCulture
    {
        public IslamicCalendarCulture()
        {
            Weeks = new IWeek[]
            {
                new Week("الأحد",7),
                new Week("الإثنين",1), 
                new Week("الثلاثاء", 2), 
                new Week("الأربعاء", 3), 
                new Week("الخميس", 4), 
                new Week("الجمعة", 5), 
                new Week("السبت", 6, isRestDay: true)
            };
            Months = new[]
            {
                new IMonth("محرم",1, isLeapMonth: false, normalLength: 29, leapLength: 30) ,
                new IMonth("صفر",2, isLeapMonth: false, normalLength: 29, leapLength: 30) ,
                new IMonth("ربیع‌الاول",3, isLeapMonth: false, normalLength: 29, leapLength: 30) ,
                new IMonth("ربیع‌الثانی",4, isLeapMonth: false, normalLength: 29, leapLength: 30) ,
                new IMonth("جمادی‌الاول",5, isLeapMonth: false, normalLength: 29, leapLength: 30) ,
                new IMonth("جمادی‌الثانی",6, isLeapMonth: false, normalLength: 29, leapLength: 30) ,
                new IMonth("رجب",7, isLeapMonth: false, normalLength: 29, leapLength: 30) ,
                new IMonth("شعبان",8, isLeapMonth: false, normalLength: 29, leapLength: 30) ,
                new IMonth("رمضان",9, isLeapMonth: false, normalLength: 29, leapLength: 30) ,
                new IMonth("شوال",10, isLeapMonth: false, normalLength: 29, leapLength: 30) ,
                new IMonth("ذیقعده",11, isLeapMonth: false, normalLength: 29, leapLength: 30) ,
                new IMonth("ذیحجه",12, isLeapMonth: false, normalLength: 29, leapLength: 30) 
            };

            PMName = "مساء";
            AMName = "صباحاً";
        }

        public override void SetLeap(bool isLeapYear)
        {
          base.SetLeap(isLeapYear);
        }

   
    }
}

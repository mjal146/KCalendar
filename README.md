JCalendar
--------------------
This is a plugin for converting Gregorian date  and Persian date (Jalali) and Hijri (Islamic) date into each other.

Installation
-----------------


Install the dependencies and devDependencies and start the server.

    Install-Package KCalendar

Getting Started
---------------
**Persian Date (Jalali Date) **

For Create a Persian Date with specified year, month, day use the constructor :

    PersianDate  persianDate = new PersianDate(1396, 9, 23);
Or for create a Persian date with `Date`: 

    PersianDate persianDate = new PersianDate(new Date());

in above code, will create new instance from `DateTime.Now()` and get now date.

For create a Persian date with JulianDay :

    PersianDate persianDate = new PersianDate(2458101.5);
   
   -------------------
    
 **Islamic Date (Hijri Date)**   
 
You can create instance of `IslamicDate`  similar to above example :
For Create a `IslamicDate` with specified year, month, day use the constructor :

    IslamicDate islamicDate= new IslamicDate(1396, 9, 23);
    
Or for create a IslamicDate  with `DateTime`: 

    IslamicDate islamicDate = new IslamicDate (DateTime.Now());

For create a IslamicDate  with JulianDay :

    IslamicDate islamicDate = new IslamicDate (2458101.5);

-------------
**Gregorian Date**

For Gregorian Date also similar to above example.

-----------

Convert date to together
------------------------
For Convert Date to other just pass to new date constructor :
**Islamic to Persian date:**

    IslamicDate islamicDate ;/* = your constructor */
    PersianDate persianDate =new PersianDate(islamicDate);
      
**Gregorian to Persian date :**

    GregorianDate date; /* your constructor */
    PersinaDate  persianDate =new PersianDate(date);

**and ...**

You can also use the following method

    IslamicDate islamicDate ;/* = your constructor */
    PersianDate persianDate= (PersianDate)islamicDate.castTo(new PersianDate());

***** For the rest, you can look like the example above

Tip
---
There are two types of algorithms available for Persian history: one of the official Iranian algorithms and Ahmad Barashk's algorithm.
The `PersianDate` class is official date that used in Iran and `PersianArithmeticDate` class developed by Bireshks Algorithm .
You can use `PersianArithmeticDate` like `PersianDate` create instance way.
**more information**

Parse Date From Pattern
-----------------
You can create new instance of `PersianDate` with `DateTimeFormatter` :

     DateTimeFormatter fmt = DateTimeFormat.forPattern("yyyy-MM-dd'T'HH:mm:ss");
     date = fmt.parseDateTime(group.getTitle());
    GregorianDate gregorianDate = new GregorianDate(date.getYear(),date.getMonthOfYear(), date.dayOfMonth().get());
    ICalendar persianDate = gregorianDate.castTo(PersianDate.class);

Or using other methods written for this purpose.

    parseFromPattern

toString Method :
-----------------
Way 1 :

**persian date**

    persianDate.toString(DateFormat.FullDate);// پنجشنبه 23 آذر 1396
    persianDate.toString(DateFormat.LongDate); // پنجشنبه 23 آذر
    persianDate.toString(DateFormat.Date);// 1396/09/23
    persianDate.toString(DateFormat.ShortDate);// 09/23

**Islamic date**

    islamicDate.toString(DateFormat.FullDate);//الخمیس‬ ٢٥ ربيع الاول ١٤٣٩
    islamicDate.toString(DateFormat.LongDate); // الخميس ٢٥ ربيع الاول
    islamicDate.toString(DateFormat.Date);// ۱۴۳۹/۰۳/۲۵ 
    islamicDate.toString(DateFormat.ShortDate);// ۰۳/۲۵

**Or custom Pattern**

    d        Represents the day of the month as a number from 1 through 31. A single-digit day is formatted without a leading zero
    dd        Represents the day of the month as a number from 01 through 31. A single-digit day is formatted with a leading zero
    ddd        Represents the abbreviated name of the day of the week (Mon, Tues, Wed etc)
    dddd        Represents the full name of the day of the week (Monday, Tuesday etc)
    h        12-hour clock hour (e.g. 7)
    hh        12-hour clock, with a leading 0 (e.g. 07)
    H        24-hour clock hour (e.g. 19)
    HH        24-hour clock hour, with a leading 0 (e.g. 19)
    m        Minutes
    mm        Minutes with a leading zero
    M        Month number
    MM        Month number with leading zero
    MMM        Abbreviated Month Name (e.g. Dec)
    MMMM        Full month name (e.g. December)
    s        Seconds
    ss        Seconds with leading zero
    t        Abbreviated AM / PM (e.g. A or P)
    y        Year, no leading zero (e.g. 2001 would be 1)
    yy        Year, leadin zero (e.g. 2001 would be 01)
    yyy        Year, (e.g. 2001 would be 2001)
    yyyy        Year, (e.g. 2001 would be 2001)
    f        Represents the most significant digit of the seconds fraction; that is, it represents the tenths of a second in a date and time value.
    ff        Represents the two most significant digits of the seconds fraction; that is, it represents the hundredths of a second in a date and time value.
    fff        Represents the three most significant digits of the seconds fraction; that is, it represents the milliseconds in a date and time value.

For example :

    persianDate.toString("yyyy/MM/dd");// 1396/09/23


Custom Leap Algorithm
---------------------
You can write for each type of your own `Leap Algorithm` and use that algorithm, and you can also write for each one a specific `Culture`.
For example:

    public class PersianBirashkLeap implements ICalendarLeap {
        public boolean isLeap(ICalendar date) {
            return ((((((date.getYear() - ((date.getYear() > 0) ? 474 : 473)) % 2820) + 474) + 38) * 682) % 2816) < 682;
        }
    }
Above class is custom Leap Algorithm  and now set leap Algorithm to Persian Date :

    persianDate.setLeapAlgorithm(new PersianBirashkLeap());

More Detail About Persian Date
------------------------------
This article is taken from https://www.fourmilab.ch/ :

**Persian Calendar** 

The modern Persian calendar was adopted in 1925, supplanting (while retaining the month names of) a traditional calendar dating from the eleventh century. The calendar consists of 12 months, the first six of which are 31 days, the next five 30 days, and the final month 29 days in a normal year and 30 days in a leap year.

Each year begins on the day in which the March equinox occurs at or after solar noon at the reference longitude for Iran Standard Time (52°30' E). Days begin at midnight in the standard time zone. There is no leap year rule; 366 day years do not recur in a regular pattern but instead occur whenever that number of days elapse between equinoxes at the reference meridian. The calendar therefore stays perfectly aligned with the seasons. No attempt is made to synchronise months with the phases of the Moon.

There is some controversy about the reference meridian at which the equinox is determined in this calendar. Various sources cite Tehran, Esfahan, and the central meridian of Iran Standard Time as that where the equinox is determined; in this implementation, the Iran Standard Time longitude is used, as it appears that this is the criterion used in Iran today. As this calendar is proleptic for all years prior to 1925 c.e., historical considerations regarding the capitals of Persia and Iran do not seem to apply. 

**Persian Algorithmic Calendar**
Ahmad Birashk proposed an alternative means of determining leap years for the Persian calendar. His technique avoids the need to determine the moment of the astronomical equinox, replacing it with a very complex leap year structure. Years are grouped into cycles which begin with four normal years after which every fourth subsequent year in the cycle is a leap year. Cycles are grouped into grand cycles of either 128 years (composed of cycles of 29, 33, 33, and 33 years) or 132 years, containing cycles of of 29, 33, 33, and 37 years. A great grand cycle is composed of 21 consecutive 128 year grand cycles and a final 132 grand cycle, for a total of 2820 years. The pattern of normal and leap years which began in 1925 will not repeat until the year 4745!

This is not the calendar in use in Iran! It is presented here solely because there are many computer implementations of the Persian calendar which use it (with which users may wish to compare results), and because its baroque complexity enthralls programmers like myself.

Each 2820 year great grand cycle contains 2137 normal years of 365 days and 683 leap years of 366 days, with the average year length over the great grand cycle of 365.24219852. So close is this to the actual solar tropical year of 365.24219878 days that this calendar accumulates an error of one day only every 3.8 million years. As a purely solar calendar, months are not synchronised with the phases of the Moon. 

Acknowledgment
--------------
[Fourmilab calendar documentation](https://www.fourmilab.ch/documents/calendar/)



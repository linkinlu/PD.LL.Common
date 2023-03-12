using System;

public static class DateTimeExtesion
{
 
    public static bool IsInRageWithMileSecoondUnit(this DateTime dt, DateTime from , DateTime to)
    {
        return dt>=from && dt<=to;
    }

    public static bool IsInRangeWithDayUnit(this DateTime dt, DateTime from , DateTime to)
    {
        return dt.Date>=from.Date && dt<=to.Date;
    }

}
using System;
using System.Runtime.CompilerServices;
using System.Text;

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

    public static string DescriptionBetweenDates(this DateTime startDt , DateTime endDt)
    {
        var timeSpan = endDt - startDt;
        StringBuilder result = new StringBuilder();
        if (timeSpan.Days > 0)
        {
            result.Append(timeSpan.Days);
            result.Append("��");
        }
        if (timeSpan.Hours > 0)
        {
            result.Append(timeSpan.Hours);
            result.Append("Сʱ");
        }
        if (timeSpan.Minutes > 0)
        {
            result.Append(timeSpan.Minutes);
            result.Append("����");
        }
        if (timeSpan.Seconds > 0)
        {
            result.Append(timeSpan.Seconds);
            result.Append("��");
        }
        if (timeSpan.Milliseconds > 0)
        {
            result.Append(timeSpan.Milliseconds);
            result.Append("����");
        }
        if (result.Length > 0)
            return result.ToString();
        return $"{timeSpan.TotalMilliseconds}����";
    }
}
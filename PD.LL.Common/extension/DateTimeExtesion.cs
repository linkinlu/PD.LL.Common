using System;
using System.Text;
using PD.LYY.UtilityLib.Model;

public static class DateTimeExtesion
{
 
    public static bool IsInRageWithMileSecoondUnit(this DateTime dt, DateTime from , DateTime to)
    {
        return dt>=from && dt<=to;
    }

    // public static bool IsInRangeWithoutTime(this DateTime dt, DateTime from , DateTime to)
    // {
    //     return dt.Date>=from.Date && dt<=to.Date;
    // }

    public static DateTime GetDayByConditionUnit(this DateTime dt, int count, ConditionUnit unit, bool minus= true)
    {
        if (count == 0 || dt == DateTime.MinValue) return DateTime.Now;

        switch (unit)
        {
            case ConditionUnit.Day: return minus ? dt.Date.AddDays(-count) : dt.Date.AddDays(count);
            case ConditionUnit.Month: return minus ? dt.Date.AddMonths(-count) : dt.Date.AddMonths(count);
            case ConditionUnit.Year: return minus ? dt.Date.AddYears(-count) : dt.Date.AddYears(count);
            default: return minus ? dt.Date.AddDays(-count) : dt.Date.AddDays(count);
        }
    }

    public static string DescriptionBetweenDates(this DateTime startDt , DateTime endDt)
    {
        var timeSpan = endDt - startDt;
        StringBuilder result = new StringBuilder();
        if (timeSpan.Days > 0)
        {
            result.Append(timeSpan.Days);
            result.Append("天");
        }
        if (timeSpan.Hours > 0)
        {
            result.Append(timeSpan.Hours);
            result.Append("小时");
        }
        if (timeSpan.Minutes > 0)
        {
            result.Append(timeSpan.Minutes);
            result.Append("分钟");
        }
        if (timeSpan.Seconds > 0)
        {
            result.Append(timeSpan.Seconds);
            result.Append("秒");
        }
        if (timeSpan.Milliseconds > 0)
        {
            result.Append(timeSpan.Milliseconds);
            result.Append("毫秒");
        }
        if (result.Length > 0)
            return result.ToString();
        return $"{timeSpan.TotalMilliseconds}毫秒";
    }
}
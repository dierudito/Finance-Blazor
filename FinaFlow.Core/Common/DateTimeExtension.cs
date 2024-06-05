namespace FinaFlow.Core.Common;
public static class DateTimeExtension
{
    public static DateTime GetFirstDay(this DateTime dateTime, int? year = null, int? month = null)
        => new(year ?? dateTime.Year, month ?? dateTime.Month, 1);

    public static DateTime GetLastDay(this DateTime dateTime, int? year = null, int? month = null)
    {
        var firstDay = dateTime.GetFirstDay(year, month);
        return firstDay.AddMonths(1).AddDays(-1);
    }
}

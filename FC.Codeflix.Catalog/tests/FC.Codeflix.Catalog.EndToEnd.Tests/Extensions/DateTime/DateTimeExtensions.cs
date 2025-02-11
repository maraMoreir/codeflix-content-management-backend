namespace FC.Codeflix.Catalog.EndToEnd.Tests.Extensions.DateTime;

internal static class DateTimeExtensions
{
    public static System.DateTime TrimMilisseconds(
        this System.DateTime dateTime
    ){
        return new System.DateTime(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            dateTime.Hour,
            dateTime.Minute,
            dateTime.Second,
            dateTime.Kind
        );
    }

}

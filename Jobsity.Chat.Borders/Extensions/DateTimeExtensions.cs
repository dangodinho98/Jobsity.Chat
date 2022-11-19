namespace Jobsity.Chat.Borders.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToFriendlyDateString(this DateTime date)
        {
            string formattedDate;
            if (date.Date == DateTime.Today)
            {
                formattedDate = "Today";
            }
            else if (date.Date == DateTime.Today.AddDays(-1))
            {
                formattedDate = "Yesterday";
            }
            else if (date.Date > DateTime.Today.AddDays(-6))
            {
                formattedDate = date.ToString("dddd");
            }
            else
            {
                formattedDate = date.ToString("MMMM dd, yyyy");
            }

            formattedDate += ", at " + date.ToString("t").ToLower();
            return formattedDate;
        }
    }
}

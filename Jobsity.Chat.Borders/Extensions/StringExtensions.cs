namespace Jobsity.Chat.Borders.Extensions
{
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        private const string Pattern = "^/stock=.+";

        public static bool IsBotCommand(this string command) => Regex.IsMatch(command, Pattern);
    }
}

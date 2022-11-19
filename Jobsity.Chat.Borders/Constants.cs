namespace Jobsity.Chat.Borders
{
    public static class Constants
    {
        public static readonly string StockApiClientName = "StockApi";
        public static readonly string ReceiveMessage = "ReceiveMessage";

        public static class Bot
        {
            public static readonly string Username = "Bot";
            public static readonly string Message = "{0} quote is ${1} per share";
        }

        public static class ErrorMessages
        {
            public static readonly string Default = "Oops, an error occurred.";
            public static readonly string MissingApplicationConfig = "Missing application config data.";
        }
    }
}

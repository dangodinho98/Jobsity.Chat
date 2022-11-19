namespace Jobsity.Chat.Bot
{
    public interface IBotProcessor
    {
        Task<string> ProcessCommand(string message);
    }
}

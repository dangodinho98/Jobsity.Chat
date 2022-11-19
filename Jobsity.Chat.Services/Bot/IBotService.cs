namespace Jobsity.Chat.Services.Bot
{
    public interface IBotService
    {
        Task<string> GetBotMessage(string message);
    }
}
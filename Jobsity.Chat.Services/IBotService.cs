namespace Jobsity.Chat.Services
{
    public interface IBotService
    {
        Task<string> GetBotMessage(string message);
    }
}
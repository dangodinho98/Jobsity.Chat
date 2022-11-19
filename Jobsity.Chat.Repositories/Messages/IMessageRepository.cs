namespace Jobsity.Chat.Repositories.Messages
{
    using Jobsity.Chat.Borders.Entities;

    public interface IMessageRepository
    {
        Task AddAsync(Message message);
        Task<IEnumerable<Message>> GetHistoryMessagesAsync();
    }
}

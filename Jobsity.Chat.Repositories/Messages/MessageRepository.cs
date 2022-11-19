namespace Jobsity.Chat.Repositories.Messages
{
    using Jobsity.Chat.Borders.Configuration;
    using Jobsity.Chat.Borders.Entities;
    using Microsoft.EntityFrameworkCore;

    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationConfig _applicationConfig;

        public MessageRepository(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig;
        }

        public async Task AddAsync(Message message)
        {
            await using var ctx = new ApplicationDbContext();
            await ctx.Messages.AddAsync(message);
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetHistoryMessagesAsync()
        {
            await using var ctx = new ApplicationDbContext();
            return await ctx.Messages
                .OrderBy(message => message.CreationDate)
                .Take(_applicationConfig.MessagesShown)
                .ToListAsync();
        }
    }
}

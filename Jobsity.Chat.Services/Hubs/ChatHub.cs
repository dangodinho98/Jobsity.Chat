namespace Jobsity.Chat.Services.Hubs
{
    using Jobsity.Chat.Borders.Dto;
    using Jobsity.Chat.Borders.Extensions;
    using Jobsity.Chat.Repositories.Messages;
    using Jobsity.Chat.Services.RabbitMQ;
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IProducer _producer;

        public ChatHub(IProducer producer,
            IMessageRepository messageRepository)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
        }

        public override async Task OnConnectedAsync()
        {
            var messages = await _messageRepository.GetHistoryMessagesAsync();

            foreach (var message in messages)
                await Clients.All.SendAsync(Borders.Constants.ReceiveMessage, message.Username, message.Text, message.CreationDate.ToFriendlyDateString());

            await base.OnConnectedAsync();
        }

        public void SendMessage(string sender, string message)
        {
            var messageDto = new MessageDto(sender, message);
            _producer.Send(messageDto);
        }
    }
}

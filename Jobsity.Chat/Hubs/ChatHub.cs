namespace Jobsity.Chat.Hubs
{
    using Jobsity.Chat.Services;
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        private readonly IBotService _botService;

        public ChatHub(IBotService botService)
        {
            _botService = botService;
        }

        public async Task SendMessage(string user, string message)
        {
            if (message.Contains("/stock="))
            {
                user = "Bot";
                message = await _botService.GetBotMessage(message);
            }

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

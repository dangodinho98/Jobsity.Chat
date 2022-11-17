namespace Jobsity.Chat.Tests.Hub
{
    using Jobsity.Chat.Hubs;
    using Jobsity.Chat.Services;
    using Microsoft.AspNetCore.SignalR;
    using Moq;

    public class Tests
    {
        private readonly Mock<IBotService> _botService = new();
        private readonly Mock<IHubCallerClients> _mockClients = new();
        private readonly Mock<IClientProxy> _mockClientProxy = new();
        private readonly Mock<HubCallerContext> _mockContext = new();
        private readonly ChatHub _chatHub;

        private readonly List<string> _clientIds = new() { "0", "1", "2" };
        public Tests()
        {
            _chatHub = new ChatHub(_botService.Object);

            _mockClients.Setup(client => client.All).Returns(_mockClientProxy.Object);
            _mockContext.Setup(context => context.ConnectionId).Returns(It.IsIn<string>(_clientIds));

            _chatHub = new ChatHub(_botService.Object)
            {
                Clients = _mockClients.Object,
                Context = _mockContext.Object
            };
        }

        [Fact]
        public async Task AllClientsNotified()
        {
            await _chatHub.SendMessage("TEST", "TEST MESSAGE");
            _mockClients.Verify(clients => clients.All, Times.Once);
        }

        [Fact]
        public async Task EmptyMessageDoesNotNotifyClients()
        {
            await _chatHub.SendMessage("TEST", string.Empty);
            _mockClients.Verify(clients => clients.All, Times.Never);
        }
    }
}

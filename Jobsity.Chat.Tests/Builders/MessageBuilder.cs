namespace Jobsity.Chat.Tests.Builders
{
    using Jobsity.Chat.Borders.Entities;

    public class MessageBuilder
    {
        private readonly Message _instance;

        public MessageBuilder()
        {
            _instance = new Message();
        }

        public MessageBuilder Withtext(string text)
        {
            _instance.Text = text;
            return this;
        }

        public Message Build() => _instance;
    }
}

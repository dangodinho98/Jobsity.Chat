namespace Jobsity.Chat.Borders.Dto
{
    public class MessageDto
    {
        public MessageDto(string? user, string message)
        {
            User = user;
            Message = message;

            CreationDate = DateTime.Now;
        }

        public string? User { get; private set; }
        public string Message { get; private set; }
        public DateTime CreationDate { get; }

        public void BotMessage(string message)
        {
            User = Constants.Bot.Username;
            Message = message;
        }
    }
}

namespace Jobsity.Chat.Borders.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

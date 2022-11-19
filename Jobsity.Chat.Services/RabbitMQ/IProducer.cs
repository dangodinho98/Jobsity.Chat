namespace Jobsity.Chat.Services.RabbitMQ
{
    using Jobsity.Chat.Borders.Dto;

    public interface IProducer
    {
        void Send(MessageDto messageDto);
    }
}

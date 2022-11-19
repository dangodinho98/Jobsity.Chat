namespace Jobsity.Chat.Services.RabbitMQ
{
    using Jobsity.Chat.Borders.Dto;

    public interface IProducer
    {
        Task Send(MessageDto messageDto);
    }
}

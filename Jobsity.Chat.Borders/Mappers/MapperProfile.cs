namespace Jobsity.Chat.Borders.Mappers
{
    using AutoMapper;
    using Jobsity.Chat.Borders.Dto;
    using Jobsity.Chat.Borders.Entities;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<MessageDto, Message>()
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Text,
                    opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.CreationDate,
                    opt => opt.MapFrom(src => src.CreationDate));
        }
    }
}

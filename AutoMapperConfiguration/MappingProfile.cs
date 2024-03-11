using AutoMapper;
using Data.Models;
using DTOs.OUTPUT;

namespace SkillBox.App.AutoMapperConfiguration
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Chat, ChatMiniDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name));

            CreateMap<Chat, ChatDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(d => d.ServiceName, opt => opt.MapFrom(c => c.Service.Name))
                .ForMember(d => d.ChatUsers, opt => opt.MapFrom(c => c.ChatUsers))
                .ForMember(d => d.Messages, opt => opt.MapFrom(c => c.Messages));
        }
    }
}

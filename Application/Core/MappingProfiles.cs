using AutoMapper;
using Domain.Entities;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Post, Post>()
                .ForMember(d => d.Title, opt => opt.Ignore())
                .ForMember(d => d.Message, opt => opt.Ignore())
                .ForMember(d => d.Modified, opt => opt.Ignore())
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created));
        }
    }
}

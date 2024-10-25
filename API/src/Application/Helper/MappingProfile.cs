using AutoMapper;
using Domain.Entity;
using Application.DTO.Request;
using Application.DTO.Response;

namespace Application.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Register, AppUser>();
            CreateMap<AppUser, UserProfile>();
        }
    }
}
using AutoMapper;
using Rick_and_Morty.Application.Dtos;
using Rick_and_Morty.Application.Requests.Account;
using Rick_and_Morty.Infrastructure.Identity.Models;

namespace Rick_and_Morty.Infrastructure.Identity.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<UpdateProfileRequest, ApplicationUser>();
        }
    }
}

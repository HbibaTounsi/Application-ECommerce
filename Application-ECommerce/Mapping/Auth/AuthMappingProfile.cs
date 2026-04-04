using Application_ECommerce.App.Athentification.Dtos;
using Application_ECommerce.Models.Auth;
using AutoMapper;

namespace Application_ECommerce.Mapping.Auth
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<RegisterViewModel, RegistrationRequestDto>();
            CreateMap<LoginViewModel, LoginRequestDto>();
            CreateMap<ResetPasswordViewModel, ResetPasswordDto>();
        }
    }
}
using AutoMapper;
using Goreu.Dto.Request.Pide.Credenciales;
using Goreu.Dto.Response.Pide.Credenciales;
using Goreu.Entities.Pide;

namespace Goreu.Services.Profiles.Pide
{
    public class CredencialReniecProfile : Profile
    {
        public CredencialReniecProfile()
        {
            CreateMap<CredencialReniec, CredencialReniecResponseDto>();
            CreateMap<AddCredencialReniecRequestDto, CredencialReniec>();

            CreateMap<CredencialReniecInfo, AddCredencialReniecRequestDto>();
        }
    }
}

namespace Goreu.Services.Profiles
{
    public class AplicacionProfile : Profile
    {
        public AplicacionProfile()
        {
            CreateMap<Aplicacion, AplicacionResponseDto>();
            CreateMap<AplicacionRequestDtoSingle, Aplicacion>();
            CreateMap<AplicacionRequestDto, Aplicacion>();
            CreateMap<AplicacionInfo, AplicacionResponseDto>();
        }
    }
}

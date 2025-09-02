using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.DtoResponse;
using Goreu.Entities;
using Goreu.Entities.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Services.Profiles
{
    public class RolProfile : Profile
    {

        public RolProfile()
        {
            CreateMap<RolRequestDto, Rol>();
            CreateMap<Rol, RolResponseDto>();
            CreateMap<RolInfo,RolResponseSingleDto>();

            CreateMap<Rol, RolPaginationResponseDto>()
                .ForMember(dest => dest.descripcionEntidad, opt => opt.MapFrom(src => src.EntidadAplicacion.Entidad.Descripcion))
                .ForMember(dest => dest.descripcionAplicacion, opt => opt.MapFrom(src => src.EntidadAplicacion.Aplicacion.Descripcion))
                .ForMember(dest => dest.descripcion, opt => opt.MapFrom(src => src.Name));
        }
    }
}

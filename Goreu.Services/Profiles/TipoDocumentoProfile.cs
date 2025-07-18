using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Services.Profiles
{
    public class TipoDocumentoProfile : Profile
    {
        public TipoDocumentoProfile()
        {
            CreateMap<TipoDocumentoInfo, TipoDocumentoResponseDto>();
            CreateMap<TipoDocumento, TipoDocumentoResponseDto>();
            CreateMap<TipoDocumentoRequestDto, TipoDocumento>();
            CreateMap<TipoDocumento, TipoDocumentoInfo>();


        }
    }
}

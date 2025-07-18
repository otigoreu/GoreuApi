using AutoMapper;
using Goreu.Dto.Request;
using Goreu.DtoResponse;
using Goreu.Entities;
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

        }
    }
}

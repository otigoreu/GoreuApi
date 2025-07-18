using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Repositories.Interface;
using Goreu.Services.Interface;
using Microsoft.Extensions.Logging;

namespace Goreu.Services.Implementation
{
    public class HistorialService : ServiceBase<Historial, HistorialRequestDto, HistorialResponseDto>, IHistorialService
    {
        private readonly IHistorialRepository repository;

        public HistorialService(IHistorialRepository repository, ILogger<HistorialService> logger, IMapper mapper) : base(repository, logger, mapper)
        {
            this.repository = repository; // ✅ Asignación correcta
        }
    }
}

using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;
using Goreu.Repositories.Interface;
using Goreu.Services.Interface;
using Microsoft.Extensions.Logging;

namespace Goreu.Services.Implementation
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly ITipoDocumentoRepository repository;
        private readonly ILogger<TipoDocumentoService> logger;
        private readonly IMapper mapper;

        public TipoDocumentoService(ITipoDocumentoRepository repository, ILogger<TipoDocumentoService> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<BaseResponseGeneric<ICollection<TipoDocumentoResponseDto>>> GetAsync()
        {
            var response = new BaseResponseGeneric<ICollection<TipoDocumentoResponseDto>>();
            try
            {
                var data = await repository.GetAsync();
                response.Data = mapper.Map<ICollection<TipoDocumentoResponseDto>>(data);
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "OCurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<TipoDocumentoResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<TipoDocumentoResponseDto>();
            try
            {
                var data = await repository.GetAsync(id);
                response.Data = mapper.Map<TipoDocumentoResponseDto>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "ocurrio un problema al obtener el registro";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;

        }
        public async Task<BaseResponseGeneric<int>> AddAsync(TipoDocumentoRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                response.Data = await repository.AddAsync(mapper.Map<TipoDocumento>(request));
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "ocurrio un problema al agregar datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, TipoDocumentoRequestDto resquest)
        {
            var response = new BaseResponse();

            try
            {
                var data = await repository.GetAsync(id);
                if (data is null)
                {
                    response.ErrorMessage = $"el expediente con id {id} no fue encontrado";
                }
                mapper.Map(resquest, data);
                await repository.UpdateAsync();
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "ocrurrio un problema al actualizar datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();

            try
            {
                await repository.DeleteAsync(id);
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Cocurrio un problem al procesadr datos";
                logger.LogError(ex, "{ErrorMEssage} {MEssage}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> FinalizedAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.FinalizedAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al finalizar Datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> InitializedAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.InitializedAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al Inicializar Datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<TipoDocumentoInfo>>> GetAsync(string? descripcion)
        {
            var response = new BaseResponseGeneric<ICollection<TipoDocumentoInfo>>();
            try
            {

                response.Data = await repository.GetAsync(descripcion);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}

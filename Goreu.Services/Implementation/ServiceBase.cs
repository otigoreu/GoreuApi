namespace Goreu.Services.Implementation
{
    public class ServiceBase<TEntity, TRequest, TResponse> : IServiceBase<TRequest, TResponse> where TEntity : EntityBase
    {

        protected readonly IRepositoryBase<TEntity> repository;
        protected readonly ILogger logger;
        protected readonly IMapper mapper;

        protected ServiceBase(IRepositoryBase<TEntity> repository, ILogger logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public virtual async Task<BaseResponseGeneric<int>> AddAsync(TRequest request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var entity = mapper.Map<TEntity>(request);
                response.Data = await repository.AddAsync(entity);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al insertar la entidad.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public virtual async Task<BaseResponseGeneric<int>> UpdateAsync(int id, TRequest request)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                var entity = await repository.GetAsync(id);
                if (entity is null)
                {
                    response.ErrorMessage = $"La entidad con ID {id} no existe.";
                    return response;
                }

                mapper.Map(request, entity);
                await repository.UpdateAsync();

                response.Data = id; // ✅ devolvemos el ID actualizado
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al actualizar la entidad.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        //public virtual async Task<BaseResponse> UpdateAsync(int id, TRequest request)
        //{
        //    var response = new BaseResponse();
        //    try
        //    {
        //        var entity = await repository.GetAsync(id);
        //        if (entity is null)
        //        {
        //            response.ErrorMessage = $"La entidad con ID {id} no existe.";
        //            return response;
        //        }

        //        mapper.Map(request, entity);
        //        await repository.UpdateAsync();
        //        response.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ErrorMessage = "Error al actualizar la entidad.";
        //        logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
        //    }
        //    return response;
        //}

        public virtual async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al eliminar la entidad.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public virtual async Task<BaseResponse> FinalizeAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.FinalizeAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al finalizar la entidad.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public virtual async Task<BaseResponse> InitializeAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.InitializeAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al inicializar la entidad.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public virtual async Task<BaseResponseGeneric<TResponse>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<TResponse>();
            try
            {
                var entity = await repository.GetAsync(id);
                if (entity is null)
                {
                    response.ErrorMessage = $"La entidad con ID {id} no existe.";
                    return response;
                }

                response.Data = mapper.Map<TResponse>(entity);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener la entidad.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}

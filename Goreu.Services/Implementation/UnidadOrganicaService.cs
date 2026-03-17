using Goreu.Services.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Goreu.Services.Implementation
{
    public class UnidadOrganicaService : ServiceBase<UnidadOrganica, UnidadOrganicaRequestDto, UnidadOrganicaResponseDto>, IUnidadOrganicaService
    {
        //private readonly IUnidadOrganicaRepository repository;
        //private readonly ILogger<UnidadOrganicaService> logger;
        //private readonly IMapper mapper;

        //public UnidadOrganicaService(IUnidadOrganicaRepository repository, ILogger<UnidadOrganicaService> logger, IMapper mapper)
        //{
        //    this.repository = repository;
        //    this.logger = logger;
        //    this.mapper = mapper;
        //}

        private readonly IUnidadOrganicaRepository repository;

        public UnidadOrganicaService(IUnidadOrganicaRepository repository, ILogger<UnidadOrganicaService> logger, IMapper mapper) : base(repository, logger, mapper)
        {
            this.repository = repository; // ✅ Asignación correcta
        }

        public async Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>> GetAsync(string descripcion, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>();
            try
            {
                var data = await repository.GetAsync(
                    predicate: s => s.Descripcion.Contains(descripcion ?? string.Empty),
                    orderBy: x => x.Descripcion,
                    pagination);

                response.Data = mapper.Map<ICollection<UnidadOrganicaResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al filtrar las unidades organicas por descripción.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>> GetAsync(int idEntidad, string descripcion, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>();
            try
            {
                var data = await repository.GetAsync(
                    predicate: s => s.IdEntidad == idEntidad && s.Descripcion.Contains(descripcion ?? string.Empty),
                    orderBy: x => x.Descripcion,
                    pagination);

                response.Data = mapper.Map<ICollection<UnidadOrganicaResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al filtrar las unidades organicas por descripción.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseSingleDto>>> GetAsyncPerUser(string idUser)
        {
            var response = new BaseResponseGeneric<ICollection<UnidadOrganicaResponseSingleDto>>();

            try
            {
                var data = await repository.GetAsyncPerUser(idUser);
                response.Data = mapper.Map<ICollection<UnidadOrganicaResponseSingleDto>>(data);
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }


        /// <summary>
        /// Obtiene las unidades orgánicas descendientes en formato jerárquico.
        /// </summary>
        /// <param name="idUnidad">Identificador de la unidad padre.</param>
        /// <returns>Retorna las unidades hijas con estructura jerárquica.</returns>
        public async Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>> GetDescendientesJerarquicoAsync(int idUnidadOrganica)
        {
            var response = new BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>();

            try
            {
                var descendientes = await repository.ObtenerDescendientesAsync(idUnidadOrganica);

                // Mapeamos a DTOs planos
                var descendientesDto = mapper.Map<List<UnidadOrganicaResponseDto>>(descendientes);

                // Construimos la jerarquía
                var jerarquico = ConstruirJerarquia(descendientesDto, idUnidadOrganica);

                response.Data = jerarquico;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al construir la jerarquía de unidades orgánicas.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        /// <summary>
        /// Construye una estructura jerárquica a partir de una lista plana.
        /// </summary>
        private List<UnidadOrganicaResponseDto> ConstruirJerarquia(List<UnidadOrganicaResponseDto> lista, int? idPadre)
        {
            return lista
                .Where(x => x.idDependencia == idPadre)
                .Select(x => new UnidadOrganicaResponseDto
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Abrev = x.Abrev,
                    idEntidad = x.idEntidad,
                    idDependencia = x.idDependencia,
                    Hijos = ConstruirJerarquia(lista, x.Id) // 👈 Recursión para los hijos
                })
                .ToList();
        }

        public async Task<BaseResponseGeneric<bool>> ValidarDescripcionAsync(string descripcion, int idEntidad)
        {
            var response = new BaseResponseGeneric<bool>();

            try
            {
                var existe = await repository.ExistsAsync(x =>
                    x.Descripcion.ToLower() == descripcion.ToLower() &&
                    x.IdEntidad == idEntidad
                );

                response.Data = existe;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al validar la descripción de la unidad orgánica.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<List<UnidadOrganicaTreeResponseDto>>> SearchTreeAsync(int idEntidad, string descripcion)
        {
            var response = new BaseResponseGeneric<List<UnidadOrganicaTreeResponseDto>>();

            try
            {
                var flat = await repository.GetByEntidadAsync(idEntidad);

                var filteredTree = BuildFilteredTree(flat, descripcion);

                response.Data = mapper.Map<List<UnidadOrganicaTreeResponseDto>>(filteredTree);
                response.Success = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener el árbol filtrado");
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
            }

            return response;
        }

        public async Task<UnidadOrganicaDescendantsCountResponseDto> CountDescendantsAsync(int idEntidad, int id)
        {
            var flat = await repository.GetByEntidadAsync(idEntidad);

            var childrenLookup = flat
                .Where(x => x.IdDependencia != null)
                .GroupBy(x => x.IdDependencia!.Value)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Id).ToList());

            // 🔹 Hijos directos (nivel 1)
            int directChildren = childrenLookup.ContainsKey(id)
                ? childrenLookup[id].Count
                : 0;

            // 🔹 Total descendientes
            int total = 0;
            var stack = new Stack<int>();
            stack.Push(id);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (!childrenLookup.TryGetValue(current, out var children))
                    continue;

                foreach (var childId in children)
                {
                    total++;
                    stack.Push(childId);
                }
            }

            return new UnidadOrganicaDescendantsCountResponseDto
            {
                DirectChildren = directChildren,
                TotalDescendants = total
            };
        }

        private List<UnidadOrganicaTreeResponseDto> BuildTree(List<UnidadOrganica> flatList)
        {
            // 1️⃣ Convertimos lista plana a diccionario de nodos DTO
            var lookup = flatList.ToDictionary(
                x => x.Id,
                x => new UnidadOrganicaTreeResponseDto
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Abrev = x.Abrev,
                    IdEntidad = x.IdEntidad,
                    IdDependencia = x.IdDependencia
                });

            var roots = new List<UnidadOrganicaTreeResponseDto>();

            // 2️⃣ Construimos jerarquía
            foreach (var item in flatList)
            {
                var node = lookup[item.Id];

                if (item.IdDependencia == null)
                {
                    roots.Add(node);
                }
                else if (lookup.TryGetValue(item.IdDependencia.Value, out var parent))
                {
                    parent.Hijos.Add(node);
                }
            }

            return roots;
        }

        private List<UnidadOrganicaTreeResponseDto> BuildFilteredTree(List<UnidadOrganica> flatList, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                return BuildTree(flatList);

            var lookup = flatList.ToDictionary(
                x => x.Id,
                x => new UnidadOrganicaTreeResponseDto
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Abrev = x.Abrev,
                    IdEntidad = x.IdEntidad,
                    IdDependencia = x.IdDependencia
                });

            var idsToInclude = new HashSet<int>();

            // 1️⃣ Encontrar coincidencias
            var matches = flatList
                .Where(x => x.Descripcion.Contains(descripcion,
                          StringComparison.OrdinalIgnoreCase))
                .ToList();

            // 2️⃣ Subir por la jerarquía
            foreach (var match in matches)
            {
                var current = match;

                while (current != null)
                {
                    idsToInclude.Add(current.Id);

                    if (current.IdDependencia == null ||
                        !flatList.Any(x => x.Id == current.IdDependencia))
                        break;

                    current = flatList.First(x => x.Id == current.IdDependencia);
                }
            }

            // 3️⃣ Construir árbol solo con nodos necesarios
            var roots = new List<UnidadOrganicaTreeResponseDto>();

            foreach (var item in flatList.Where(x => idsToInclude.Contains(x.Id)))
            {
                var node = lookup[item.Id];

                if (item.IdDependencia == null ||
                    !idsToInclude.Contains(item.IdDependencia.Value))
                {
                    roots.Add(node);
                }
                else
                {
                    var parent = lookup[item.IdDependencia.Value];
                    parent.Hijos.Add(node);
                }
            }

            return roots;
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<BaseResponseGeneric<List<UnidadOrganicaTreeResponseDto>>> GetTreeByUnidadOrganicaAsync(int idEntidad, int idUnidadOrganica, TipoBusquedaArbol tipoBusqueda, string descripcion)
        {
            try
            {
                var flat = await repository.GetByEntidadAsync(idEntidad);

                var result = BuildFilteredTree(flat, idUnidadOrganica, tipoBusqueda, descripcion);

                return BaseResponseGeneric<List<UnidadOrganicaTreeResponseDto>>.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error en BuildTree");

                return BaseResponseGeneric<List<UnidadOrganicaTreeResponseDto>>
                    .Fail("Ocurrió un error al construir el árbol");
            }
        }

        private List<UnidadOrganicaTreeResponseDto> BuildFilteredTree(List<UnidadOrganica> flatList, int idUnidadOrganica, TipoBusquedaArbol tipoBusqueda, string descripcion)
        {
            var lookup = flatList.ToDictionary(
                x => x.Id,
                x => new UnidadOrganicaTreeResponseDto
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Abrev = x.Abrev,
                    IdEntidad = x.IdEntidad,
                    IdDependencia = x.IdDependencia
                });

            var idsToInclude = new HashSet<int>();

            var nodoBase = flatList.FirstOrDefault(x => x.Id == idUnidadOrganica);
            if (nodoBase == null)
                return new List<UnidadOrganicaTreeResponseDto>();

            // 🔹 Aplicar filtro por descripción si viene
            var candidatos = string.IsNullOrWhiteSpace(descripcion)
                ? flatList
                : flatList.Where(x => x.Descripcion.Contains(descripcion, StringComparison.OrdinalIgnoreCase)).ToList();

            switch (tipoBusqueda)
            {
                case TipoBusquedaArbol.PadreEHijos:
                    AgregarPadre(nodoBase, flatList, idsToInclude);
                    AgregarHijos(nodoBase.Id, flatList, idsToInclude, candidatos);
                    break;

                case TipoBusquedaArbol.PadreYHermanos:
                    AgregarPadre(nodoBase, flatList, idsToInclude);
                    AgregarHermanos(nodoBase, flatList, idsToInclude, candidatos);
                    break;
            }

            return ConstruirArbol(idsToInclude, flatList, lookup);
        }

        private void AgregarPadre(UnidadOrganica nodo, List<UnidadOrganica> flatList, HashSet<int> ids)
        {
            var current = nodo;

            while (current != null)
            {
                ids.Add(current.Id);

                if (current.IdDependencia == null)
                    break;

                current = flatList.FirstOrDefault(x => x.Id == current.IdDependencia);
            }
        }

        private void AgregarHijos(int parentId, List<UnidadOrganica> flatList, HashSet<int> ids, List<UnidadOrganica> filtro)
        {
            var hijos = flatList.Where(x => x.IdDependencia == parentId).ToList();

            foreach (var hijo in hijos)
            {
                if (filtro.Contains(hijo))
                {
                    ids.Add(hijo.Id);
                }

                //AgregarHijos(hijo.Id, flatList, ids, filtro);
            }
        }

        private void AgregarHermanos(UnidadOrganica nodo, List<UnidadOrganica> flatList, HashSet<int> ids, List<UnidadOrganica> filtro)
        {
            if (nodo.IdDependencia == null)
                return;

            var hermanos = flatList
                .Where(x => x.IdDependencia == nodo.IdDependencia)
                .ToList();

            foreach (var hermano in hermanos)
            {
                if (filtro.Contains(hermano))
                {
                    ids.Add(hermano.Id);
                }
            }
        }

        private List<UnidadOrganicaTreeResponseDto> ConstruirArbol(HashSet<int> idsToInclude, List<UnidadOrganica> flatList, Dictionary<int, UnidadOrganicaTreeResponseDto> lookup)
        {
            var roots = new List<UnidadOrganicaTreeResponseDto>();

            foreach (var item in flatList.Where(x => idsToInclude.Contains(x.Id)))
            {
                var node = lookup[item.Id];

                if (item.IdDependencia == null ||
                    !idsToInclude.Contains(item.IdDependencia.Value))
                {
                    roots.Add(node);
                }
                else
                {
                    var parent = lookup[item.IdDependencia.Value];
                    parent.Hijos.Add(node);
                }
            }

            return roots;
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------

        //public async Task EliminarAsync(int idEntidad, int id)
        //{
        //    var unidad = await repository.GetAsync(id);

        //    if (unidad == null)
        //        throw new Exception("Unidad no encontrada");

        //    // Traemos todas para evitar múltiples queries
        //    var todas = await repository.GetByEntidadAsync(idEntidad);

        //    await ProcesarEliminacionRecursiva(unidad, todas);

        //    await repository.SaveChangesAsync();
        //}

        //private async Task ProcesarEliminacionRecursiva(
        //    UnidadOrganica unidad,
        //    List<UnidadOrganica> todas)
        //{
        //    bool estaRelacionada = await repository.EstaRelacionadaAsync(unidad.Id);

        //    if (estaRelacionada)
        //    {
        //        // 🔹 Baja lógica
        //        if (unidad.Estado)
        //        {
        //            unidad.Estado = false;

        //            if (!unidad.Descripcion.EndsWith(" - BAJA"))
        //                unidad.Descripcion += " - BAJA";

        //            await repository.UpdateAsync(unidad);
        //        }

        //        return;
        //    }

        //    // 🔹 Buscar hijos en memoria
        //    var hijos = todas
        //        .Where(x => x.IdDependencia == unidad.Id)
        //        .ToList();

        //    foreach (var hijo in hijos)
        //    {
        //        await ProcesarEliminacionRecursiva(hijo, todas);
        //    }

        //    // 🔹 Eliminación física
        //    repository.Remove(unidad);
        //}


    }
}
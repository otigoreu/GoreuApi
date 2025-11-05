using Microsoft.EntityFrameworkCore;

namespace Goreu.Repositories.Implementation
{
    public class UnidadOrganicaRepository : RepositoryBase<UnidadOrganica>, IUnidadOrganicaRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UnidadOrganicaRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ICollection<UnidadOrganica>> GetAsync<TKey>(Expression<Func<UnidadOrganica, bool>> predicate, Expression<Func<UnidadOrganica, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Set<UnidadOrganica>()
                .Include(x => x.Entidad)
                .Include(x => x.Dependencia)
                .Include(x => x.Hijos)
                .Include(z => z.UsuarioUnidadOrganicas.Where(ea => ea.Estado))

                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .AsQueryable();

            await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
            var response = await queryable.Paginate(pagination).ToListAsync();

            return response;
        }

        public async Task<ICollection<UnidadOrganicaInfo>> GetAsyncPerUser(string idUser)
        {
            //var query = context.Set<UnidadOrganicaInfo>().FromSqlRaw("UnidadOrganicaPerIdUser {0}", idUser);
            var query = context.Set<UnidadOrganicaInfo>().FromSqlRaw(
                @"select uo.Id, uo.Abrev, uo.Descripcion,uo.IdEntidad,uo.IdDependencia,uo.Estado
                from adm.UnidadOrganica uo join adm.UsuarioUnidadOrganica uuo 
                on uuo.IdUnidadOrganica= uo.Id join adm.Usuario u on u.Id=uuo.IdUsuario where u.Id={0}",idUser);


            return await query.ToListAsync();
        }

        /// <summary>
        /// Obtiene todos los descendientes (hijos, nietos, etc.) de una unidad orgánica dada.
        /// </summary>
        public async Task<ICollection<UnidadOrganica>> ObtenerDescendientesAsync(int idUnidadOrganica)
        {
            // 🔹 Traemos todas las unidades (más eficiente que incluir recursivamente)
            var todas = await context.Set<UnidadOrganica>().AsNoTracking().ToListAsync();

            return ObtenerHijosRecursivo(idUnidadOrganica, todas);
        }

        /// <summary>
        /// Función auxiliar recursiva para obtener los hijos.
        /// </summary>
        private List<UnidadOrganica> ObtenerHijosRecursivo(int idPadre, List<UnidadOrganica> todas)
        {
            var hijos = todas.Where(u => u.IdDependencia == idPadre).ToList();
            var resultado = new List<UnidadOrganica>(hijos);

            foreach (var hijo in hijos)
            {
                resultado.AddRange(ObtenerHijosRecursivo(hijo.Id, todas));
            }

            return resultado;
        }
    }
}

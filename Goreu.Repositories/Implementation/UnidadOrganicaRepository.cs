using Goreu.Dto.Request;
using Goreu.Entities;
using Goreu.Entities.Info;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
                from Administrador.UnidadOrganica uo join Administrador.UsuarioUnidadOrganica uuo 
                on uuo.IdUnidadOrganica= uo.Id join Administrador.Usuario u on u.Id=uuo.IdUsuario where u.Id={0}",idUser);


            return await query.ToListAsync();
        }
    }
}

﻿using Goreu.Dto.Request;
using Goreu.Entities;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Goreu.Repositories.Implementation
{
    public class EntidadAplicacionRepository : RepositoryBase<EntidadAplicacion>, IEntidadAplicacionRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public EntidadAplicacionRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ICollection<EntidadAplicacion>> GetAsync<TKey>(Expression<Func<EntidadAplicacion, bool>> predicate, Expression<Func<EntidadAplicacion, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Set<EntidadAplicacion>()

                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .AsQueryable();

            await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
            var response = await queryable.Paginate(pagination).ToListAsync();

            return response;
        }

        public async Task<EntidadAplicacion> GetAsync(int idEntidad, int idAplicacion)
        {
            return context.Set<EntidadAplicacion>()
               .FirstOrDefault(z => z.IdEntidad == idEntidad && z.IdAplicacion == idAplicacion);
        }
    }
}

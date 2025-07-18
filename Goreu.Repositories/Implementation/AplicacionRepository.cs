﻿using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Http;

namespace Goreu.Repositories.Implementation
{
    public class AplicacionRepository : RepositoryBase<Aplicacion>, IAplicacionRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AplicacionRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ICollection<Aplicacion>> GetAsync<TKey>(Expression<Func<Aplicacion, bool>> predicate, Expression<Func<Aplicacion, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Set<Aplicacion>()

                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .AsQueryable();

            await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
            var response = await queryable.Paginate(pagination).ToListAsync();

            return response;
        }


        public async Task<ICollection<AplicacionInfo>> GetAsyncPerUser(string idUser)
        {
            
            var query = context.Set<AplicacionInfo>().FromSqlRaw(
                @"select a.Id, a.Descripcion, a.Estado from Administrador.Usuario u 
                join Administrador.UsuarioRol ur on u.Id=ur.UserId 
                join Administrador.Rol r on r.Id=ur.RoleId join Administrador.EntidadAplicacionRol ear on ear.IdRol=r.Id
                join Administrador.EntidadAplicacion ea on ea.id=ear.idEntidadAplicacion join Administrador.Aplicacion a on a.Id=ea.IdAplicacion 
                where u.Id={0}",idUser);

            return await query.ToListAsync();
        }
    }
}

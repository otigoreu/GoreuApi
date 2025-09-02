using Goreu.Dto.Request;
using Goreu.Entities;
using Goreu.Entities.Info;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Repositories.Implementation
{
    public class PersonaRepository : RepositoryBase<Persona>, IPersonaRepository
    {
        private readonly IHttpContextAccessor httpContext;

        public PersonaRepository(ApplicationDbContext context, IHttpContextAccessor httpContext) : base(context)
        {
            this.httpContext = httpContext;
        }


        public async Task<ICollection<Persona>> GetAsync(string? search, PaginationDto? pagination)
        {
            var queryable = context.Set<Persona>()
                .AsNoTracking();

            if (!string.IsNullOrEmpty(search))
            {
                queryable = queryable.Where(x =>
                    x.Nombres.Contains(search) ||
                    x.ApellidoPat.Contains(search) ||
                    x.ApellidoMat.Contains(search));
            }

            if (pagination is not null)
            {
                await httpContext.HttpContext.InsertarPaginacionHeader(queryable);
                queryable = queryable.Paginate(pagination);
            }

            return await queryable.ToListAsync();
        }

        //public async Task<ICollection<Persona>> GetAsync(string? search, PaginationDto? pagination)
        //{
        //    //eager loading optimizado
        //    var queryable = context.Set<Persona>()
        //        .Where(x => $"{x.Nombres} {x.ApellidoPat} {x.ApellidoMat}".Contains(search ?? string.Empty))
        //        .AsNoTracking()
        //        .AsQueryable();

        //    if (pagination is not null)
        //    {
        //        await httpContext.HttpContext.InsertarPaginacionHeader(queryable);
        //        queryable = queryable.Paginate(pagination);
        //    }

        //    var response = await queryable.ToListAsync();
        //    return response;
        //}
        
        public async Task<ICollection<PersonaInfo>> GetAsyncEmail(string? email, PaginationDto pagination)
        {
            //eager loading optimizado
            var queryable = context.Set<Persona>()
                .Where(x => x.Email.Contains(email ?? string.Empty))
                .AsNoTracking()
                .Select(x => new PersonaInfo
                {
                    Id = x.Id,
                    nombres = x.Nombres,
                    apellidoPat = x.ApellidoPat,
                    apellidoMat = x.ApellidoMat,
                    fechaNac = x.FechaNac,
                    email = x.Email,
                    idTipoDoc = x.IdTipoDoc,
                    nroDoc = x.NroDoc,
                    estado = x.Estado

                }).AsQueryable();

            await httpContext.HttpContext.InsertarPaginacionHeader(queryable);
            return await queryable.OrderBy(x => x.Id).Paginate(pagination).ToListAsync();

        }
        
        public async Task FinalizedAsync(int id)
        {
            var person = await GetAsync(id);
            if (person is not null)
            {
                person.Estado = false;
                await UpdateAsync();
            }
        }

        public async Task InitializedAsync(int id)
        {
            var person = await context.Set<Persona>().IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (person is not null)
            {
                person.Estado= true;
                context.Set<Persona>().Update(person);
                await context.SaveChangesAsync();

            }
        }

        public async Task<Persona> GetAsyncNumdoc(string numdoc)
        {
            var persona = await context.Set<Persona>()
               .AsNoTracking()
               .Where(x => x.NroDoc == numdoc)
               .FirstOrDefaultAsync();

            if (persona == null) return persona;

            context.Set<Persona>().Entry(persona).State = EntityState.Detached;
            return persona;
        }
    }
}

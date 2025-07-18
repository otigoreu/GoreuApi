using Goreu.Dto.Request;
using Goreu.Entities;
using Goreu.Entities.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Repositories.Interface
{
    public interface IPersonaRepository : IRepositoryBase<Persona>
    {
        Task<ICollection<PersonaInfo>> GetAsync(string? nombres, PaginationDto pagination);
        Task<ICollection<PersonaInfo>> GetAsyncfilter(string? nombres, PaginationDto pagination);

        Task<ICollection<PersonaInfo>> GetAsyncEmail(string? email, PaginationDto pagination);
        Task<Persona> GetAsyncNumdoc(string numdoc);

        Task FinalizedAsync(int id);
        Task InitializedAsync(int id);
    }
}
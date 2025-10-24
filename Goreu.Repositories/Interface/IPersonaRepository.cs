using Goreu.Dto.Response;

namespace Goreu.Repositories.Interface
{
    public interface IPersonaRepository : IRepositoryBase<Persona>
    {
        Task<ICollection<Persona>> GetAsync(string? search, PaginationDto? pagination);
        //Task<ICollection<PersonaInfo>> GetAsyncEmail(string? email, PaginationDto pagination);
        Task<Persona> GetAsyncNumdoc(string numdoc);
        Task FinalizedAsync(int id);
        Task InitializedAsync(int id);
    }
}
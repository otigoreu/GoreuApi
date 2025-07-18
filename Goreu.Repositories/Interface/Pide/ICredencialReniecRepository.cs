using Goreu.Entities.Info;
using Goreu.Entities.Pide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Repositories.Interface.Pide
{
    public interface ICredencialReniecRepository : IRepositoryBase<CredencialReniec>
    {
        Task<ICollection<CredencialReniecInfo>> GetAsync(string? descripcion);
        Task<CredencialReniecInfo> GetByNumdocAsync(string nuDniUsuario);
        Task FinalizedAsync(int id);
        Task InitializedAsync(int id);
    }
}

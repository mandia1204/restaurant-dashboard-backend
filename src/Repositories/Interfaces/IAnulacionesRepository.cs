using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Repositories.Interfaces
{
    public interface IAnulacionesRepository
    {
        Task<IEnumerable<Anulacion>> GetAsync();
    }
}
using System.Threading.Tasks;
using Models;

namespace Repositories.Interfaces
{
    public interface ICardRepository
    {
        Task<Card<V>> GetAsync<V>(string cardName);
        Task<Card<V1,V2>> GetAsync<V1, V2>(string cardName);
    }
}
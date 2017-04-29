using Models;
using System.Data.SqlClient;

namespace Repositories.Mappers {
    public interface IReaderToCard
    {
        Card Map<T>(SqlDataReader r);
    }
}
using Models;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repositories.Mappers {
    public interface IReaderToChart
    {
        Chart Map(SqlDataReader r, string chartName);
    }
}
using Models;
using System.Data.SqlClient;

namespace Repositories.Mappers {
    public interface IReaderToChart
    {
        Chart Map(SqlDataReader r, string chartName, string title);
    }
}
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Models;
using Repositories.Helpers;

namespace Repositories.Interfaces{
    public interface IDashboardReader{
        Dictionary<string, Task<SqlDataReader>> GetReaders(AdoHelper helper, DashboardParameters pars);
    }
}
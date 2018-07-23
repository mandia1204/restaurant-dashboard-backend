using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class AnulacionesRepository : IAnulacionesRepository
    {
        private readonly string connectionString;
        public AnulacionesRepository(IOptions<DatabaseSettings> dbSettings) {
            this.connectionString = dbSettings.Value.ConnectionString;
        }
        public async Task<IEnumerable<Anulacion>> GetAsync()
        {
            using(var connection = new SqlConnection(this.connectionString)){
                await connection.OpenAsync();
                return await connection.QueryAsync<Anulacion>("USP_DASHBOARD_ANULACIONES");
            }
        }
    }
}
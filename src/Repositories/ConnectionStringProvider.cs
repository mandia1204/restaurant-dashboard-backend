using Microsoft.Extensions.Options;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly DatabaseSettings dbSettings;
        private readonly Secrets secrets;
        
        public ConnectionStringProvider(IOptions<DatabaseSettings> dbSettings, IOptions<Secrets> secrets) {
            this.dbSettings = dbSettings.Value;
            this.secrets = secrets.Value;
        }

        public string GetConnectionString() {
            return $"Data Source={dbSettings.DataSource}; Initial Catalog={dbSettings.InitialCatalog}; User id={secrets.UserId}; Password={secrets.Password};MultipleActiveResultSets=True;";
        }
    }
}
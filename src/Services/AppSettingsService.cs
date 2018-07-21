using Models;
using Microsoft.Extensions.Options;

namespace Services
{   
    public class AppSettingsService : IAppSettingsService
    {
        private DatabaseSettings dbSettings;
        public AppSettingsService(IOptions<DatabaseSettings> dbSettings)
        {
           this.dbSettings = dbSettings.Value;
        }

        public DatabaseSettings GetDatabaseSettings()
        {
            return dbSettings;
        }
    }
}
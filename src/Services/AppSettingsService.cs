using Models;
using Microsoft.Extensions.Options;

namespace Services
{   
    public class AppSettingsService : IAppSettingsService
    {
        private DatabaseSettings _dbSettings;
        public AppSettingsService(IOptions<DatabaseSettings> dbSettings)
        {
           _dbSettings = dbSettings.Value;
        }

        public DatabaseSettings GetDatabaseSettings()
        {
            return _dbSettings;
        }
    }
}
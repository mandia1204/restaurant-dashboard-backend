using Models;

namespace Services
{
    public interface IAppSettingsService
    {
        DatabaseSettings GetDatabaseSettings();
    }
}
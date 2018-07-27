using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Repositories.Interfaces
{
    public interface IChartRepository
    {
        Task<IEnumerable<ChartRow<K,V>>> GetAsync<K,V>(string chartName, DashboardParameters pars);
        Task<IEnumerable<ChartRow<K,V>>> GetWithGroupAsync<K,V>(string chartName, DashboardParameters pars);
    }
}
using System.Threading.Tasks;
using Models;

namespace Repositories{
    public interface IDashboardRepository{
        Task<Dashboard> GetDashboardAsync();
    }
}
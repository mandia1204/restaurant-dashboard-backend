using System.Threading.Tasks;
using Models;

namespace Services{
    public interface IDashboardService{
        Task<Dashboard> GetDashboardAsync();
    }
}
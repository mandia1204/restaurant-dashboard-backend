using System.Threading.Tasks;
using Models;
using Models.Dto;

namespace Services.Interfaces {
    public interface IDashboardService{
        Task<DashboardDto> GetDashboardAsync(DashboardParameters pars);
    }
}
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services{
    public class DashboardService: IDashboardService {
        
        private IDashboardRepository repository;

        public DashboardService(IDashboardRepository repository){
            this.repository = repository;
        }
        public Task<Dashboard> GetDashboardAsync(DashboardParameters pars){
            return repository.GetDashboardAsync(pars);
        }
    }
}
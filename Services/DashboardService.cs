using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services{
    public class DashboardService: IDashboardService {
        
        private IDashboardRepository _repository;

        public DashboardService(IDashboardRepository repository){
            _repository = repository;
        }
        public Task<Dashboard> GetDashboardAsync(){
            return _repository.GetDashboardAsync();
        }
    }
}
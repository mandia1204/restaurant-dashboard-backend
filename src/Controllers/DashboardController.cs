using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace restaurant_dashboard_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private IDashboardService _service;

        public DashboardController(IDashboardService service){
            _service = service;
        }

        // GET api/dashboard
        [HttpGet]
        public Task<Dashboard> Get()
        {
            return _service.GetDashboardAsync();
        }
    }
}
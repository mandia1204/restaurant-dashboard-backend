using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace restaurant_dashboard_backend.Controllers
{
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

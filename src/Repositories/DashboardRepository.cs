using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Services;
using Repositories.Mappers;
using Repositories.Helpers;

namespace Repositories {
    public class DashboardRepository : IDashboardRepository{

        private DatabaseSettings _dbSettings;
        private IReaderToChart _readerToChart;
        public DashboardRepository(IAppSettingsService appSettings, IReaderToChart readerToChart){
            _dbSettings = appSettings.GetDatabaseSettings();
            _readerToChart = readerToChart;
        }

        public async Task<Dashboard> GetDashboardAsync(){
            
            var dashboard = new Dashboard { charts = new List<Chart>() };
            using(var helper = new AdoHelper(_dbSettings)){
                using(var reader = await helper.ExecDataReaderAsync("USP_DASHBOARD_VENTA_ANUAL")){
                    dashboard.charts.Add(_readerToChart.Map(reader, "VENTAS", "Ventas Anuales"));
                }
            }

            return dashboard;
        }
    }
}
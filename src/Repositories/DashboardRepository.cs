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
        private IReaderToCard _readerToCard;
        public DashboardRepository(IAppSettingsService appSettings, IReaderToChart readerToChart, IReaderToCard readerToCard){
            _dbSettings = appSettings.GetDatabaseSettings();
            _readerToChart = readerToChart;
            _readerToCard = readerToCard;
        }

        public async Task<Dashboard> GetDashboardAsync(){
            
            var dashboard = new Dashboard { charts = new List<Chart>(), cards = new Dictionary<string, Card>()};
            using(var helper = new AdoHelper(_dbSettings)){
                var P1 = helper.ExecDataReaderAsync("USP_DASHBOARD_PRODUCCION_DEL_DIA");
                var P2 = helper.ExecDataReaderAsync("USP_DASHBOARD_VENTA_ANUAL");
                var P3 = helper.ExecDataReaderAsync("USP_DASHBOARD_VENTA_DEL_DIA");
                var P4 = helper.ExecDataReaderAsync("USP_DASHBOARD_PAX_DEL_DIA");

                var readers = await Task.WhenAll(P1, P2, P3, P4);

                dashboard.cards.Add("PRODUCCION_DIA", _readerToCard.Map<double>(readers[0]));
                dashboard.charts.Add(_readerToChart.Map(readers[1], "VENTAS", "Ventas Anuales"));
                dashboard.cards.Add("VENTA_DIA", _readerToCard.Map<double>(readers[2]));
                dashboard.cards.Add("PAX_DIA",_readerToCard.Map<int>(readers[3]));
            }

            return dashboard;
        }
    }
}
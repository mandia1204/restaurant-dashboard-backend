using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Services;
using Repositories.Mappers;
using Repositories.Helpers;

namespace Repositories {
    public class DashboardRepository : IDashboardRepository{
        private DatabaseSettings _dbSettings;
        private IChartMapper _chartMapper;
        private ICardMapper _cardMapper;
        private ITicketPromedioCardMapper _ticketPromedioCardMapper;
        private IProduccionCardMapper _produccionCardMapper;
        private IAnulacionMapper _anulacionMapper;
        public DashboardRepository(IAppSettingsService appSettings, 
        IChartMapper chartMapper, ICardMapper cardMapper
        , ITicketPromedioCardMapper ticketPromedioCardMapper
        , IProduccionCardMapper produccionCardMapper, IAnulacionMapper anulacionMapper){
            _dbSettings = appSettings.GetDatabaseSettings();
            _chartMapper = chartMapper;
            _cardMapper = cardMapper;
            _ticketPromedioCardMapper = ticketPromedioCardMapper;
            _produccionCardMapper = produccionCardMapper;
            _anulacionMapper = anulacionMapper;
        }

        public async Task<Dashboard> GetDashboardAsync(){
            
            var dashboard = new Dashboard { charts = new List<Chart>(), cards = new Dictionary<string, Card>()};
            using(var helper = new AdoHelper(_dbSettings)){
                var P1 = helper.ExecDataReaderAsync("USP_DASHBOARD_PRODUCCION_DEL_DIA");
                var P2 = helper.ExecDataReaderAsync("USP_DASHBOARD_VENTA_ANUAL");
                var P3 = helper.ExecDataReaderAsync("USP_DASHBOARD_VENTA_DEL_DIA");
                var P4 = helper.ExecDataReaderAsync("USP_DASHBOARD_PAX_DEL_DIA");
                var P5 = helper.ExecDataReaderAsync("USP_DASHBOARD_ANULACIONES");

                var readers = await Task.WhenAll(P1, P2, P3, P4, P5);

                dashboard.charts.Add(_chartMapper.Map(readers[1], "VENTAS", "Ventas Anuales"));
                dashboard.cards.Add("PRODUCCION_DIA", _produccionCardMapper.Map(readers[0]));
                dashboard.cards.Add("VENTA_DIA", _cardMapper.Map<double>(readers[2]));
                dashboard.cards.Add("PAX_DIA",_cardMapper.Map<int>(readers[3]));
                dashboard.cards.Add("TICKET_PROMEDIO_DIA",_ticketPromedioCardMapper.Map((ProduccionCard)dashboard.cards["PRODUCCION_DIA"], dashboard.cards["PAX_DIA"]));
                dashboard.anulaciones = _anulacionMapper.Map(readers[4]);
            }

            return dashboard;
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Services;
using Repositories.Mappers;
using Repositories.Helpers;
using System;
using Util;
using Repositories.Interfaces;

namespace Repositories {
    public class DashboardRepository : IDashboardRepository{
        private readonly DatabaseSettings _dbSettings;
        private readonly IChartMapper _chartMapper;
        private readonly ICardMapper _cardMapper;
        private readonly ITicketPromedioCardMapper _ticketPromedioCardMapper;
        private readonly IProduccionCardMapper _produccionCardMapper;
        private readonly IAnulacionMapper _anulacionMapper;
        private readonly IDashboardReader _dashboardReader;
        public DashboardRepository(IAppSettingsService appSettings, 
        IChartMapper chartMapper, ICardMapper cardMapper
        , ITicketPromedioCardMapper ticketPromedioCardMapper
        , IProduccionCardMapper produccionCardMapper, IAnulacionMapper anulacionMapper, IDashboardReader dashboardReader){
            _dbSettings = appSettings.GetDatabaseSettings();
            _chartMapper = chartMapper;
            _cardMapper = cardMapper;
            _ticketPromedioCardMapper = ticketPromedioCardMapper;
            _produccionCardMapper = produccionCardMapper;
            _anulacionMapper = anulacionMapper;
            _dashboardReader = dashboardReader;
        }
        public async Task<Dashboard> GetDashboardAsync(DashboardParameters pars) {
            var dashboard = new Dashboard();
            using(var helper = new AdoHelper(_dbSettings)){
                var readerDictionary = _dashboardReader.GetReaders(helper, pars);
               
                await Task.WhenAll(readerDictionary.Values);

                if(readerDictionary.ContainsKey(Ops.VentaAnual)) {
                    dashboard.charts.Add(_chartMapper.Map(readerDictionary[Ops.VentaAnual].Result, "VENTAS_ANUALES", new List<string>{pars.anio.ToString()}));
                }
                if(readerDictionary.ContainsKey(Ops.AnulacionesMes)) {
                    dashboard.charts.Add(_anulacionMapper.MapMensual(readerDictionary[Ops.AnulacionesMes].Result, "ANULACIONES_DEL_MES", pars.mes));
                }
                if(readerDictionary.ContainsKey(Ops.ProduccionDia)) {
                    dashboard.cards.Add("PRODUCCION_DIA", _produccionCardMapper.Map(readerDictionary[Ops.ProduccionDia].Result));
                }
                if(readerDictionary.ContainsKey(Ops.VentaDia)) {
                    dashboard.cards.Add("VENTA_DIA", _cardMapper.Map<double>(readerDictionary[Ops.VentaDia].Result));
                }
                if(readerDictionary.ContainsKey(Ops.PaxDia)) {
                    dashboard.cards.Add("PAX_DIA", _cardMapper.Map<int>(readerDictionary[Ops.PaxDia].Result));
                }
                if(readerDictionary.ContainsKey(Ops.ProduccionDia) && readerDictionary.ContainsKey(Ops.PaxDia)){
                    dashboard.cards.Add("TICKET_PROMEDIO_DIA", _ticketPromedioCardMapper.Map((ProduccionCard)dashboard.cards["PRODUCCION_DIA"], dashboard.cards["PAX_DIA"]));
                }
                if(readerDictionary.ContainsKey(Ops.Anulaciones)) {
                    dashboard.anulaciones = _anulacionMapper.Map(readerDictionary[Ops.Anulaciones].Result);
                }
            }

            return dashboard;
        }
    }
}
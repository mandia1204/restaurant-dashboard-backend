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
        private readonly DatabaseSettings dbSettings;
        private readonly IChartMapper chartMapper;
        private readonly ICardMapper cardMapper;
        private readonly ITicketPromedioCardMapper ticketPromedioCardMapper;
        private readonly IProduccionCardMapper produccionCardMapper;
        private readonly IAnulacionMapper anulacionMapper;
        private readonly IDashboardReader dashboardReader;
        public DashboardRepository(IAppSettingsService appSettings, 
        IChartMapper chartMapper, ICardMapper cardMapper
        , ITicketPromedioCardMapper ticketPromedioCardMapper
        , IProduccionCardMapper produccionCardMapper, IAnulacionMapper anulacionMapper, IDashboardReader dashboardReader){
            dbSettings = appSettings.GetDatabaseSettings();
            this.chartMapper = chartMapper;
            this.cardMapper = cardMapper;
            this.ticketPromedioCardMapper = ticketPromedioCardMapper;
            this.produccionCardMapper = produccionCardMapper;
            this.anulacionMapper = anulacionMapper;
            this.dashboardReader = dashboardReader;
        }
        public async Task<Dashboard> GetDashboardAsync(DashboardParameters pars) {
            var dashboard = new Dashboard();
            using(var helper = new AdoHelper(dbSettings)){
                var readerDictionary = dashboardReader.GetReaders(helper, pars);
               
                await Task.WhenAll(readerDictionary.Values);

                if(readerDictionary.ContainsKey(Ops.VentaAnual)) {
                    var chart = chartMapper.Map<int>(readerDictionary[Ops.VentaAnual].Result, "VENTAS_ANUALES", new List<string>{pars.anio.ToString()}, Constants.Meses);
                    dashboard.charts.Add(chart);
                }
                if(readerDictionary.ContainsKey(Ops.AnulacionesMes)) {
                    dashboard.charts.Add(anulacionMapper.MapMensual(readerDictionary[Ops.AnulacionesMes].Result, "ANULACIONES_DEL_MES", pars.mes));
                }
                if(readerDictionary.ContainsKey(Ops.ProductosVendidosMes)) {
                    var chart = chartMapper.Map<string>(readerDictionary[Ops.ProductosVendidosMes].Result, "PRODUCTOS_VENDIDOS_DEL_MES", new List<string>{pars.mes.ToString()}, Constants.TiposDeProducto);
                    dashboard.charts.Add(chart);
                }
                if(readerDictionary.ContainsKey(Ops.ProduccionDia)) {
                    dashboard.cards.Add("PRODUCCION_DIA", produccionCardMapper.Map(readerDictionary[Ops.ProduccionDia].Result));
                }
                if(readerDictionary.ContainsKey(Ops.VentaDia)) {
                    dashboard.cards.Add("VENTA_DIA", cardMapper.Map<double>(readerDictionary[Ops.VentaDia].Result));
                }
                if(readerDictionary.ContainsKey(Ops.PaxDia)) {
                    dashboard.cards.Add("PAX_DIA", cardMapper.Map<int>(readerDictionary[Ops.PaxDia].Result));
                }
                if(readerDictionary.ContainsKey(Ops.ProduccionDia) && readerDictionary.ContainsKey(Ops.PaxDia)){
                    dashboard.cards.Add("TICKET_PROMEDIO_DIA", ticketPromedioCardMapper.Map((ProduccionCard)dashboard.cards["PRODUCCION_DIA"], dashboard.cards["PAX_DIA"]));
                }
                if(readerDictionary.ContainsKey(Ops.Anulaciones)) {
                    dashboard.anulaciones = anulacionMapper.Map(readerDictionary[Ops.Anulaciones].Result);
                }
            }

            return dashboard;
        }
    }
}
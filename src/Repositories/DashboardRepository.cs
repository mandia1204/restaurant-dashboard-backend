using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Services;
using Repositories.Mappers;
using Repositories.Helpers;
using System.Data;
using System.Linq;
using System;
using System.Data.SqlClient;
using Util;

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

        private Dictionary<string, Func<Task<SqlDataReader>>> GetDataFunctions(AdoHelper helper, DashboardParameters pars) {
            var funcs = new Dictionary<string, Func<Task<SqlDataReader>>>();

            funcs.Add(Ops.ProduccionDia, () => {
                return helper.ExecDataReaderAsync("USP_DASHBOARD_PRODUCCION_DEL_DIA");
            });
            funcs.Add(Ops.VentaAnual, () => {
                return helper.ExecDataReaderAsync("USP_DASHBOARD_VENTA_ANUAL", CommandType.StoredProcedure, "@YEAR", pars.anio);
            });
            funcs.Add(Ops.VentaDia, () => {
                return helper.ExecDataReaderAsync("USP_DASHBOARD_VENTA_DEL_DIA");
            });
            funcs.Add(Ops.PaxDia, () => {
                return helper.ExecDataReaderAsync("USP_DASHBOARD_PAX_DEL_DIA");
            });
            funcs.Add(Ops.Anulaciones, () => {
                return helper.ExecDataReaderAsync("USP_DASHBOARD_ANULACIONES");
            });
            funcs.Add(Ops.AnulacionesMes, () => {
                return helper.ExecDataReaderAsync("USP_DASHBOARD_ANULACIONES_DEL_MES", CommandType.StoredProcedure, "@YEAR", pars.anio, "@MONTH", pars.mes);
            });
            return funcs;
        }
        public async Task<Dashboard> GetDashboardAsync(DashboardParameters pars) {

            var queries = pars.ops.Split(','); //"PDD,VA,VDD,PXD,ANL,ANM"
            var dashboard = new Dashboard { charts = new List<Chart>(), cards = new Dictionary<string, Card>(), anulaciones = new List<Anulacion>()};
            using(var helper = new AdoHelper(_dbSettings)){
                var dataFuncs = GetDataFunctions(helper, pars);
                var readerTaskList = new List<Task<SqlDataReader>>();
                var readerDictionary = new Dictionary<string,Task<SqlDataReader>>();
                foreach(var query in queries) {
                    readerDictionary.Add(query, dataFuncs[query]());
                }
               
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
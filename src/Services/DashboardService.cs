using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Builders;
using Models;
using Models.Dto;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;
using Util;

namespace Services{
    public class DashboardService: IDashboardService {
        private readonly IAnulacionesRepository anulacionesRepository;
        private readonly IChartRepository chartRepository;
        private readonly ICardRepository cardRepository;
        private readonly IDashboardBuilder dashboardBuilder;
        public DashboardService(IAnulacionesRepository anulacionesRepository, IChartRepository chartRepository, ICardRepository cardRepository, IDashboardBuilder dashboardBuilder){
            this.anulacionesRepository = anulacionesRepository;
            this.chartRepository = chartRepository;
            this.dashboardBuilder = dashboardBuilder;
            this.cardRepository = cardRepository;
        }
        public async Task<DashboardDto> GetDashboardAsync(DashboardParameters pars) {
            var ops = pars.ParseOps();
            Task<IEnumerable<Anulacion>> anulacionesTask = null;
            Task<IEnumerable<ChartRow<string, double>>> ventasProductosMesTask = null;
            Task<IEnumerable<ChartRow<string, int>>> platosVendidosMesTask = null;
            Task<IEnumerable<ChartRow<int, double>>> ventasAnualesTask = null;
            Task<IEnumerable<ChartRow<string, int>>> anulacionesDelMesTask = null;
            Task<Card<int>> paxDelDiaTask = null;
            Task<Card<double>> ventaDelDiaTask = null;
            Task<Card<double,double>> produccionDelDiaTask = null;

            if(ops.Contains(Ops.Anulaciones)) {
                anulacionesTask = anulacionesRepository.GetAsync();
            }
            if(ops.Contains(Ops.ProductosVendidosMes)) {
                ventasProductosMesTask = chartRepository.GetAsync<string,double>(Charts.ProductosVendidosMes, pars);
            }
            if(ops.Contains(Ops.PlatosMasVendidosMes)) {
                platosVendidosMesTask = chartRepository.GetAsync<string,int>(Charts.PlatosMasVendidosMes, pars);
            }
            if(ops.Contains(Ops.VentaAnual)) {
                ventasAnualesTask = chartRepository.GetAsync<int,double>(Charts.VentasAnuales, pars);
            }
            if(ops.Contains(Ops.AnulacionesMes)) {
                anulacionesDelMesTask = chartRepository.GetAsync<string,int>(Charts.AnulacionesDelMes, pars);
            }
            if(ops.Contains(Ops.PaxDia)) {
                paxDelDiaTask = cardRepository.GetAsync<int>(Cards.PaxDia);
            }
            if(ops.Contains(Ops.VentaDia)) {
                ventaDelDiaTask = cardRepository.GetAsync<double>(Cards.VentaDia);
            }
            if(ops.Contains(Ops.ProduccionDia)) {
                produccionDelDiaTask = cardRepository.GetAsync<double,double>(Cards.ProduccionDia);
            }
            
            var taskList = new List<Task>{ anulacionesTask, ventasProductosMesTask, platosVendidosMesTask, ventasAnualesTask, anulacionesDelMesTask, 
                                           paxDelDiaTask, ventaDelDiaTask, produccionDelDiaTask
                                         };
            
            await Task.WhenAll(taskList.Where(t => t!=null));

            var dashboard = new Dashboard {
                Anulaciones = anulacionesTask?.Result,
                ProductosVendidosMes = ventasProductosMesTask?.Result,
                PlatosVendidosMes = platosVendidosMesTask?.Result,
                VentasAnuales = ventasAnualesTask?.Result,
                AnulacionesDelMes = anulacionesDelMesTask?.Result,
                PaxDelDia = paxDelDiaTask?.Result,
                VentaDelDia = ventaDelDiaTask?.Result,
                ProduccionDelDia = produccionDelDiaTask?.Result
            };
            return dashboardBuilder.Build(dashboard, pars);
        }
    }
}
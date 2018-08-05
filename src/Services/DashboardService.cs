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

            var anulacionesTask = ops.Contains(Ops.Anulaciones) ? anulacionesRepository.GetAsync() : null;
            var ventasProductosMesTask = ops.Contains(Ops.ProductosVendidosMes) ? chartRepository.GetAsync<string,double>(Charts.ProductosVendidosMes, pars): null;
            var platosVendidosMesTask = ops.Contains(Ops.PlatosMasVendidosMes) ? chartRepository.GetAsync<string,int>(Charts.PlatosMasVendidosMes, pars): null;
            var ventasAnualesTask = ops.Contains(Ops.VentaAnual) ? chartRepository.GetAsync<int,double>(Charts.VentasAnuales, pars): null;
            var ventasAnualesGroupedTask = ops.Contains(Ops.VentaAnualGrouped) ? chartRepository.GetWithGroupAsync<int,double>(Charts.VentasAnualesGrupo, pars): null;
            var anulacionesDelMesTask = ops.Contains(Ops.AnulacionesMes) ? chartRepository.GetAsync<string,int>(Charts.AnulacionesDelMes, pars): null;
            var paxDelDiaTask = ops.Contains(Ops.PaxDia) ? cardRepository.GetAsync<int>(Cards.PaxDia): null;
            var ventaDelDiaTask = ops.Contains(Ops.VentaDia) ? cardRepository.GetAsync<double>(Cards.VentaDia): null;
            var produccionDelDiaTask = ops.Contains(Ops.ProduccionDia) ? cardRepository.GetAsync<double,double>(Cards.ProduccionDia): null;
            var mozoProduccionMesTask = ops.Contains(Ops.MozoProduccionMes) ? chartRepository.GetAsync<string,double>(Charts.MozoProduccionMes, pars): null;
            
            var taskList = new List<Task>{ anulacionesTask, ventasProductosMesTask, platosVendidosMesTask, ventasAnualesTask, anulacionesDelMesTask, 
                                           paxDelDiaTask, ventaDelDiaTask, produccionDelDiaTask, ventasAnualesGroupedTask, mozoProduccionMesTask
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
                ProduccionDelDia = produccionDelDiaTask?.Result,
                VentasAnualesGrupo = ventasAnualesGroupedTask?.Result,
                MozoProduccionDelMes = mozoProduccionMesTask?.Result
            };
            return dashboardBuilder.Build(dashboard, pars);
        }
    }
}
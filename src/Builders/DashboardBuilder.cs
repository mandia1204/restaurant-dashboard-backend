using System.Collections.Generic;
using System.Linq;
using Mappers;
using Mappers.Interfaces;
using Models;
using Models.Dto;
using Util;

namespace Builders
{
    public class DashboardBuilder: IDashboardBuilder
    {
        private readonly IAnulacionMapper anulacionMapper;
        private readonly IChartMapper chartMapper;
        private readonly ICardMapper cardMapper;
        public DashboardBuilder(IAnulacionMapper anulacionMapper, IChartMapper chartMapper, ICardMapper cardMapper) {
            this.anulacionMapper = anulacionMapper;
            this.chartMapper = chartMapper;
            this.cardMapper = cardMapper;
        }
        public DashboardDto Build(Dashboard model, DashboardParameters pars) {
            var ops = pars.ParseOps();
            var ventasProductosMesChart = chartMapper.Map(model.productosVendidosMes, Charts.ProductosVendidosMes, new List<string> { pars.mes.ToString() }, Constants.TiposDeProducto);
            var platosVendidosMesChart = chartMapper.Map(model.platosVendidosMes, Charts.PlatosMasVendidosMes, new List<string> { pars.mes.ToString() }, null);
            var ventasAnualesChart = chartMapper.Map(model.ventasAnuales, Charts.VentasAnuales, new List<string> { pars.anio.ToString() }, Constants.Meses);
            var anulacionesDelMesChart = chartMapper.Map(model.anulacionesDelMes, Charts.AnulacionesDelMes, new List<string> { pars.mes.ToString() }, Constants.MotivosEliminacion);
            var paxDelDiaCard = cardMapper.Map(model.paxDelDia);
            var ventaDelDiaCard = cardMapper.Map(model.ventaDelDia);
            var produccionDelDiaCard = cardMapper.Map(model.produccionDelDia);
            CardDto ticketPromedioCard = null;
            if(ops.Contains(Ops.TicketPromedioDia)){
                ticketPromedioCard = cardMapper.MapTicketPromedio(model.produccionDelDia, model.paxDelDia);
            }

            var charts = new List<ChartDto>{ ventasProductosMesChart, platosVendidosMesChart, ventasAnualesChart, anulacionesDelMesChart };
            var cards = new Dictionary<string, CardDto> { { Cards.PaxDia, paxDelDiaCard }, { Cards.VentaDia, ventaDelDiaCard }, {Cards.ProduccionDia, produccionDelDiaCard}, {Cards.TicketPromedioDia, ticketPromedioCard} };

            var dashboard = new DashboardDto {
                anulaciones = anulacionMapper.Map(model.anulaciones),
                charts = charts.Where(c => c!= null).ToList(),
                cards = cards.Where(k => k.Value!= null).ToDictionary(k => k.Key, k => k.Value)
            };

            return dashboard;
        }
    }
}
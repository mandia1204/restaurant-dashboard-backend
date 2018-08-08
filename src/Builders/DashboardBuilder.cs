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
            var ventasProductosMes = chartMapper.Map(model.ProductosVendidosMes, Charts.ProductosVendidosMes, Constants.Meses[pars.mes], Constants.TiposDeProducto);
            var platosVendidosMes = chartMapper.Map(model.PlatosVendidosMes, Charts.PlatosMasVendidosMes, Constants.Meses[pars.mes] , null);
            var ventasAnuales = chartMapper.Map(model.VentasAnuales, Charts.VentasAnuales, pars.anio.ToString(), Constants.Meses);
            var ventasAnualesGrupo = chartMapper.Map(model.VentasAnualesGrupo, Charts.VentasAnualesGrupo,  Constants.Meses);
            var anulacionesDelMes = chartMapper.Map(model.AnulacionesDelMes, Charts.AnulacionesDelMes, Constants.Meses[pars.mes], Constants.MotivosEliminacion);
            var mozoProduccionMes = chartMapper.Map(model.MozoProduccionDelMes, Charts.MozoProduccionMes, Constants.Meses[pars.mes], null);
            var paxDelDia = cardMapper.Map(model.PaxDelDia);
            var ventaDelDia = cardMapper.Map(model.VentaDelDia);
            var produccionDelDia = cardMapper.Map(model.ProduccionDelDia);
            CardDto ticketPromedio = null;
            if(ops.Contains(Ops.TicketPromedioDia)){
                ticketPromedio = cardMapper.MapTicketPromedio(model.ProduccionDelDia, model.PaxDelDia);
            }

            var charts = new List<ChartDto>{ ventasProductosMes, platosVendidosMes, ventasAnuales, ventasAnualesGrupo, anulacionesDelMes, mozoProduccionMes };
            var cards = new Dictionary<string, CardDto> { { Cards.PaxDia, paxDelDia }, { Cards.VentaDia, ventaDelDia }, {Cards.ProduccionDia, produccionDelDia}, {Cards.TicketPromedioDia, ticketPromedio} };

            var dashboard = new DashboardDto {
                Anulaciones = anulacionMapper.Map(model.Anulaciones),
                Charts = charts.Where(c => c!= null).ToList(),
                Cards = cards.Where(k => k.Value!= null).ToDictionary(k => k.Key, k => k.Value)
            };

            return dashboard;
        }
    }
}
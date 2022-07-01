using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Models;
using Repositories.Interfaces;
using Util;

namespace Repositories
{
    public class ChartRepository : IChartRepository
    {
        private readonly string connectionString;
        public ChartRepository(IConnectionStringProvider conn) {
            this.connectionString = conn.GetConnectionString();
        }
        public async Task<IEnumerable<ChartRow<K,V>>> GetAsync<K,V>(string chartName, DashboardParameters pars)
        {
            using(var connection = new SqlConnection(this.connectionString)) {
                await connection.OpenAsync();
                var tup = GetQueryParams(chartName, pars);
                return await connection.QueryAsync<ChartRow<K,V>>(tup.Item1, tup.Item2, null, null, CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ChartRow<K,V>>> GetWithGroupAsync<K,V>(string chartName, DashboardParameters pars)
        {
            using(var connection = new SqlConnection(this.connectionString)) {
                await connection.OpenAsync();
                var tup = GetQueryParams(chartName, pars);
                return await connection.QueryAsync<ChartRow<K,V>>(tup.Item1, tup.Item2, null, null, CommandType.Text);
            }
        }
        
        private Tuple<string, object> GetQueryParams(string chartName, DashboardParameters pars) {
            switch(chartName) {
                case Charts.ProductosVendidosMes:
                    return new Tuple<string, object>("USP_DASHBOARD_PRODUCTOS_VENTAS_MES", new { YEAR = pars.anio, MONTH = pars.mes });
                case Charts.PlatosMasVendidosMes:
                    return new Tuple<string, object>("USP_DASHBOARD_PLATOS_MAS_VENDIDOS_MES", new { YEAR = pars.anio, MONTH = pars.mes });
                case Charts.VentasAnuales:
                    return new Tuple<string, object>("USP_DASHBOARD_VENTA_ANUAL", new { YEAR = pars.anio });
                case Charts.VentasAnualesGrupo:
                    var years = new List<int>{ pars.anio -1 , pars.anio};
                    return new Tuple<string, object>(ChartQueries.VentasAnualesGroup, new { YEARS = years });
                case Charts.MozoProduccionMes: 
                    return new Tuple<string, object>("USP_DASHBOARD_MOZO_PRODUCCION_MES", new { YEAR = pars.anio, MONTH = pars.mes });
                case Charts.AnulacionesDelMes:
                    return new Tuple<string, object>("USP_DASHBOARD_ANULACIONES_DEL_MES", new { YEAR = pars.anio, MONTH = pars.mes });
                default:
                    return null;
            }
        }
    }
}
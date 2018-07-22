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
        public ChartRepository(IOptions<DatabaseSettings> dbSettings) {
            this.connectionString = dbSettings.Value.connectionString;
        }
        public async Task<IEnumerable<ChartRow<K,V>>> GetAsync<K,V>(string chartName, DashboardParameters pars)
        {
            using(var connection = new SqlConnection(this.connectionString)) {
                await connection.OpenAsync();
                var tup = GetUspParams(chartName, pars);
                return await connection.QueryAsync<ChartRow<K,V>>(tup.Item1, tup.Item2, null, null, CommandType.StoredProcedure);
            }
        }
        private Tuple<string, object> GetUspParams(string chartName, DashboardParameters pars) {
            switch(chartName) {
                case Charts.ProductosVendidosMes:
                    return new Tuple<string, object>("USP_DASHBOARD_PRODUCTOS_VENTAS_MES_2", new { YEAR = pars.anio, MONTH = pars.mes });
                case Charts.PlatosMasVendidosMes:
                    return new Tuple<string, object>("USP_DASHBOARD_PLATOS_MAS_VENDIDOS_MES_2", new { YEAR = pars.anio, MONTH = pars.mes });
                case Charts.VentasAnuales:
                    return new Tuple<string, object>("USP_DASHBOARD_VENTA_ANUAL_2", new { YEAR = pars.anio });
                case Charts.AnulacionesDelMes:
                    return new Tuple<string, object>("USP_DASHBOARD_ANULACIONES_DEL_MES_2", new { YEAR = pars.anio, MONTH = pars.mes });
                default:
                    return null;
            }
        }
    }
}
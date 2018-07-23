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
    public class CardRepository : ICardRepository
    {
        private readonly string connectionString;
        public CardRepository(IOptions<DatabaseSettings> dbSettings) {
            this.connectionString = dbSettings.Value.ConnectionString;
        }
        public Task<Card<V>> GetAsync<V>(string cardName)
        {
            return GetFirstAsync<Card<V>>(cardName);
        }

        public Task<Card<V1,V2>> GetAsync<V1, V2>(string cardName)
        {
            return GetFirstAsync<Card<V1,V2>>(cardName);
        }

        private async Task<T> GetFirstAsync<T>(string cardName)
        {
            using(var connection = new SqlConnection(this.connectionString)) {
                await connection.OpenAsync();
                var tup = GetUspParams(cardName);
                return await connection.QueryFirstOrDefaultAsync<T>(tup.Item1, tup.Item2, null, null, CommandType.StoredProcedure);
            }
        }

        private Tuple<string, object> GetUspParams(string cardName) {
            switch(cardName) {
                case Cards.PaxDia:
                    return new Tuple<string, object>("USP_DASHBOARD_PAX_DEL_DIA", null);
                case Cards.VentaDia:
                    return new Tuple<string, object>("USP_DASHBOARD_VENTA_DEL_DIA", null);
                case Cards.ProduccionDia:
                    return new Tuple<string, object>("USP_DASHBOARD_PRODUCCION_DEL_DIA", null);
                default:
                    return null;
            }
        }
    }
}
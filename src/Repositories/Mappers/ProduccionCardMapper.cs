using System.Data.SqlClient;
using Models;

namespace Repositories.Mappers {
    public interface IProduccionCardMapper
    {
        ProduccionCard Map(SqlDataReader r);
    }
    public class ProduccionCardMapper: IProduccionCardMapper {
        public ProduccionCard Map(SqlDataReader r) {
            var card = new ProduccionCard() ;
            
            while (r.Read()){
                var totalOrdinal = r.GetOrdinal("total");
                var paloteoOrdinal = r.GetOrdinal("total_paloteo");
                card.value = string.Format("{0:0.00}", r.GetDouble(totalOrdinal));
                card.totalPaloteo = r.GetDouble(paloteoOrdinal);
            }

            return card;
        }
    }
}
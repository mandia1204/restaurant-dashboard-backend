using System.Data.SqlClient;
using Models;

namespace Repositories.Mappers {
    public interface ITicketPromedioCardMapper{
        Card Map(ProduccionCard produccion, Card pax);
    }
    public class TicketPromedioCardMapper: ITicketPromedioCardMapper {
        public Card Map(ProduccionCard produccion, Card pax) {
            var card = new Card () ;
            var total =string.Format("{0:0.00}", produccion.totalPaloteo / double.Parse(pax.value));
            card.value = total =="NaN" ? "0.00": total;
            return card;
        }
    }
}
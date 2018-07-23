using System.Data.SqlClient;
using Mappers.Interfaces;
using Models;
using Models.Dto;

namespace Mappers {
    public class CardMapper: ICardMapper {
        public CardDto Map<V>(Card<V> model) {
            if(model == null){
                return null;
            }

            var card = new CardDto() ;
            
            if(typeof(V)==typeof(int)){
                card.Value = model.Value.ToString();
            }else if(typeof(V)==typeof(double)){
                card.Value = string.Format("{0:0.00}", model.Value);
            }

            return card;
        }
        public CardDto Map<V1,V2>(Card<V1,V2> model) {
            if(model == null) {
                return null;
            }
            return Map<V1>(new Card<V1>{ Value = model.Value});
        }
        /// <summary>
        /// paloteo / pax
        /// </summary>
        public CardDto MapTicketPromedio(Card<double, double> produccion, Card<int> pax) {
            if(produccion == null || pax == null){
                return null;
            }

            var card = new CardDto () ;
            var total =string.Format("{0:0.00}", produccion.Value2 / pax.Value);
            card.Value = total =="NaN" ? "0.00": total;
            return card;
        }
    }
}
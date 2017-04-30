using System.Data.SqlClient;
using Models;

namespace Repositories.Mappers {
    public interface ICardMapper
    {
        Card Map<T>(SqlDataReader r);
    }
    public class CardMapper: ICardMapper {
        public Card Map<T>(SqlDataReader r) {
            var card = new Card() ;
            
            while (r.Read()){
                var totalOrdinal = r.GetOrdinal("total");
                if(typeof(T)==typeof(int)){
                    card.value = r.GetInt32(totalOrdinal).ToString();
                }else if(typeof(T)==typeof(double)){
                    card.value = string.Format("{0:0.00}", r.GetDouble(totalOrdinal));
                }
            }

            return card;
        }
    }
}
using System.Data.SqlClient;
using Models;

namespace Repositories.Mappers {
    public class ReaderToCard: IReaderToCard {
        public Card Map<T>(SqlDataReader r) {
            var card = new Card() ;
            
            while (r.Read()){
                card.title = r.GetString(1);
                if(!r.IsDBNull(0)) {
                    if(typeof(T)==typeof(int)){
                        card.value = r.GetInt32(0).ToString();
                    }else if(typeof(T)==typeof(double)){
                        card.value = string.Format("{0:0.00}", r.GetDouble(0));
                    }
                }else{
                    card.value = default(T).ToString();
                }
            }

            return card;
        }
    }
}
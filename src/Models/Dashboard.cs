using System.Collections.Generic;

namespace Models{
    public class Dashboard {

        public Dashboard(){
            charts = new List<Chart>(); 
            cards = new Dictionary<string, Card>();
        }
        public List<Chart> charts {get; set;}
        public Dictionary<string, Card> cards {get; set;}
        public List<Anulacion> anulaciones {get; set;}
    }
}
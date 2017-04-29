using System.Collections.Generic;

namespace Models{
    public class Dashboard {
        public List<Chart> charts {get; set;}
        public Dictionary<string, Card> cards {get; set;}
    }
}
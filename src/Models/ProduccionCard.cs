using Newtonsoft.Json;

namespace Models {
    public class ProduccionCard : Card {
        [JsonIgnore]
        public double totalPaloteo {get;set;}
    }
}

using System.Collections.Generic;

namespace Models.Dto {
    public class ChartDto {
        public string name { get; set;}
        public Dictionary<string, Dictionary<string, object>> data { get; set;}
    }
}
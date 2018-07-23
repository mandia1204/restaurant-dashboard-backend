using System.Collections.Generic;

namespace Models.Dto {
    public class ChartDto {
        public string Name { get; set;}
        public Dictionary<string, Dictionary<string, object>> Data { get; set;}
    }
}
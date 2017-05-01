using System.Collections.Generic;

namespace Models {
    public class Chart {
        public string name { get; set;}
        public Dictionary<string, Dictionary<string, int>> data { get; set;}
    }
}
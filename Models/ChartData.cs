using System.Collections.Generic;

namespace Models {
    public class ChartData {
        public string name { get; set;}
        public string[] labels { get; set;}
        public List<Dataset> datasets { get; set;}

    }
}
using System;
using System.Collections.Generic;

namespace Models{
    public class DashboardParameters {
        public short anio {get; set;}
        public short mes {get; set;}
        public string ops {get; set;}
        public IEnumerable<string> ParseOps() {
            return ops != null ?  ops.Split(",") : null;
        }
    }
}
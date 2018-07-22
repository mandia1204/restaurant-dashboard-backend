using System.Collections.Generic;

namespace Models.Dto {
    public class DashboardDto {
        public DashboardDto(){
            charts = new List<ChartDto>(); 
            cards = new Dictionary<string, CardDto>();
        }
        public List<ChartDto> charts {get; set;}
        public Dictionary<string, CardDto> cards {get; set;}
        public IEnumerable<AnulacionDto> anulaciones {get; set;}
    }
}
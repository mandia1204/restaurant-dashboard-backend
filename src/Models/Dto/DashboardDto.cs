using System.Collections.Generic;

namespace Models.Dto {
    public class DashboardDto {
        public DashboardDto(){
            Charts = new List<ChartDto>(); 
            Cards = new Dictionary<string, CardDto>();
        }
        public List<ChartDto> Charts {get; set;}
        public Dictionary<string, CardDto> Cards {get; set;}
        public IEnumerable<AnulacionDto> Anulaciones {get; set;}
    }
}
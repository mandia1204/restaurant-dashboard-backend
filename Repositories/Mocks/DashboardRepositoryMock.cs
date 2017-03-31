using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Repositories.Mocks {
    public class DashboardRepositoryMock : IDashboardRepository{
        public async Task<Dashboard> GetDashboardAsync(){
            
            var dashboard = new Dashboard {
                charts = new List<Chart>{
                    new Chart {
                        name= "VENTAS",
                        labels= new string[] {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio"},
                        datasets= new List<Dataset> { 
                            new Dataset {
                                label= "Ventas del Año",
                                data= new int[] {12000, 15000, 13000, 11000, 20000, 50000, 60000}
                            }
                        }
                    }
                }
            };
            
            return dashboard;
        }
    }
}
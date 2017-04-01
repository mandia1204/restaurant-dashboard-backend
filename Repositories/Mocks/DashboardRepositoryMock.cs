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
                    },
                    new Chart {
                        name= "VENTAS_COMPRAS",
                        labels= new string[] {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio"},
                        datasets= new List<Dataset> { 
                            new Dataset {
                                label= "Ventas",
                                data= new int[] {65, 59, 80, 81, 56, 55, 40}
                            },
                            new Dataset {
                                label = "Compras",
                                data= new int[] {28, 48, 40, 19, 86, 27, 90}
                            }
                        }
                    }
                }
            };
            
            return dashboard;
        }
    }
}
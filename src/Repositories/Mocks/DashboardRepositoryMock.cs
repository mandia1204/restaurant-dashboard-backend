using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Repositories.Mocks {
    public class DashboardRepositoryMock : IDashboardRepository{
        public async Task<Dashboard> GetDashboardAsync(DashboardParameters pars){
            Dashboard dashboard = null;
            var task = Task.Run(() => {
                dashboard = new Dashboard {
                    charts = new List<Chart>{
                        new Chart {
                            name= "VENTAS",
                            // labels= new string[] {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio"},
                            // datasets= new List<Dataset> { 
                            //     new Dataset {
                            //         label= "Ventas del Año",
                            //         data= new int[] {12000, 15000, 13000, 11000, 20000, 50000, 60000}
                            //     }
                            // }
                        },
                        new Chart {
                            name= "VENTAS_COMPRAS",
                            // labels= new string[] {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio"},
                            // datasets= new List<Dataset> { 
                            //     new Dataset {
                            //         label= "Ventas",
                            //         data= new int[] {65, 59, 80, 81, 56, 55, 40}
                            //     },
                            //     new Dataset {
                            //         label = "Compras",
                            //         data= new int[] {28, 48, 40, 19, 86, 27, 90}
                            //     }
                            // }
                        },
                        new Chart {
                            name= "VENTAS_SEMANAL",
                            // labels= new string[] {"Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo"},
                            // datasets= new List<Dataset> { 
                            //     new Dataset {
                            //         label= "Semana(01/03 - 07/03)",
                            //         data= new int[] {3900, 1500, 4500, 11000, 5000, 1700, 2500}
                            //     }
                            // }
                        },
                        new Chart {
                            name= "PRODUCTOS_VENDIDOS",
                            // labels= new string[] {"Alimentos", "Bebidas", "Otros"},
                            // datasets= new List<Dataset> { 
                            //     new Dataset {
                            //         data= new int[] {15000, 7000, 6000}
                            //     }
                            // }
                        },
                        new Chart {
                            name= "PLATOS_VENDIDOS",
                            // labels= new string[] {"Aji de gallina", "Arroz con pollo", "Estofado de res", "Lomo saltado", "Arroz chaufa", "Menestron", "Cau Cau", "Higado encebollado", "Papa rellena", "Ceviche" },
                            // datasets= new List<Dataset> { 
                            //     new Dataset {
                            //         label= "Top 10 platos(unidades)",
                            //         data= new int[] {500, 470, 450, 432, 350, 320, 300, 240, 230, 150}
                            //     }
                            // }
                        }
                    }
                };
            });

            await task;
            
            return dashboard;
        }
    }
}
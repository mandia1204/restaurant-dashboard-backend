using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Repositories{
    public class DashboardRepository : IDashboardRepository{
        public async Task<Dashboard> GetDashboardAsync(){
            var dashboard = new Dashboard {
                charts = new List<ChartData>{
                    new ChartData {
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

    // string connectionString = string.Format(@"Data Source=.\sql2008express; Initial Catalog=VENTAS; User id=sa; Password=1234;");
            
            // using (var con = new SqlConnection(connectionString))
            // {
            //     con.Open();
            //     using (var command = new SqlCommand("select TOP 10 * from TMESA", con))
            //     using (var reader = command.ExecuteReader())
            //     {
            //         while (reader.Read())
            //         {
            //             Console.WriteLine("{0} {1} {2}",
            //                 reader.GetString(0), reader.GetString(1), reader.GetString(2));
            //         }
            //     }
            // }
}
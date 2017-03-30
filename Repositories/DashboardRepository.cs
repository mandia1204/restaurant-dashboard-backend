using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Services;
using Repositories.Mappers;
using Repositories.Helpers;

namespace Repositories {
    public class DashboardRepository : IDashboardRepository{

        private DatabaseSettings _dbSettings;
        private IReaderToChart _readerToChart;
        public DashboardRepository(IAppSettingsService appSettings, IReaderToChart readerToChart){
            _dbSettings = appSettings.GetDatabaseSettings();
            _readerToChart = readerToChart;
        }

        public async Task<Dashboard> GetDashboardAsync(){
            
            var dashboard = new Dashboard { charts = new List<Chart>() };

            using(var helper = new AdoHelper(_dbSettings)){
                using(var reader = helper.ExecDataReader("select TOP 10 * from TMESA")){
                    dashboard.charts.Add(_readerToChart.Map(reader, "VENTAS"));
                }
            }
            
            // var dashboard = new Dashboard {
            //     charts = new List<Chart>{
            //         new Chart {
            //             name= "VENTAS",
            //             labels= new string[] {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio"},
            //             datasets= new List<Dataset> { 
            //                 new Dataset {
            //                     key= "VENTAS_ANUALES",
            //                     label= "Ventas del Año",
            //                     data= new int[] {12000, 15000, 13000, 11000, 20000, 50000, 60000}
            //                 }
            //             }

            //         }
            //     }
            // };

            return dashboard;
        }
    }
}
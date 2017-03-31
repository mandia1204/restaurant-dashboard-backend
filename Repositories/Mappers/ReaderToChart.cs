using System.Collections.Generic;
using System.Data.SqlClient;
using Models;
using Util;

namespace Repositories.Mappers {
    public class ReaderToChart: IReaderToChart {
        public Chart Map(SqlDataReader r, string chartName, string title) {
            var chart = new Chart{ name = chartName} ;
            //titulo, etiqueta , total
            //VENTAS_ANUALES, Enero, 15000 
            var labels = new List<string>();
            var data = new List<int>();
            while (r.Read()){
                labels.Add(Constants.Meses[r.GetInt32(0)]);
                data.Add((int)r.GetDouble(1));
            }

            chart.labels = labels.ToArray();
            chart.datasets = new List<Dataset> {
                new Dataset {
                    label = title,
                    data = data.ToArray()
                }
            };

            return chart;
        }
    }
}
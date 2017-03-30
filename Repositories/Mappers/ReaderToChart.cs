using System.Collections.Generic;
using System.Data.SqlClient;
using Models;
using Util;

namespace Repositories.Mappers {
    public class ReaderToChart: IReaderToChart {
        public Chart Map(SqlDataReader r, string chartName) {
            var chart = new Chart{ name = chartName} ;
            //titulo, etiqueta , total
            //VENTAS_ANUALES, Enero, 15000 
            var labels = new List<string>();
            var data = new List<int>();
            var title = "";
            while (r.Read()){
                if(title == "") 
                    title = Constants.DataSetLabels[r.GetString(0)];

                labels.Add(r.GetString(1));
                data.Add(r.GetInt32(2));
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
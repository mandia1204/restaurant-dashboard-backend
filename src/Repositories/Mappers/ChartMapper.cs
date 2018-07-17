using System.Collections.Generic;
using System.Data.SqlClient;
using Models;
using Util;

namespace Repositories.Mappers {
    public interface IChartMapper
    {
        Chart Map<T>(SqlDataReader r, string chartName, List<string> dataHeaders, Dictionary<T, string> valueMapper);
    }
    public class ChartMapper: IChartMapper {
        
      public Chart Map<T>(SqlDataReader r, string chartName, List<string> dataHeaders, Dictionary<T, string> valueMapper) {
            var data = new Dictionary<string, Dictionary<string, int>>();
            var chart = new Chart{ name = chartName} ;

            dataHeaders.ForEach(h => {
                var itemData = new Dictionary<string, int>();
                while (r.Read()){
                    itemData.Add(valueMapper[r.GetFieldValue<T>(0)], (int)r.GetDouble(1));
                 }

                data.Add(h, itemData);
            });

            chart.data = data;
            return chart;
      }
    }
}
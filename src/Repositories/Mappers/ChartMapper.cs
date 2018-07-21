using System.Collections.Generic;
using System.Data.SqlClient;
using Models;
using Util;

namespace Repositories.Mappers {
    public interface IChartMapper
    {
        Chart Map<K, V>(SqlDataReader r, string chartName, List<string> dataHeaders, Dictionary<K, string> valueMapper);
    }
    public class ChartMapper: IChartMapper {
        
      public Chart Map<K, V>(SqlDataReader r, string chartName, List<string> dataHeaders, Dictionary<K, string> valueMapper) {
            var data = new Dictionary<string, Dictionary<string, object>>();
            var chart = new Chart{ name = chartName} ;

            dataHeaders.ForEach(h => {
                var itemData = new Dictionary<string, object>();
                while (r.Read()){
                    var label = r.GetFieldValue<K>(0);
                    itemData.Add(valueMapper!=null ? valueMapper[label]: label.ToString(), r.GetFieldValue<V>(1));
                 }

                data.Add(h, itemData);
            });

            chart.data = data;
            return chart;
      }
    }
}
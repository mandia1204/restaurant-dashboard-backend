using System.Collections.Generic;
using System.Linq;
using Mappers.Interfaces;
using Models;
using Models.Dto;

namespace Mappers
{
    public class ChartMapper: IChartMapper
    {
        public ChartDto Map<K, V>(IEnumerable<ChartRow<K, V>> model, string chartName, List<string> dataHeaders, Dictionary<K, string> valueMapper) {
            if(model== null){
                return null;
            }
            var data = new Dictionary<string, Dictionary<string, object>>();
            var chart = new ChartDto { Name = chartName} ;
            
            dataHeaders.ForEach(h => {
                var itemData = model.Select(c => { 
                    return new { Key = valueMapper!=null ? valueMapper[c.Key]: c.Key.ToString(), Value = c.Value };
                });
                data.Add(h, itemData.ToDictionary(x => x.Key, x=> (object)x.Value));
            });
            chart.Data = data;
            return chart;
        }
    }
}
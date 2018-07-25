using System.Collections.Generic;
using System.Linq;
using Mappers.Interfaces;
using Models;
using Models.Dto;

namespace Mappers
{
    public class ChartMapper: IChartMapper
    {
        public ChartDto Map<K, V>(IEnumerable<ChartRow<K, V>> model, string chartName, IEnumerable<string> dataHeaders, Dictionary<K, string> valueMapper) {
            if(model== null){
                return null;
            }
            var data = new Dictionary<string, Dictionary<string, object>>();
            var chart = new ChartDto { Name = chartName} ;
            
            foreach(var header in dataHeaders){
                var itemData = model.ToDictionary(k => valueMapper !=null ? valueMapper[k.Key]: k.Key.ToString(), v => (object)v.Value);
                data.Add(header, itemData);
            }

            chart.Data = data;
            return chart;
        }
    }
}
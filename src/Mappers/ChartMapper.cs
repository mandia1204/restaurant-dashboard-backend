using System.Collections.Generic;
using System.Linq;
using Mappers.Interfaces;
using Models;
using Models.Dto;

namespace Mappers
{
    public class ChartMapper: IChartMapper
    {
        /// <summary>
        /// Simple chart
        /// </summary>
        public ChartDto Map<K, V>(IEnumerable<ChartRow<K, V>> model, string chartName, string headerName, Dictionary<K, string> valueMapper) {
            if(model== null){
                return null;
            }
            var itemData = model.ToDictionary(k => valueMapper !=null ? valueMapper[k.Key]: k.Key.ToString(), v => (object)v.Value);

            return new ChartDto {
                Name = chartName,
                Data = new Dictionary<string, Dictionary<string, object>> { { headerName, itemData} }
            };
        }

        /// <summary>
        /// For mapping chart with groups
        /// </summary>
        public ChartDto Map<K, V>(IEnumerable<ChartRow<K, V>> model, string chartName, Dictionary<K, string> valueMapper) {
            if(model== null){
                return null;
            }
            var data = new Dictionary<string, Dictionary<string, object>>();
            var groups = model.Select(m => m.Group).Distinct().OrderBy(m=> m);
            foreach(var groupName in groups) {
                var groupData = model.Where(m => m.Group == groupName).OrderBy(m=> m.Key); 
                var itemData = groupData.ToDictionary(k => valueMapper !=null ? valueMapper[k.Key]: k.Key.ToString(), v => (object)v.Value);
                data.Add(groupName, itemData);
            }

            return new ChartDto {
                Name = chartName,
                Data = data
            };
        }
    }
}
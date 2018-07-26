using System.Collections.Generic;
using Models;
using Models.Dto;

namespace Mappers.Interfaces
{
    public interface IChartMapper
    {
        ChartDto Map<K, V>(IEnumerable<ChartRow<K, V>> model, string chartName, string header, Dictionary<K, string> valueMapper);
        ChartDto Map<K, V>(IEnumerable<ChartRow<K, V>> model, string chartName, Dictionary<K, string> valueMapper);
    }
}
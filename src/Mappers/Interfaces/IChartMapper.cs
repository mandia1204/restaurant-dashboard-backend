using System.Collections.Generic;
using Models;
using Models.Dto;

namespace Mappers.Interfaces
{
    public interface IChartMapper
    {
        ChartDto Map<K, V>(IEnumerable<ChartRow<K, V>> model, string chartName, IEnumerable<string> dataHeaders, Dictionary<K, string> valueMapper);
    }
}
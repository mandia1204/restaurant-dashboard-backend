using Models;
using Models.Dto;

namespace Mappers.Interfaces
{
    public interface ICardMapper
    {
        CardDto Map<V>(Card<V> model);
        CardDto Map<V1,V2>(Card<V1,V2> model);
        CardDto MapTicketPromedio(Card<double, double> produccion, Card<int> pax);
    }
}
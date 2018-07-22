using System.Collections.Generic;
using Models;
using Models.Dto;

namespace Mappers.Interfaces
{
    public interface IAnulacionMapper
    {
         IEnumerable<AnulacionDto> Map(IEnumerable<Anulacion> anulaciones);
    }
}
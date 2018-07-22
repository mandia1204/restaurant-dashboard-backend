using System.Collections.Generic;
using System.Data.SqlClient;
using Models;
using System.Globalization;
using Util;
using Models.Dto;
using System.Linq;
using Mappers.Interfaces;

namespace Mappers {
    public class AnulacionMapper: IAnulacionMapper {
        public IEnumerable<AnulacionDto> Map(IEnumerable<Anulacion> anulaciones) {
            if(anulaciones == null) {
                return new List<AnulacionDto>();
            }
            return anulaciones.Select(a => new AnulacionDto {
                fecha = a.fecha.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                observacion = a.observacion?.ToLower(),
                tipo = Constants.MotivosEliminacion[a.tipo]
            });
        }
    }
}
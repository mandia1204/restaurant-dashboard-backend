using System.Collections.Generic;
using System.Data.SqlClient;
using Models;
using Extensions;
using System.Globalization;

namespace Repositories.Mappers {
    public interface IAnulacionMapper
    {
        List<Anulacion> Map(SqlDataReader r);
    }
    public class AnulacionMapper: IAnulacionMapper {
        public List<Anulacion> Map(SqlDataReader r) {
            var list = new List<Anulacion>() ;
            
            while (r.Read()){
                list.Add(new Anulacion {
                    fecha= r.GetDateTime(0).ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    observacion= r.SafeGetString(1).ToLower(),
                    tipo= r.SafeGetString(2).ToLower()
                });
            }

            return list;
        }
    }
}
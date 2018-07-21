using System.Collections.Generic;
using System.Data.SqlClient;
using Models;
using Extensions;
using System.Globalization;
using Util;

namespace Repositories.Mappers {
    public interface IAnulacionMapper
    {
        List<Anulacion> Map(SqlDataReader r);
        Chart MapMensual(SqlDataReader r, string chartName, int mes);
    }
    public class AnulacionMapper: IAnulacionMapper {
        public List<Anulacion> Map(SqlDataReader r) {
            var list = new List<Anulacion>() ;
            
            while (r.Read()){
                list.Add(new Anulacion {
                    fecha= r.GetDateTime(0).ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    observacion= r.SafeGetString(1).ToLower(),
                    tipo= Constants.MotivosEliminacion[r.SafeGetString(2)]
                });
            }

            return list;
        }

        public Chart MapMensual(SqlDataReader r, string chartName, int mes) {
            var chart = new Chart{ name = chartName} ;

            var data = new Dictionary<string, Dictionary<string, object>>();
            var itemData = new Dictionary<string, object>() ;
            
            while (r.Read()){
                for(var x=0; x<5; x++) {
                    var current = string.Format("00{0}", x);
                    var val = r.GetInt32(r.GetOrdinal(current));
                    itemData.Add(Constants.MotivosEliminacion[current],val);
                }
            }

            data.Add(Constants.Meses[mes], itemData);
            chart.data = data;
            
            return chart;
        }
    }
}
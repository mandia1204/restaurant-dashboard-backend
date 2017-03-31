using System.Collections.Generic;

namespace Util
{
    public class Constants {
        public static readonly Dictionary<string, string> DataSetLabels= new Dictionary<string, string>()
        {
            {"VENTAS_ANUALES","Ventas del Año" }
        };

        public static readonly Dictionary<int, string> Meses= new Dictionary<int, string>()
        {
            {1,"Enero" },{2,"Febrero" },{3,"Marzo" },{4,"Abril" },{5,"Mayo" },{6,"Junio" },{7,"Julio" },
            {8,"Agosto" },{9,"Septiembre"},{10,"Octubre" },{11,"Noviembre" }, {12,"Diciembre" }
        };
    }
}
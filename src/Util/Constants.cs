using System.Collections.Generic;

namespace Util
{
    public class Constants {
        public static readonly Dictionary<int, string> Meses= new Dictionary<int, string>()
        {
            {1,"Enero" },{2,"Febrero" },{3,"Marzo" },{4,"Abril" },{5,"Mayo" },{6,"Junio" },{7,"Julio" },
            {8,"Agosto" },{9,"Septiembre"},{10,"Octubre" },{11,"Noviembre" }, {12,"Diciembre" }
        };
        public static readonly Dictionary<string, string> MotivosEliminacion= new Dictionary<string, string>()
        {
            {"000", "otro" }, {"001", "digitación" }, {"002", "falta producción" }, {"003", "derrame" }, {"004", "cambio" }
        };


    }
    public static class Ops
    {
        public const string ProduccionDia = "PDD";
        public const string VentaAnual = "VA";
        public const string VentaDia = "VDD";
        public const string PaxDia = "PXD";
        public const string Anulaciones = "ANL";
        public const string AnulacionesMes = "ANM";
    }
}
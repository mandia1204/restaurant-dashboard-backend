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
            {"000", "otro" }, {"001", "digitación" }, {"002", "producción" }, {"003", "derrame" }, {"004", "cambio" }, {"005", "mal comandado" }, {"006", "sin insumo" }, {"default", "default" }
        };

        public static readonly Dictionary<string, string> TiposDeProducto= new Dictionary<string, string>()
        {
            {"01","Alimentos" }, {"02","Bebidas" }, {"03","Marketing" }, {"04","Otros" }
        };
    }
    public static class Ops
    {
        public const string VentaAnual = "VA";
        public const string VentaDia = "VDD";
        public const string ProduccionDia = "PDD";
        public const string TicketPromedioDia = "TPD";
        public const string PaxDia = "PXD";
        public const string Anulaciones = "ANL";
        public const string AnulacionesMes = "ANM";
        public const string ProductosVendidosMes = "PVM";
        public const string PlatosMasVendidosMes = "PMV";
    }

    public static class Charts {
        public const string VentasAnuales = "ventasAnuales";
        public const string AnulacionesDelMes = "anulacionesDelMes";
        public const string ProductosVendidosMes = "productosVendidosMes";
        public const string MozoDelMes = "mozoDelMes";
        public const string PlatosMasVendidosMes = "platosMasVendidosMes";
    }

    public static class Cards {
        public const string ProduccionDia = "produccionDia";
        public const string VentaDia = "ventaDia";
        public const string PaxDia = "paxDia";
        public const string TicketPromedioDia = "ticketPromedioDia";
    }
}
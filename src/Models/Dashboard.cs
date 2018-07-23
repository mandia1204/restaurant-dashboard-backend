using System.Collections.Generic;
using System.ComponentModel;
using Models.Dto;

namespace Models{
    public class Dashboard {
        public IEnumerable<ChartRow<string, double>> ProductosVendidosMes {get; set;}
        public IEnumerable<ChartRow<string, int>> PlatosVendidosMes {get; set;}
        public IEnumerable<ChartRow<int, double>> VentasAnuales {get; set;}
        public IEnumerable<ChartRow<string, int>> AnulacionesDelMes {get; set;}
        public Card<int> PaxDelDia {get; set;}
        public Card<double> VentaDelDia {get; set;}
        /// <summary>
        /// Value1 = ventas del día, Value2= total paloteo
        /// </summary>
        public Card<double,double> ProduccionDelDia {get; set;}
        public IEnumerable<Anulacion> Anulaciones {get; set;}
    }
}
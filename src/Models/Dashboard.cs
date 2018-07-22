using System.Collections.Generic;
using System.ComponentModel;
using Models.Dto;

namespace Models{
    public class Dashboard {
        public IEnumerable<ChartRow<string, double>> productosVendidosMes {get; set;}
        public IEnumerable<ChartRow<string, int>> platosVendidosMes {get; set;}
        public IEnumerable<ChartRow<int, double>> ventasAnuales {get; set;}
        public IEnumerable<ChartRow<string, int>> anulacionesDelMes {get; set;}
        public Card<int> paxDelDia {get; set;}
        public Card<double> ventaDelDia {get; set;}
        /// <summary>
        /// Value1 = ventas del día, Value2= total paloteo
        /// </summary>
        public Card<double,double> produccionDelDia {get; set;}
        public IEnumerable<Anulacion> anulaciones {get; set;}
    }
}
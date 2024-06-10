using System.Collections.Generic;
using System.Linq;
using Mappers;
using Models;
using NUnit.Framework;

namespace Builders
{
    [TestFixture]
    public class DashboardBuilderTest
    {
        [Test]
        public void Build_PassingModelWithFullData_ReturnsDashboardDto() {
            var model = new Dashboard {
                ProductosVendidosMes = new List<ChartRow<string,double>> {
                    new ChartRow<string, double> {
                        Key = "01",
                        Value = 200.77
                    }
                },PlatosVendidosMes = new List<ChartRow<string,int>> {
                    new ChartRow<string, int> {
                        Key = "Plato1",
                        Value = 150
                    }
                },
                VentasAnuales = new List<ChartRow<int,double>> {
                    new ChartRow<int, double> {
                        Key = 10,
                        Value = 200
                    }
                },
                VentasAnualesGrupo = new List<ChartRow<int,double>> {
                    new ChartRow<int, double> {
                        Group = "Ola",
                        Key = 4,
                        Value = 2000
                    },
                    new ChartRow<int, double> {
                        Group = "Ola",
                        Key = 5,
                        Value = 1000
                    }
                },
                AnulacionesDelMes = new List<ChartRow<string,int>> {
                    new ChartRow<string, int> {
                        Key = "000",
                        Value = 150
                    }
                },
                MozoProduccionDelMes = new List<ChartRow<string,double>> {
                    new ChartRow<string, double> {
                        Key = "mozo1",
                        Value = 599
                    }
                },
                PaxDelDia = new Card<int> { Value = 100 },
                VentaDelDia = new Card<double> { Value = 200 },
                ProduccionDelDia = new Card<double, double> { Value = 200, Value2 = 300 },
                Anulaciones = new List<Anulacion> {
                    new Anulacion {
                        Observacion = "obs 1",
                        Tipo = "001"
                    },
                    new Anulacion {
                        Observacion = "obs 2",
                        Tipo = "002"
                    }
                }
            };
            var parameters = new DashboardParameters {
                anio = 2018,
                mes = 4,
                ops = "TPD"
            };

            var anulacionesMapper = new AnulacionMapper();
            var chartMapper = new ChartMapper();
            var cardMapper = new CardMapper();

            var builder = new DashboardBuilder(anulacionesMapper, chartMapper, cardMapper);
            var result = builder.Build(model, parameters);

            Assert.That(result.Charts.Count(), Is.EqualTo(6));
            Assert.That(result.Cards.Count(), Is.EqualTo(4));
            Assert.That(result.Anulaciones.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Build_PassingModelWithEmptyData_ReturnsEmptyDashboardDto() {
            var model = new Dashboard();
            var parameters = new DashboardParameters {
                anio = 2018,
                mes = 4,
                ops = "TPD"
            };

            var anulacionesMapper = new AnulacionMapper();
            var chartMapper = new ChartMapper();
            var cardMapper = new CardMapper();

            var builder = new DashboardBuilder(anulacionesMapper, chartMapper, cardMapper);
            var result = builder.Build(model, parameters);

            Assert.That(result.Charts.Count(), Is.EqualTo(0));
            Assert.That(result.Cards.Count(), Is.EqualTo(0));
            Assert.That(result.Anulaciones.Count(), Is.EqualTo(0));
        }
    }
}
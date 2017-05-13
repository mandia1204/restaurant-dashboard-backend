using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Models;

namespace Repositories.Mocks {
    public class DashboardRepositoryMock : IDashboardRepository{
        public async Task<Dashboard> GetDashboardAsync(DashboardParameters pars){
            Dashboard dashboard = null;
            var date = DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            var task = Task.Run(() => {
                dashboard = new Dashboard {
                    charts = new List<Chart>{
                        new Chart {
                            name= "VENTAS_ANUALES",
                            data = new Dictionary<string, Dictionary<string, int>> {
                                { "2016", new  Dictionary<string, int>{
                                        {"Septiembre", 70611},
                                        {"Marzo", 42356     },
                                        {"Diciembre", 66843 },
                                        {"Junio", 61640     },
                                        {"Julio", 73136     },
                                        {"Enero", 42479     },
                                        {"Octubre", 55396   },
                                        {"Abril", 54790     },
                                        {"Mayo", 58968      },
                                        {"Febrero", 38636   },
                                        {"Noviembre", 45015 },
                                        {"Agosto", 61566    }
                                    }
                                }
                            }
                        },
                        new Chart {
                            name= "ANULACIONES_DEL_MES",
                            data = new Dictionary<string, Dictionary<string, int>> {
                                { "Octubre", new  Dictionary<string, int>
                                    {
                                        {"otro", 15 },
                                        {"digitación", 20 },
                                        {"falta producción", 6},
                                        {"derrame", 10 },
                                        {"cambio", 16 }
                                    }
                            }
                        }
                    }
                  },//end charts
                  cards = new Dictionary<string, Card> {
                      {"PRODUCCION_DIA", new Card { value= "2500"}},
                      {"VENTA_DIA", new Card { value= "1500"}},
                      {"PAX_DIA", new Card { value= "58"}},
                      {"TICKET_PROMEDIO_DIA", new Card { value= "177"}}
                  },
                  anulaciones = new List<Anulacion> {
                      new Anulacion { fecha = date, tipo= "otro", observacion=""},
                      new Anulacion { fecha = date, tipo= "derrame", observacion="Se le cayó al cliente el vaso."},
                      new Anulacion { fecha = date, tipo= "digitación", observacion=""},
                      new Anulacion { fecha = date, tipo= "otro", observacion="Un problema con la cuenta."},
                      new Anulacion { fecha = date, tipo= "digitación", observacion="No se escribió bien."},
                      new Anulacion { fecha = date, tipo= "otro", observacion="Cliente no le gustó la comida. Solicitó el cambio o de lo contrario no iba a cancelar su cuenta, se tuvo que acceder a su petición."},
                      new Anulacion { fecha = date, tipo= "falta producción", observacion="No llegó el camión a tiempo."},
                      new Anulacion { fecha = date, tipo= "digitación", observacion=""},
                      new Anulacion { fecha = date, tipo= "derrame", observacion=""},
                      new Anulacion { fecha = date, tipo= "cambio", observacion="Se confundió de plato."},
                  }
                };
            });

            await task;
            
            return dashboard;
        }
    }
}
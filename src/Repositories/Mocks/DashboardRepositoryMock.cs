using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Models;

namespace Repositories.Mocks {
    public class DashboardRepositoryMock : IDashboardRepository{
        public async Task<Dashboard> GetDashboardAsync(DashboardParameters pars){
            Dashboard dashboard = null;
            var date = DateTime.Now.ToString("dd/MM HH:mm", CultureInfo.InvariantCulture);
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
                                { "Mayo", new  Dictionary<string, int>
                                    {
                                        {"otro", 15 },
                                        {"digitación", 20 },
                                        {"falta producción", 6},
                                        {"derrame", 10 },
                                        {"cambio", 16 }
                                    }
                            }
                        }
                    },new Chart {
                            name= "PRODUCTOS_VENDIDOS_DEL_MES",
                            data = new Dictionary<string, Dictionary<string, int>> {
                                { "Mayo", new  Dictionary<string, int>
                                    {
                                        {"alimentos", 4500 },
                                        {"bebidas", 1000 },
                                        {"postres", 800},
                                        {"otros", 200 }
                                    }
                            }
                        }
                    },new Chart {
                            name= "MOZO_DEL_MES",
                            data = new Dictionary<string, Dictionary<string, int>> {
                                { "Mayo", new  Dictionary<string, int>
                                    {
                                        {"marcos", 100 },
                                        {"salvatore", 90 },
                                        {"maria", 80},
                                        {"juan", 70 },
                                        {"carmen", 60 },
                                        // {"josue", 50 },
                                        // {"francisco", 40 }
                                        // {"miguel", 30 },
                                        // {"karla", 20 },
                                        // {"norma", 10 }
                                    }
                            }
                        }
                    },new Chart {
                            name= "PLATOS_VENDIDOS_DEL_MES",
                            data = new Dictionary<string, Dictionary<string, int>> {
                                { "Mayo", new  Dictionary<string, int>
                                    {
                                        {"ceviche", 1500 },
                                        {"causa", 1200 },
                                        {"papa rellena", 1000},
                                        {"seco de carne", 900 },
                                        {"lomo saltado", 870 },
                                        // {"aji de gallina", 850 },
                                        // {"spaguetti alfredo", 750 }
                                        // {"huancaina", 700 },
                                        // {"ensalada mixta", 300 },
                                        // {"salchipapa", 150 }
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
                      new Anulacion { fecha = date, tipo= "producción", observacion="No llegó el camión a tiempo."},
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
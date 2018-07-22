using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Models;
using Models.Dto;
using Services.Interfaces;
using Util;

namespace Services.Mocks {
    public class DashboardServiceMock : IDashboardService{
        public async Task<DashboardDto> GetDashboardAsync(DashboardParameters pars){
            DashboardDto dashboard = null;
            var date = DateTime.Now.ToString("dd/MM HH:mm", CultureInfo.InvariantCulture);
            var task = Task.Run(() => {
                dashboard = new DashboardDto {
                    charts = new List<ChartDto>{
                        new ChartDto {
                            name= Charts.VentasAnuales,
                            data = new Dictionary<string, Dictionary<string, object>> {
                                { "2016", new  Dictionary<string, object>{
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
                        new ChartDto {
                            name= Charts.AnulacionesDelMes,
                            data = new Dictionary<string, Dictionary<string, object>> {
                                { "Mayo", new  Dictionary<string, object>
                                    {
                                        {"otro", 15 },
                                        {"digitación", 20 },
                                        {"falta producción", 6},
                                        {"derrame", 10 },
                                        {"cambio", 16 }
                                    }
                            }
                        }
                    },new ChartDto {
                            name= Charts.ProductosVendidosMes,
                            data = new Dictionary<string, Dictionary<string, object>> {
                                { "Mayo", new  Dictionary<string, object>
                                    {
                                        {"alimentos", 4500 },
                                        {"bebidas", 1000 },
                                        {"postres", 800},
                                        {"otros", 200 }
                                    }
                            }
                        }
                    },new ChartDto {
                            name= Charts.MozoDelMes,
                            data = new Dictionary<string, Dictionary<string, object>> {
                                { "Mayo", new  Dictionary<string, object>
                                    {
                                        {"marcos", 100 },
                                        {"salvatore", 90 },
                                        {"maria", 80},
                                        {"juan", 70 },
                                        {"carmen", 60 },
                                        {"josue", 50 },
                                        {"francisco", 40 },
                                        {"miguel", 30 },
                                        {"karla", 20 },
                                        {"norma", 10 }
                                    }
                            }
                        }
                    },new ChartDto {
                            name= Charts.PlatosMasVendidosMes,
                            data = new Dictionary<string, Dictionary<string, object>> {
                                { "Mayo", new  Dictionary<string, object>
                                    {
                                        {"ceviche", 1500 },
                                        {"causa", 1200 },
                                        {"papa rellena", 1000},
                                        {"seco de carne", 900 },
                                        {"lomo saltado", 870 },
                                        {"aji de gallina", 850 },
                                        {"spaguetti alfredo", 750 },
                                        {"huancaina", 700 },
                                        {"ensalada mixta", 300 },
                                        {"salchipapa", 150 }
                                    }
                            }
                        }
                    }
                  },//end charts
                  cards = new Dictionary<string, CardDto> {
                      {Cards.ProduccionDia, new CardDto { value= "2500"}},
                      {Cards.VentaDia, new CardDto { value= "1500"}},
                      {Cards.PaxDia, new CardDto { value= "58"}},
                      {Cards.TicketPromedioDia, new CardDto { value= "177"}}
                  },
                  anulaciones = new List<AnulacionDto> {
                      new AnulacionDto { fecha = date, tipo= "otro", observacion=""},
                      new AnulacionDto { fecha = date, tipo= "derrame", observacion="Se le cayó al cliente el vaso."},
                      new AnulacionDto { fecha = date, tipo= "digitación", observacion=""},
                      new AnulacionDto { fecha = date, tipo= "otro", observacion="Un problema con la cuenta."},
                      new AnulacionDto { fecha = date, tipo= "digitación", observacion="No se escribió bien."},
                      new AnulacionDto { fecha = date, tipo= "otro", observacion="Cliente no le gustó la comida. Solicitó el cambio o de lo contrario no iba a cancelar su cuenta, se tuvo que acceder a su petición."},
                      new AnulacionDto { fecha = date, tipo= "producción", observacion="No llegó el camión a tiempo."},
                      new AnulacionDto { fecha = date, tipo= "digitación", observacion=""},
                      new AnulacionDto { fecha = date, tipo= "derrame", observacion=""},
                      new AnulacionDto { fecha = date, tipo= "cambio", observacion="Se confundió de plato."},
                  }
                };
            });

            await task;
            
            return dashboard;
        }
    }
}
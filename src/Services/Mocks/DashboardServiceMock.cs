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
                    Charts = new List<ChartDto>{
                        new ChartDto {
                            Name= Charts.VentasAnuales,
                            Data = new Dictionary<string, Dictionary<string, object>> {
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
                            Name= Charts.AnulacionesDelMes,
                            Data = new Dictionary<string, Dictionary<string, object>> {
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
                            Name= Charts.ProductosVendidosMes,
                            Data = new Dictionary<string, Dictionary<string, object>> {
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
                            Name= Charts.MozoDelMes,
                            Data = new Dictionary<string, Dictionary<string, object>> {
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
                            Name= Charts.PlatosMasVendidosMes,
                            Data = new Dictionary<string, Dictionary<string, object>> {
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
                  Cards = new Dictionary<string, CardDto> {
                      {Cards.ProduccionDia, new CardDto { Value= "2500"}},
                      {Cards.VentaDia, new CardDto { Value= "1500"}},
                      {Cards.PaxDia, new CardDto { Value= "58"}},
                      {Cards.TicketPromedioDia, new CardDto { Value= "177"}}
                  },
                  Anulaciones = new List<AnulacionDto> {
                      new AnulacionDto { Fecha = date, Tipo= "otro", Observacion=""},
                      new AnulacionDto { Fecha = date, Tipo= "derrame", Observacion="Se le cayó al cliente el vaso."},
                      new AnulacionDto { Fecha = date, Tipo= "digitación", Observacion=""},
                      new AnulacionDto { Fecha = date, Tipo= "otro", Observacion="Un problema con la cuenta."},
                      new AnulacionDto { Fecha = date, Tipo= "digitación", Observacion="No se escribió bien."},
                      new AnulacionDto { Fecha = date, Tipo= "otro", Observacion="Cliente no le gustó la comida. Solicitó el cambio o de lo contrario no iba a cancelar su cuenta, se tuvo que acceder a su petición."},
                      new AnulacionDto { Fecha = date, Tipo= "producción", Observacion="No llegó el camión a tiempo."},
                      new AnulacionDto { Fecha = date, Tipo= "digitación", Observacion=""},
                      new AnulacionDto { Fecha = date, Tipo= "derrame", Observacion=""},
                      new AnulacionDto { Fecha = date, Tipo= "cambio", Observacion="Se confundió de plato."},
                  }
                };
            });

            await task;
            
            return dashboard;
        }
    }
}
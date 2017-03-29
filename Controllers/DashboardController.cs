using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace restaurant_dashboard_backend.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        // GET api/dashboard
        [HttpGet]
        public Dashboard Get()
        {
            // string connectionString = string.Format(@"Data Source=.\sql2008express; Initial Catalog=VENTAS; User id=sa; Password=1234;");
            
            // using (var con = new SqlConnection(connectionString))
            // {
            //     con.Open();
            //     using (var command = new SqlCommand("select TOP 10 * from TMESA", con))
            //     using (var reader = command.ExecuteReader())
            //     {
            //         while (reader.Read())
            //         {
            //             Console.WriteLine("{0} {1} {2}",
            //                 reader.GetString(0), reader.GetString(1), reader.GetString(2));
            //         }
            //     }
            // }
            var model = new Dashboard {
                charts = new List<ChartData>{
                    new ChartData {
                        name= "VENTAS",
                        labels= new string[] {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio"},
                        datasets= new List<Dataset> { 
                            new Dataset {
                                label= "Ventas del Año",
                                data= new int[] {12000, 15000, 13000, 11000, 20000, 50000, 60000}
                            }
                        }

                    }
                }
            };

            return model;
        }

        // GET api/dashboard/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/dashboard
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/dashboard/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/dashboard/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

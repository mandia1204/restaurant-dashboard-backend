using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Repositories.Helpers;
using System;
using System.Threading.Tasks;
using Models;
using Util;
using Repositories.Interfaces;

namespace Repositories {
    public class DashboardReader: IDashboardReader
    {
        private Dictionary<string, Func<Task<SqlDataReader>>> GetDataFunctions(AdoHelper helper, DashboardParameters pars) {
            var funcs = new Dictionary<string, Func<Task<SqlDataReader>>>();

            funcs.Add(Ops.ProduccionDia, () => {
                return helper.ExecDataReaderAsync("USP_DASHBOARD_PRODUCCION_DEL_DIA");
            });
            funcs.Add(Ops.VentaAnual, () => {
                return helper.ExecDataReaderAsync("USP_DASHBOARD_VENTA_ANUAL", CommandType.StoredProcedure, "@YEAR", pars.anio);
            });
            funcs.Add(Ops.VentaDia, () => {
                return helper.ExecDataReaderAsync("USP_DASHBOARD_VENTA_DEL_DIA");
            });
            funcs.Add(Ops.PaxDia, () => {
                return helper.ExecDataReaderAsync("USP_DASHBOARD_PAX_DEL_DIA");
            });
            funcs.Add(Ops.Anulaciones, () => {
                return helper.ExecDataReaderAsync("USP_DASHBOARD_ANULACIONES");
            });
            funcs.Add(Ops.AnulacionesMes, () => {
                return helper.ExecDataReaderAsync("USP_DASHBOARD_ANULACIONES_DEL_MES", CommandType.StoredProcedure, "@YEAR", pars.anio, "@MONTH", pars.mes);
            });
            return funcs;
        }
        public Dictionary<string, Task<SqlDataReader>> GetReaders(AdoHelper helper, DashboardParameters pars) {
            var dataFuncs = GetDataFunctions(helper, pars);
            var queries = pars.ops.Split(','); //"PDD,VA,VDD,PXD,ANL,ANM"

            var readers = new Dictionary<string,Task<SqlDataReader>>();
            foreach(var query in queries) {
                 readers.Add(query, dataFuncs[query]());
            }

            return readers;
        }
    }
}
namespace Repositories
{
    public class ChartQueries
    {
        public const string VentasAnualesGroup = @"SELECT YEAR(fRegistro) [Group], MONTH(fRegistro) [Key], SUM(nventa) [Value] FROM dbo.MDOCUMENTO WHERE tTipoDocumento= '01' AND YEAR(fRegistro) IN @YEARS GROUP BY YEAR(fRegistro), MONTH(fRegistro)";

    }
}
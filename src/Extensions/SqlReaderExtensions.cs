using System.Data.SqlClient;

namespace Extensions {
    public static class SqlReaderExtensions {
        public static string SafeGetString(this SqlDataReader r, int colIndex)
        {
            if(!r.IsDBNull(colIndex))
                return r.GetString(colIndex);
            return string.Empty;
        }
    }
}
namespace Models
{
    public class DatabaseSettings
    {
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

        public string ConnectionString {
            get {
                return $"Data Source={DataSource}; Initial Catalog={InitialCatalog}; User id={UserId}; Password={Password};MultipleActiveResultSets=True;";
            }
        }
    }
}
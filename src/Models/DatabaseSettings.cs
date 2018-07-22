namespace Models
{
    public class DatabaseSettings
    {
        public string dataSource { get; set; }
        public string initialCatalog { get; set; }
        public string userId { get; set; }
        public string password { get; set; }

        public string connectionString {
            get {
                return $"Data Source={dataSource}; Initial Catalog={initialCatalog}; User id={userId}; Password={password};MultipleActiveResultSets=True;";
            }
        }
    }
}
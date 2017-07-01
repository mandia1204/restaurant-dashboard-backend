namespace Models
{
    public class SecuritySettings
    {
        public string secretKey { get; set; }
        public string issuer { get; set; }
        public string audience { get; set; }
    }
}
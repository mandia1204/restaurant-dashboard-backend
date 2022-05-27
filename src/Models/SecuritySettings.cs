namespace Models
{
    public class SecuritySettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public bool UseRsa { get; set; }
        public bool UseKms { get; set; }
    }
}
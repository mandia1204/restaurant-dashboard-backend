namespace Models
{
    public class SecuritySettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public bool UseRsa { get; set; }
        public bool UseKms { get; set; }
    }
}
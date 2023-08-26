namespace StudentInformationSystem.Models
{
    public class AppSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpirationMinutes { get; set; }

        public int RefreshTokenExpirationMinutes { get; set; }
    }
}

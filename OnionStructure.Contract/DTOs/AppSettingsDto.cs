namespace OnionStructure.Contract.DTOs
{
    public class AppSettingsDto
    {
        public AWSDto AWS { get; set; }
        public ConnectionDto ConnectionStrings { get; set; }
        public JWTTokenDto JWTToken { get; set; }
    }


    public class AWSDto
    {
        public string AWSProfileName { get; set; }
        public string AWSAccessKey { get; set; }
        public string AWSSecretKey { get; set; }
    }

    public class ConnectionDto
    {
        public string DBConnectionString { get; set; }
    }

    public class JWTTokenDto
    {
        public string Secret { get; set; }
    }
}

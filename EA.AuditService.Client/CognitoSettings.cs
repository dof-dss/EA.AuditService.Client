namespace EA.AuditService.Client
{
    public class CognitoSettings
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string ClientCredentialsUrl { get; set; }

        public string BuildBasicAuthorizationHeader()
        {
            var buffer = System.Text.Encoding.Default.GetBytes(ClientId + ":" + Secret);
            var base64EncodedString = System.Convert.ToBase64String(buffer);
            return "Basic " + base64EncodedString;
        }
    }
}
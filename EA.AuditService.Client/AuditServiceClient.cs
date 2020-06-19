using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EA.AuditService.Client
{

    public interface IAuditServiceClient
    {
        Task Post(AuditItem auditItem);
    }
    public class AuditServiceClient: IAuditServiceClient
    {
        private HttpClient _httpClient;
        private IAuthClient _authClient;

        public AuditServiceClient(IHttpClientFactory httpClientFactory, IAuthClient authClient)
        {
            _authClient = authClient;
            _httpClient = httpClientFactory.CreateClient("auditService");
        }

        public async Task Post(AuditItem auditItem)
        {
            _httpClient.DefaultRequestHeaders.Authorization = await _authClient.GetAuthenticationHeader();
            _httpClient.DefaultRequestHeaders.Add("x-requestid", Guid.NewGuid().ToString());

            var response = await _httpClient.PostAsync("audits",
                new StringContent(JsonConvert.SerializeObject(auditItem), Encoding.UTF8, "application/json"));
        }
    }
}
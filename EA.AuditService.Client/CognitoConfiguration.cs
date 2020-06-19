using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EA.AuditService.Client
{
    public static class CognitoConfiguration
    {
        private static readonly AuditServiceSettings AuditServiceSettings = new AuditServiceSettings();
        private static IConfiguration _configuration;

        static CognitoConfiguration()
        {
            _configuration = AuditServiceConfigurationBuilder.Build();
            _configuration.GetSection("AuditService").Bind(AuditServiceSettings);
            _configuration.GetSection("AuditService").GetSection("Cognito").Bind(AuditServiceSettings.CognitoSettings);
        }

        public static void ConfigureCognitoForApp(this IServiceCollection services)
        {
            services.AddHttpClient("auditCognito", c =>
            {
                c.DefaultRequestHeaders.Add("Authorization", AuditServiceSettings.CognitoSettings.BuildBasicAuthorizationHeader());
                c.BaseAddress = new Uri(AuditServiceSettings.CognitoSettings.ClientCredentialsUrl);
            });

            services.Configure<CognitoSettings>(_configuration.GetSection("AuditService").GetSection("Cognito"));
            services.AddScoped<IAuthClient, CognitoClient>();
        }
    }
}

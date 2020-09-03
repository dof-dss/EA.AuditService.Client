using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EA.AuditService.Client
{
    public static class AuditServiceConfiguration
    {
        private static readonly AuditServiceSettings auditServiceSettings = new AuditServiceSettings();
        private static AuditServiceOptions auditServiceOptions = new AuditServiceOptions();

        static AuditServiceConfiguration() =>
            AuditServiceConfigurationBuilder.Build().GetSection("AuditService").Bind(auditServiceSettings);

        public static void ConfigureAuditService(this IServiceCollection services, Action<AuditServiceOptions> options = null)
        {
            options?.Invoke(auditServiceOptions);

            var httpClientBuilder = services.AddHttpClient("auditService", c =>
            {
                c.BaseAddress = new Uri(auditServiceSettings.BaseAddress);
            });

            if (auditServiceOptions.HttpClientHandler != null)
                httpClientBuilder.ConfigurePrimaryHttpMessageHandler(() => auditServiceOptions.HttpClientHandler); ;

            services.ConfigureCognitoForApp();

            services.AddScoped<IAuditServiceClient, AuditServiceClient>();
        }
    }
}

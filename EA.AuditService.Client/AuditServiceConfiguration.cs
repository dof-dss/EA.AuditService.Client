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

        static AuditServiceConfiguration() =>
            AuditServiceConfigurationBuilder.Build().GetSection("AuditService").Bind(auditServiceSettings);

        public static void ConfigureAuditService(this IServiceCollection services)
        {
            services.AddHttpClient("auditService", c =>
            {
                c.BaseAddress = new Uri(auditServiceSettings.BaseAddress);
            });

            services.ConfigureCognitoForApp();

            services.AddScoped<IAuditServiceClient, AuditServiceClient>();
        }
    }
}

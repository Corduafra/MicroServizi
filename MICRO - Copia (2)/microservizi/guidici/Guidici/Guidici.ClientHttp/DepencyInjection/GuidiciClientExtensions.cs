using Guidici.ClientHttp.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Security;

namespace Guidici.ClientHttp.DepencyInjection
{
    public static class GuidiciClientExtensions
    {
        public static IServiceCollection AddGuidiciClient(this IServiceCollection services, IConfiguration configuration)
        {
           IConfigurationSection configurationSection = configuration.GetSection(GuidicClientOptions.SectionName);
            GuidicClientOptions opt= configurationSection.Get<GuidicClientOptions>() ?? new();

            services.AddHttpClient<IClientHttp, ClientHttp>(client =>
            {
                client.BaseAddress = new Uri(opt.BaseAddress);
            }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; }
            });

            return services;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Proprietario.ClientHttp.Abstraction;


namespace Proprietario.ClientHttp.DepencyInjection
{
    public static  class ProprietarioClientExtension
    {
        public static IServiceCollection AddProprietarioClient(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection configurationSection = configuration.GetSection(ProprietarioClientOptions.SectionName);
            ProprietarioClientOptions options = configurationSection.Get<ProprietarioClientOptions>() ?? new();

             services.AddHttpClient<IClientHttp, ClientHttp>(o => {
                o.BaseAddress = new Uri(options.BaseAddress);
            }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            return services;
        }

    }
}

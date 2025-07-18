using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Registrazioni.ClientHttp.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrazioni.ClientHttp.DepencyInjection
{
    public static class RegistrazioniClientExtensions
    {
        public static IServiceCollection AddRegistrazioniClient(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection configurationSection = configuration.GetSection(RegistrazioniClientOptions.SectionName);
            RegistrazioniClientOptions opt = configurationSection.Get<RegistrazioniClientOptions>() ?? new();

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

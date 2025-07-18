using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidici.Business.Kafka
{
    public class OutboxHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OutboxHostedService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(2);

        public OutboxHostedService(IServiceProvider serviceProvider, ILogger<OutboxHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Servizio Outbox avviato.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var processor = scope.ServiceProvider.GetRequiredService<TransactionalOutboxProcessor>();

                    await processor.ProcessAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Errore durante l'esecuzione del processor.");
                }

                await Task.Delay(_interval, stoppingToken);
            }

            _logger.LogInformation("Servizio Outbox terminato.");
        }
    }

}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Registrazioni.Repository;
using Registrazioni.Repository.Abstraction;
using Registrazioni.Repository.Model;
using Registrazioni.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Registrazioni.Business.kafka.MessageHandler
{
    public class TransactionalOutboxProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<TransactionalOutboxProcessor> _logger;

        public TransactionalOutboxProcessor(IServiceScopeFactory serviceScopeFactory, ILogger<TransactionalOutboxProcessor> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Avvio del servizio TransactionalOutboxProcessor");
            while (!stoppingToken.IsCancellationRequested)
            {
                await using var scope = _serviceScopeFactory.CreateAsyncScope();
                var repository = scope.ServiceProvider.GetRequiredService<IRepository>();

                var messages = await repository.GetAllTransactionalOutbox(stoppingToken);
                foreach (var msg in messages)
                {
                    try
                    {
                        var dto = JsonSerializer.Deserialize<VotazioneDto>(msg.Messaggio);
                        var entity = new Votazione
                        {
                            IdGiudice = dto.IdGiudice,
                            IdCane = dto.IdCane,
                            Voto = dto.Voto
                        };

                        await repository.AddVotazione(entity, stoppingToken);
                        await repository.DeleteTransactionalOutbox(msg.Id, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Errore durante la scrittura da outbox a Votazioni");
                    }
                }

                await repository.SaveChangesAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken); // Attendi prima del prossimo ciclo
            }


        }
        public async Task ProcessAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Avvio del servizio TransactionalOutboxProcessor");
            while (!stoppingToken.IsCancellationRequested)
            {
                await using var scope = _serviceScopeFactory.CreateAsyncScope();
                var repository = scope.ServiceProvider.GetRequiredService<IRepository>();

                var messages = await repository.GetAllTransactionalOutbox(stoppingToken);
                foreach (var msg in messages)
                {
                    try
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            WriteIndented = true
                        };
                        var dto = JsonSerializer.Deserialize<VotazioneDto>(msg.Messaggio,options);
                        var entity = new Votazione
                        {
                            IdGiudice = dto.IdGiudice,
                            IdCane = dto.IdCane,
                            Voto = dto.Voto
                        };

                        await repository.AddVotazione(entity, stoppingToken);
                        await repository.DeleteTransactionalOutbox(msg.Id, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Errore durante la scrittura da outbox a Votazioni");
                    }
                }

                await repository.SaveChangesAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken); // Attendi prima del prossimo ciclo
            }
        }




    }

}

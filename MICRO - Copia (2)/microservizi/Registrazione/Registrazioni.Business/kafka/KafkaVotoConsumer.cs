using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Registrazioni.Business.kafka.kafkaAbstract;
using Registrazioni.Business.kafka.MessageHandler;
using Registrazioni.Business.Kafka;
using Registrazioni.Repository.Abstraction;
using Registrazioni.Repository.Model;
using Registrazioni.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Registrazioni.Business.kafka
{
    public class KafkaVotoConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ConsumerConfig _config;

        public KafkaVotoConsumer(IServiceScopeFactory scopeFactory, ConsumerConfig config)
        {
            _scopeFactory = scopeFactory;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
            consumer.Subscribe("votazione");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(stoppingToken); // Consuma il messaggio Kafka

                    using var scope = _scopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<IRepository>();

                    var outbox = new TransactionalOutbox
                    {
                        Tabella = "Votazioni",
                        Messaggio = result.Message.Value
                    };

                    await db.AddTransactionalOutbox(outbox, stoppingToken);
                    await db.SaveChangesAsync(stoppingToken);

                    // Ora chiama il processor per processare l'outbox
                    var outboxProcessor = scope.ServiceProvider.GetRequiredService<TransactionalOutboxProcessor>();

                    await outboxProcessor.ProcessAsync(stoppingToken);  // metodo da implementare per processare tutti gli outbox o il nuovo

                }
                catch (ConsumeException ex)
                {
                    Console.WriteLine($"Errore Kafka: {ex.Error.Reason}");
                }
                catch (OperationCanceledException)
                {
                    // Normale chiusura
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore generico consumer: {ex.Message}");
                }

                // Piccola pausa per evitare loop troppo veloce
                await Task.Delay(100, stoppingToken);
            }

            consumer.Close();
        }

    }




}

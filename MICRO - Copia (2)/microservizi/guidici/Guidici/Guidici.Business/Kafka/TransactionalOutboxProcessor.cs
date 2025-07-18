using Guidici.Repository.Abstraction;
using Guidici.Repository.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidici.Business.Kafka
{
    public class TransactionalOutboxProcessor
    {
        private readonly ILogger<TransactionalOutboxProcessor> _logger;
        private readonly IRepository _repository;
        private readonly IProducerClient<string, string> _producerClient;
        private readonly KafkaTopics _topics;

        public TransactionalOutboxProcessor(
            ILogger<TransactionalOutboxProcessor> logger,
            IRepository repository,
            IProducerClient<string, string> producerClient,
            IOptions<KafkaTopics> optionsTopics)
        {
            _logger = logger;
            _repository = repository;
            _producerClient = producerClient;
            _topics = optionsTopics.Value;
        }

        public async Task ProcessAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Controllo messaggi nella transactional outbox...");

            var messages = await _repository.GetAllTransactionalOutbox(cancellationToken);
            if (!messages.Any())
            {
                _logger.LogInformation("Nessun messaggio da elaborare.");
                return;
            }

            foreach (var msg in messages)
            {
                try
                {
                    string topic = msg.Tabella switch
                    {
                        nameof(VotazioneKafka) => _topics.Votazione,
                        _ => throw new InvalidOperationException($"Topic non gestito: {msg.Tabella}")
                    };

                    _logger.LogInformation("Invio messaggio con ID {Id} al topic {Topic}", msg.Id, topic);

                    await _producerClient.ProduceAsync(topic, msg.Id.ToString(), msg.Messaggio, cancellationToken);

                    await _repository.DeleteTransactionalOutbox(msg.Id, cancellationToken);
                    await _repository.SaveChangesAsync(cancellationToken);

                    _logger.LogInformation("Messaggio con ID {Id} inviato e rimosso.", msg.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Errore durante l'invio del messaggio con ID {Id}", msg.Id);
                }
            }
        }
    }

}

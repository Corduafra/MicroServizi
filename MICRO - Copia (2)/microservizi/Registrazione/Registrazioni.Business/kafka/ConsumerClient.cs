using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Registrazioni.Business.kafka.kafkaAbstract;
using Registrazioni.Business.Kafka;
using System.Net;
using System.Text.Json;





namespace Registrazioni.Business.kafka
{
    public class KafkaConsumerClient : IConsumerClient<string, string> { 
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger<KafkaConsumerClient> _logger;

        public KafkaConsumerClient(
        IOptions<KafkaSettings> kafkaOptions,
        ILogger<KafkaConsumerClient> logger)
        {
           
            _logger = logger;
            _consumer = new ConsumerBuilder<string, string>(GetConsumerConfig(kafkaOptions)).Build();
        }

        private ConsumerConfig GetConsumerConfig(IOptions<KafkaSettings> options)
        {
            ConsumerConfig consumerConfig = new();
            consumerConfig.BootstrapServers = options.Value.BootstrapServers;
            consumerConfig.GroupId = options.Value.GroupId;
            consumerConfig.ClientId = Dns.GetHostName();
            consumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;
            consumerConfig.EnableAutoCommit = false;
            consumerConfig.AutoCommitIntervalMs = 0;
            consumerConfig.AllowAutoCreateTopics = false;
            //consumerConfig.EnableAutoOffsetStore = false;
            
            _logger.LogInformation("Kafka ConsumerConfig: {consumerConfig}", JsonSerializer.Serialize(consumerConfig));

            return consumerConfig;
        }

        public void Subscribe(IEnumerable<string> topics)
        {
            try
            {
                _logger.LogInformation("Sottoscrizione ai seguenti topic: '{topics}'...", string.Join("', '", topics));
                _consumer.Subscribe(topics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception sollevata all'interno del metodo {methodName}. Exception Message: {message}", nameof(Subscribe), ex.Message);
                throw;
            }
            _logger.LogInformation("Sottoscrizione completata!");
        }

        /// <inheritdoc/>
        public void Subscribe(string topic)
        {
            Subscribe([topic]);
        }

        public async Task ConsumeInLoopAsync(
            string topic,
            Func<ConsumeResult<string, string>, Task> handleMessage,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("START Kafka ConsumeInLoopAsync");

            Subscribe("votazione");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(cancellationToken);
                    if (result?.Message != null)
                    {
                        _logger.LogInformation("Kafka msg: {0} -> {1}", result.Message.Key, result.Message.Value);
                        await handleMessage(result);
                        _consumer.Commit(result);
                    }
                }
                catch (ConsumeException e)
                {
                    _logger.LogError(e, "Errore consumo Kafka");
                }
            }
        }

        public void Dispose() => _consumer?.Dispose();

        public void Commit(ConsumeResult<string, string>? result)
        {
            try
            {
                if (result != null)
                {
                    _logger.LogDebug("Commit offset: {result}", JsonSerializer.Serialize(result));
                    _consumer.Commit(result);
                    //_consumer.StoreOffset(result);
                }
            }
            catch (TopicPartitionOffsetException ex)
            {
                _logger.LogCritical(ex, "TopicPartitionOffsetException sollevata all'interno del metodo {methodName}: {reason}", nameof(Commit), ex.Error.Reason);
                throw;
            }
            catch (KafkaException ex)
            {
                _logger.LogCritical(ex, "KafkaException sollevata all'interno del metodo {methodName}: {reason}", nameof(Commit), ex.Error.Reason);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Exception sollevata all'interno del metodo {methodName}: {message}", nameof(Commit), ex.Message);
                throw;
            }
        }
    }

   
    



}


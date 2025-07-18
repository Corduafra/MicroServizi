using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidici.Business.Kafka
{
    public class ProducerClient : IProducerClient<string, string>
    {
        private readonly IProducer<string, string> _producer;

        public ProducerClient()
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task ProduceAsync(string topic, string key, string message, CancellationToken cancellationToken)
        {
            await _producer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = message }, cancellationToken);
        }

        public async Task ProduceAsync(string topic, int partition, string key, string message, CancellationToken cancellationToken = default)
        {
            var msg = new Message<string, string> { Key = key, Value = message };
            var topicPartition = new TopicPartition(topic, new Partition(partition));
            await _producer.ProduceAsync(topicPartition, msg, cancellationToken);
        }

        public void Dispose()
        {
            _producer?.Dispose();
        }
    }
}

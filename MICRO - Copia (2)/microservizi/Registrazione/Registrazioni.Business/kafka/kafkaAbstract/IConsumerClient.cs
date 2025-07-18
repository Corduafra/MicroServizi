using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrazioni.Business.kafka.kafkaAbstract
{
    public interface IConsumerClient<TKey, TValue>
    {

        void Subscribe(string topic);
        Task ConsumeInLoopAsync(
            string topic,
            Func<ConsumeResult<TKey, TValue>, Task> handleMessage,
            CancellationToken cancellationToken = default);
    }

}

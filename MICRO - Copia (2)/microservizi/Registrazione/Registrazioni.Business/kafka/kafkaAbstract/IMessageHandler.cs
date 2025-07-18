using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrazioni.Business.kafka.kafkaAbstract
{
    public interface IMessageHandler<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        Task OnMessageReceivedAsync(TKey key, TValue value, CancellationToken cancellationToken = default);
    }

    public interface IMessageHandlerFactory<TKey, TValue>
    where TKey : class
    where TValue : class
    {
        IMessageHandler<TKey, TValue> Create(string topic, IServiceProvider serviceProvider);
    }

}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Registrazioni.Business.kafka.kafkaAbstract;

using Registrazioni.Repository.Abstraction;
using Registrazioni.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrazioni.Business.kafka.MessageHandler
{
    public class MessageHandler : IMessageHandler<string, string>
    {
        private readonly IRepository _repository;
        private readonly ILogger<MessageHandler> _logger;

        public MessageHandler(IRepository repository, ILogger<MessageHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnMessageReceivedAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Salvataggio nel transactional outbox");

            var entry = new TransactionalOutbox
            {
                Messaggio = value,
                Tabella = "Votazioni",
                
            };

            await _repository.AddTransactionalOutbox(entry, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }

}

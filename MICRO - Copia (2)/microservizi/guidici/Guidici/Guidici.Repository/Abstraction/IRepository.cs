using Guidici.Repository.Model;
using Guidici.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidici.Repository.Abstraction
{
    public interface IRepository
    {

        Task BeginTransactionAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken c = default);
        Task CreateGuidiceAsync(string nome, string cognome, string codiceFiscale, CancellationToken c = default);

        Task <Persona?> GetGuidice(int id, CancellationToken c = default); 


        //comunicazione kakfa
       // Task<List<VotazioneKafka>> GetVotazioneKafkas(VotazioneDto votazioneDto,CancellationToken c = default);
        Task<VotazioneKafka> InsertVotazioneKafka(VotazioneDto v,CancellationToken c = default);


        #region TransactionalOutbox

        Task <IEnumerable<TransactionalOutbox>> GetAllTransactionalOutbox(CancellationToken c = default);

        Task <TransactionalOutbox?> GetTransactionalOutboxByKey(long id, CancellationToken c = default);

        Task DeleteTransactionalOutbox(long id, CancellationToken c = default);

        Task InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox, CancellationToken c = default);

        #endregion
    }
}

using Registrazioni.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrazioni.Repository.Abstraction
{
    public interface IRepository
    {
        Task<Cane?> GetByIdAsync(int id,CancellationToken c=default);
        Task<int> SaveChangesAsync(CancellationToken c=default);
        Task CreateCane(string nome, string razza, CancellationToken c=default);

        Task<IEnumerable<TransactionalOutbox>> GetAllTransactionalOutbox(CancellationToken c = default);
        IEnumerable<TransactionalOutbox> GetTransactionalOutboxes();
        TransactionalOutbox? GetTransactionalOutbox(long id, CancellationToken cancellationToken = default);
        Task AddTransactionalOutbox(TransactionalOutbox transactionalOutbox, CancellationToken cancellationToken);
        Task DeleteTransactionalOutbox(long id, CancellationToken cancellationToken = default);
        Task AddVotazione(Votazione votazione, CancellationToken cancellationToken=default);

        Task<Votazione?> GetVotazione(int id, CancellationToken cancellationToken=default);


    }
}

using Microsoft.EntityFrameworkCore;
using Registrazioni.Repository.Abstraction;
using Registrazioni.Repository.Model;

namespace Registrazioni.Repository
{
    public class Repository (RegistrazioniDbContext registrazioniDbContext) : IRepository
    {
        public async Task<Cane?> GetByIdAsync(int id,CancellationToken c=default)
        {

            return await registrazioniDbContext.Cani.FindAsync(id);
        }

        public async Task CreateCane(string nome, string razza, CancellationToken c=default)
        {
            Cane cane = new Cane
            {
                Nome = nome,
                Razza = razza,
               

            };

            await registrazioniDbContext.Cani.AddAsync(cane,c);
           
        }

        public async Task<int> SaveChangesAsync(CancellationToken c=default)
        {
             return await registrazioniDbContext.SaveChangesAsync(c);
        }

        public async Task AddVotazione(Votazione votazione, CancellationToken c=default)
        {
            await registrazioniDbContext.Votazioni.AddAsync(votazione,c);
        }

        public async Task<Votazione?> GetVotazione(int id, CancellationToken cancellationToken = default)
        {
            return await registrazioniDbContext.Votazioni.FindAsync(id);
        }



        public  IEnumerable<TransactionalOutbox> GetTransactionalOutboxes() => registrazioniDbContext.TransactionalOutboxList.ToList();
        
        public TransactionalOutbox? GetTransactionalOutbox(long id, CancellationToken cancellationToken = default)
        {
            return registrazioniDbContext.TransactionalOutboxList.FirstOrDefault(t => t.Id == id);
        }
        public async Task AddTransactionalOutbox(TransactionalOutbox transactionalOutbox)
        {
            registrazioniDbContext.TransactionalOutboxList.Add(transactionalOutbox);
        }
        public async Task DeleteTransactionalOutbox(long id, CancellationToken cancellationToken=default)
        {
            registrazioniDbContext.TransactionalOutboxList.Remove(GetTransactionalOutbox(id) ?? 
                throw new ArgumentException("TransactionalOutbox con id {id} non trovato", nameof(id)));
        }

        public async Task<IEnumerable<TransactionalOutbox>> GetAllTransactionalOutbox(CancellationToken c = default)
        {
            return await registrazioniDbContext.TransactionalOutboxList.ToListAsync(c);
        }

        public async Task AddTransactionalOutbox(TransactionalOutbox transactionalOutbox, CancellationToken cancellationToken)
        {
            registrazioniDbContext.TransactionalOutboxList.Add(transactionalOutbox);
        }
    }
}

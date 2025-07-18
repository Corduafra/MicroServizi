using Guidici.Repository.Abstraction;
using Guidici.Repository.Model;
using Guidici.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Guidici.Repository;


public class Repository(GuidiciDbContext guidiciDbContext) : IRepository
    {
    public async Task BeginTransactionAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default)
    {
        if (guidiciDbContext.Database.CurrentTransaction != null)
        {
            // La connessione è già in una transazione
            await action(cancellationToken);
        }
        else
        {
            // Viene avviata una transazione 
            await using IDbContextTransaction transaction = await guidiciDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await action(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }





    public async Task<int> SaveChangesAsync(CancellationToken c=default)
        {
            return await guidiciDbContext.SaveChangesAsync(c);
        }

        public async Task CreateGuidiceAsync(string nome, string cognome, string codiceFiscale, CancellationToken c=default)
        {
            Persona persona = new Persona();
            persona.Nome = nome;
            persona.Cognome = cognome;
            persona.CodiceFiscale = codiceFiscale;

            await guidiciDbContext.Persone.AddAsync(persona,c);

        }

        public async Task<Persona?> GetGuidice(int id, CancellationToken c = default)
        {
            return await guidiciDbContext.Persone.Where(p => p.Id == id).FirstOrDefaultAsync(c);
        }


        public async Task<List<VotazioneKafka>> GetVotazioneKafkas(CancellationToken c = default)
        {
            return await guidiciDbContext.VotazioniKafka.ToListAsync(c);
        }


        public async Task<VotazioneKafka> InsertVotazioneKafka(VotazioneDto v, CancellationToken c = default)
        {
            VotazioneKafka votazioneKafka = new VotazioneKafka();
            votazioneKafka.IdCane = v.IdCane;
            votazioneKafka.IdGiudice = v.IdGiudice;
            votazioneKafka.Voto = v.Voto;

            await guidiciDbContext.VotazioniKafka.AddAsync(votazioneKafka,c);
            return votazioneKafka;
        }




    public async Task<IEnumerable<TransactionalOutbox>> GetAllTransactionalOutbox(CancellationToken c = default)
    {
       return await guidiciDbContext.TransactionalOutboxList.ToListAsync(c);
    }

    public async Task <TransactionalOutbox?> GetTransactionalOutboxByKey(long id, CancellationToken c = default)
        {
        return  guidiciDbContext.TransactionalOutboxList.Find(id, c);
        }

        public async Task DeleteTransactionalOutbox(long id, CancellationToken c = default)
        {
            var transactionalOutbox = guidiciDbContext.TransactionalOutboxList.Find(id);
            guidiciDbContext.TransactionalOutboxList.Remove(transactionalOutbox);
        }

        public async Task InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox, CancellationToken c=default)
        {
            guidiciDbContext.TransactionalOutboxList.Add(transactionalOutbox);
        }
        
    }
    

    

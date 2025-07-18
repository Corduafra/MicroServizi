using Proprietario.Repository.Abstraction;
using Proprietario.Repository.Model;
using Proprietario.Shared;

namespace Proprietario.Repository
{
    public class Repository(ProprietarioDbContext proprietarioDbContext) : IRepository
    {
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await proprietarioDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Soggetto?> GetProprietario(int id, CancellationToken cancellationToken = default)
        {
            return await proprietarioDbContext.Soggetti.FindAsync( id , cancellationToken);
        }

        public async Task CreateSoggetto(SoggettoDto soggettoDto, CancellationToken cancellationToken = default)
        {
            Soggetto soggetto = new Soggetto()
            { Nome = soggettoDto.Nome, Cognome = soggettoDto.Cognome, Cf = soggettoDto.Cf };
            await proprietarioDbContext.Soggetti.AddAsync(soggetto, cancellationToken);
        }

        //associare il cane al suo proprietario
        public async Task CaneProprietario(CaneProprietarioDto caneProprietarioDto, CancellationToken c=default)
        {
            CaneProprietario caneProprietario = new CaneProprietario { IdCane = caneProprietarioDto.IdCane, IdProprietario = caneProprietarioDto.IdProprietario };
            await proprietarioDbContext.CaneProprietari.AddAsync(caneProprietario, c);

        }

    }
}

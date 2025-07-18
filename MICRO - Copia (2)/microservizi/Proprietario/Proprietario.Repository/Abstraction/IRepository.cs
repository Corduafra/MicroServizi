using Proprietario.Repository.Model;
using Proprietario.Shared;

namespace Proprietario.Repository.Abstraction
{
    public interface IRepository
    {

        Task<Soggetto?> GetProprietario(int id,CancellationToken c=default);

        Task CreateSoggetto(SoggettoDto soggettoDto, CancellationToken c = default);

      

        Task<int> SaveChangesAsync(CancellationToken c = default);

        Task CaneProprietario(CaneProprietarioDto caneProprietarioDto, CancellationToken c = default);
    }
}

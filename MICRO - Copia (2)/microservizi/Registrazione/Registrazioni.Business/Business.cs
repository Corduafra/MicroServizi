using Microsoft.Extensions.Logging;
using Registrazioni.Business.Abstraction;
using Registrazioni.Repository.Abstraction;
using Registrazioni.Shared;

namespace Registrazioni.Business
{
    public class Business(IRepository repository, ILogger<Business> logger): IBusiness
    {
        public async Task CreateCaneAsync(CaneDto caneDto, CancellationToken c = default)
        {
            await repository.CreateCane(caneDto.Nome,caneDto.Razza, c);
            await repository.SaveChangesAsync(c);

        }
        public async Task<CaneDto?> GetCaneDtoAsync(int id, CancellationToken c = default)
        {
            var cane = await repository.GetByIdAsync(id,c);
            if (cane == null)
            {
                logger.LogWarning("Cane {id} not found", id);
                return null;
            }
            return new CaneDto
            {
                Nome = cane.Nome,
                
                Razza = cane.Razza,
            };
        }



    }
}

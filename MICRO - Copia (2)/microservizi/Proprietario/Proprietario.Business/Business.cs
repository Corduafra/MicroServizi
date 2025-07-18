using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Proprietario.Business.Abstraction;
using Proprietario.Repository.Abstraction;
using Proprietario.Shared;
using Registrazioni.Shared;
using System.Globalization;
using System.Net.Http.Json;

namespace Proprietario.Business
{
    public class Business(IRepository repository, Registrazioni.ClientHttp.Abstraction.IClientHttp clientHttp,ILogger<Business> logger) : IBusiness
    {

        public async Task<SoggettoDto?> GetProprietario(int id, CancellationToken c = default)
        {
            var soggetto = await repository.GetProprietario(id, c);

            if (soggetto == null)
            {
                logger.LogWarning("Soggetto non trovato");
                return null;
            }

            return new SoggettoDto { Nome = soggetto.Nome, Cognome = soggetto.Cognome, Cf = soggetto.Cf };
           
        }


        public async Task CreateSoggetto(SoggettoDto soggettoDto, CancellationToken c = default)
        {
            await repository.CreateSoggetto(soggettoDto, c);
            await repository.SaveChangesAsync(c);


        }

        //associo il cane al proprietario
        public async Task CaneProprietario(CaneProprietarioDto caneProprietarioDto, CancellationToken c = default)
        {
            CaneDto? cane = await clientHttp.GetCaneDtoAsync(caneProprietarioDto.IdCane, c);

            if (cane is null)
                throw new Exception($"Cane non trovato con id:{caneProprietarioDto.IdCane} ");

            await repository.CaneProprietario(caneProprietarioDto, c);
            await repository.SaveChangesAsync(c);
          
/*
            var queryString = QueryString.Create(new Dictionary<string, string?>() {
                {"idCane",caneProprietarioDto.IdCane.ToString(CultureInfo.InvariantCulture) },
                

                });
            
            var response = await clientHttp.GetAsync($"https://localhost:7137/Cane/GetCane{queryString}", c);
            
            var cane = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<CaneDto?>(cancellationToken: c);
            Console.WriteLine(cane);

            if (cane is null)
                throw new Exception($"Cane non trovato con id:{caneProprietarioDto.IdCane} ");

            await repository.CaneProprietario(caneProprietarioDto, c);
            await repository.SaveChangesAsync(c);
*/
            

        }

    }
}

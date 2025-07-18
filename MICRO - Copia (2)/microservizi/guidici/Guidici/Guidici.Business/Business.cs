using Guidici.Business.Abstraction;
using Guidici.Repository.Abstraction;

using Guidici.Shared;
using Microsoft.Extensions.Logging;

using AutoMapper;
using Guidici.Business.Factory;

namespace Guidici.Business;

    public class Business(IRepository repository, ILogger<Business> logger, IMapper map) : IBusiness

    {
      
        
        public async Task CreateVotazione(VotazioneDto votazioneDto, CancellationToken c = default)
        {
        await repository.BeginTransactionAsync(async (CancellationToken c) =>
        {
            var votazione = await repository.InsertVotazioneKafka(votazioneDto, c);
            await repository.SaveChangesAsync(c);
            var newVotazione = map.Map<VotazioneDto>(votazione);
            await repository.InsertTransactionalOutbox(TransactionalOutboxFactory.CreateInsert(newVotazione), c);

            await repository.SaveChangesAsync(c);

        }, c);

            

            
        }





        //logica per ottenere un guidice
        public async Task<PersonaDto?> GetGuidice(int id, CancellationToken c = default)
        {
            var persona = await repository.GetGuidice(id, c);

            if (persona is null)
                return null;

            return new PersonaDto
            {
               
                Nome = persona.Nome,
                Cognome = persona.Cognome,
                CodiceFiscale = persona.CodiceFiscale
            };
        }

    //logica per creare un guidice
    public async Task CreateGuidiceAsync(PersonaDto personaDto, CancellationToken c = default)
    {
        await repository.CreateGuidiceAsync(personaDto.Nome, personaDto.Cognome, personaDto.CodiceFiscale, c);

        await repository.SaveChangesAsync(c);
    }


}

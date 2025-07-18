using Guidici.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidici.Business.Abstraction
{
    public interface IBusiness
    {
        Task CreateGuidiceAsync(PersonaDto personaDto, CancellationToken c = default);
        Task<PersonaDto?> GetGuidice(int id, CancellationToken c = default);

        Task CreateVotazione(VotazioneDto votazioneDto, CancellationToken c = default);

        
    }
}

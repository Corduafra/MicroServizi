using Guidici.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidici.ClientHttp.Abstraction
{
    public  interface IClientHttp
    {
        
        Task<string?> CreateGuidice(PersonaDto personaDto, CancellationToken c = default);
        Task<PersonaDto?> GetGuidice(int id, CancellationToken c = default);
        
    }
}

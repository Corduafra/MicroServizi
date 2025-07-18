using Registrazioni.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrazioni.ClientHttp.Abstraction
{
    public interface IClientHttp
    {
        Task<string?> CreateCaneAsync(CaneDto caneDto, CancellationToken c = default);
        Task<CaneDto?> GetCaneDtoAsync(int id, CancellationToken c = default);
    }
}

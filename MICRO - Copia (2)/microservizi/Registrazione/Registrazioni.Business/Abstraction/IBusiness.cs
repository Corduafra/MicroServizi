using Registrazioni.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrazioni.Business.Abstraction
{
    public  interface IBusiness
    {
        Task CreateCaneAsync(CaneDto caneDto, CancellationToken c = default);
        Task<CaneDto?> GetCaneDtoAsync(int id, CancellationToken c = default);


    }
}

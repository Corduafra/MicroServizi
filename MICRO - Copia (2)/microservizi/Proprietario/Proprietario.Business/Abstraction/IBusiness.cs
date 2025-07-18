using Proprietario.Repository.Model;
using Proprietario.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proprietario.Business.Abstraction
{
    public interface IBusiness
    {
        Task<SoggettoDto?> GetProprietario(int id, CancellationToken c = default);

        Task CreateSoggetto(SoggettoDto soggettoDto, CancellationToken c = default);

        Task CaneProprietario(CaneProprietarioDto caneProprietarioDto, CancellationToken c = default);


    }
}

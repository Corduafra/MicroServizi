using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrazioni.Shared
{
    public class VotazioneDto
    {
        public int Id { get; set; }
        public int IdGiudice { get; set; }
        public int IdCane { get; set; }
        public int Voto { get; set; }
    }
}


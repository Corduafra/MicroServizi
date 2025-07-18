using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proprietario.ClientHttp.DepencyInjection
{
    public class ProprietarioClientOptions
    {
        public const string SectionName = "ProrprietarioClientHttp";
        public string BaseAddress { get; set; } = "";

    }
}

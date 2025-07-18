using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrazioni.ClientHttp.DepencyInjection
{
    public class RegistrazioniClientOptions
    {
        /// <summary>
        /// Nome sezione nel file di configurazione "appsettings.json".
        /// </summary>
        public const string SectionName = "RegistrazioniClientHttp";

        /// <summary>
        /// Base URL di destinazione.
        /// </summary>
        public string BaseAddress { get; set; } = "";
    }
}

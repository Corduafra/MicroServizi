
namespace Guidici.Repository.Model

{
    public class TransactionalOutbox
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// ID del Giudice che ha effettuato la votazione
        /// </summary>
       // public int IdGiudice { get; set; }

        /// <summary>
        /// ID del Cane votato
        /// </summary>
       // public  int IdCane { get; set; } 

        /// <summary>
        /// Voto assegnato dal Giudice al Cane (ad esempio, da 1 a 10)
        /// </summary>
       // public int Voto { get; set; }

        
        /// <summary>
        /// Nome della tabella modificata
        /// </summary>
        public string Tabella { get; set; } = "Votazioni";

        /// <summary>
        /// Messaggio JSON di tipo OperationMessage contenente il record inserito/modificato/eliminato
        /// </summary>
        public string Messaggio { get; set; } = string.Empty;
    }
}

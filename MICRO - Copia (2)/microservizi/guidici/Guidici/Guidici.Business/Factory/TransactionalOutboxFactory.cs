using Guidici.Repository.Model;
using Guidici.Shared;
using System.Text.Json;



namespace Guidici.Business.Factory
{
    public class TransactionalOutboxFactory
    {
        #region Votazioni
        /* public static TransactionalOutbox CreateVotazione(VotazioneDto votazioneDto)
         {
             return new TransactionalOutbox
             {
                 Id = Guid.NewGuid().ToString(),
                 Type = "Votazione",
                 Payload = JsonSerializer.Serialize(votazioneDto),
                 CreatedAt = DateTime.UtcNow
             };
         }
        */
        private static TransactionalOutbox Create(VotazioneDto dto, string operation) => Create(nameof(VotazioneKafka), dto, operation);

        private static TransactionalOutbox Create<TDTO>(string table, TDTO dto, string operation) where TDTO : class, new()
        {
            OperationMessage<TDTO> opMsg = new OperationMessage<TDTO>()
            {
               Operation = operation,
               Dto = dto
            };
            opMsg.Validate();
            return new TransactionalOutbox()
            {
                
                Tabella = table,
                Messaggio = JsonSerializer.Serialize(opMsg),
              
            };
        }

        public static TransactionalOutbox CreateInsert(VotazioneDto dto) => Create(dto, Operations.Insert);
        #endregion
    }

    internal class OperationMessage<TDTO>
    {
        /// <summary>
        /// Operazione da eseguire. <br/>
        /// Dominio valori: <see cref="Operations"/>
        /// </summary>
        public required string Operation { get; set; } = default!;
        public required TDTO Dto { get; set; } = default!;

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Operation))
                throw new InvalidOperationException("Operation must be specified.");

            if (Dto == null)
                throw new InvalidOperationException("DTO cannot be null.");
        }
    }
}

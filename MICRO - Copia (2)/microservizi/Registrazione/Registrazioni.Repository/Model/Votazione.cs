

namespace Registrazioni.Repository.Model
{
    public class Votazione
    {

        public int Id { get; set; }
        public int IdGiudice { get; set; }
        public int IdCane { get; set; }
        public int Voto { get; set; }
    }

}


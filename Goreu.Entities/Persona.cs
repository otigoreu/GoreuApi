using Goreu.Entities.Pide;

namespace Goreu.Entities
{
    public class Persona : EntityBase
    {
        public string Nombres { get; set; } = default!;
        public string ApellidoPat { get; set; } = default!;
        public string ApellidoMat { get; set; } = default!;
        public DateTime FechaNac { get; set; }
        public string Email { get; set; } = default!;
        public int IdTipoDoc { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public  string NroDoc { get; set; } = default!;
        
        // Relación uno a muchos
        public ICollection<Usuario> Usuarios { get; set; }
        public CredencialReniec CredencialReniec { get; set; }
    }
}

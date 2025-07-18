namespace Goreu.Entities.Pide
{
    public class CredencialReniec:EntityBase
    {
        public string nuDniUsuario { get; set; }
        public string nuRucUsuario { get; set; }
        public string password { get; set; }

        public DateTime fechaRegistro { get; set; }
        public Guid UsuarioID { get; set; }

        public int PersonaID { get; set; }
        public Persona Persona { get; set; }
    }
}

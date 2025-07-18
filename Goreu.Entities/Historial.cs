namespace Goreu.Entities
{
    public class Historial : EntityBase
    {
        public DateTime FechaRegistro { get; set; }
        public string operacion { get; set; }
        public int ID_pk { get; set; }

        public int idIndiceTabla { get; set; }
        public IndiceTabla IndiceTabla { get; set; }

        public string idUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}

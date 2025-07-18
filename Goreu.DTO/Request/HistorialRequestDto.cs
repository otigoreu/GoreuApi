namespace Goreu.Dto.Request
{
    public class HistorialRequestDto
    {
        public DateTime FechaRegistro { get; set; }
        public string operacion { get; set; }
        public int ID_pk { get; set; }

        public int idIndiceTabla { get; set; }
        public string idUsuario { get; set; }
    }
}

namespace Goreu.Dto.Response
{
    public class UnidadOrganicaResponseSingleDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string? NombreEntidad { get; set; }
        public string? NombreDependencia { get; set; }
        public int CantidadHijos { get; set; }
        public bool Estado { get; set; }

        public int idEntidad { get; set; }
        public int? idDependencia { get; set; }

    }
}

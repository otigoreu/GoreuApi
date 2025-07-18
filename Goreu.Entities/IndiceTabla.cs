namespace Goreu.Entities
{
    public class IndiceTabla : EntityBase
    {
        public string Descripcion { get; set; }

        public ICollection<Historial> Historials { get; set; } = new List<Historial>();
    }
}

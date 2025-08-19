namespace Goreu.Dto.Request
{
    public class RolRequestDto
    {
        public string Name { get; set; } = default!;
        public char Nivel { get; set; }= default!;
        public string NormalizedName { get; set; } = default!;
    }
}

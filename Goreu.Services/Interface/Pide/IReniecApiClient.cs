namespace Goreu.Services.Interface.Pide
{
    public interface IReniecApiClient
    {
        Task<XDocument> ConsultarAsync(string dniConsulta, string dniUsuario, string rucUsuario, string password);
        Task<bool> ActualizarPasswordAsync(string dniUsuario, string rucUsuario, string oldPassword, string newPassword);
    }
}

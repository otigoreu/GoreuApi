namespace Goreu.Services.Implementation.Pide
{
    public class ReniecApiClient : IReniecApiClient
    {
        private readonly HttpClient _httpClient;

        public ReniecApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<XDocument> ConsultarAsync(string dniConsulta, string dniUsuario, string rucUsuario, string password)
        {
            var payload = new
            {
                PIDE = new
                {
                    nuDniConsulta = dniConsulta,
                    nuDniUsuario = dniUsuario,
                    nuRucUsuario = rucUsuario,
                    password = password
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://ws2.pide.gob.pe/Rest/RENIEC/Consultar", content);

            response.EnsureSuccessStatusCode();

            var xmlContent = await response.Content.ReadAsStringAsync();
            return XDocument.Parse(xmlContent);
        }

        public async Task<bool> ActualizarPasswordAsync(string dniUsuario, string rucUsuario, string oldPassword, string newPassword)
        {
            var payload = new
            {
                PIDE = new
                {
                    credencialAnterior = oldPassword,
                    credencialNueva = newPassword,
                    nuDni = dniUsuario,
                    nuRuc = rucUsuario
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://ws2.pide.gob.pe/Rest/RENIEC/Actualizar?out=json", content);

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadAsStringAsync();
            return result.Contains("0000");
        }
    }
}

using AutoMapper;
using Azure;
using Goreu.Dto.Request.Pide;
using Goreu.Dto.Request.Pide.Credenciales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Goreu.Services.Implementation.Pide
{
    public class ReniecService : IReniecService
    {
        private readonly IReniecApiClient _reniecApiClient;
        private readonly ICredencialReniecService _credencialReniecService;
        private readonly IMapper _mapper;

        public ReniecService(IReniecApiClient reniecApiClient, ICredencialReniecService credencialReniecService, IMapper mapper)
        {
            _reniecApiClient = reniecApiClient;
            _credencialReniecService = credencialReniecService;
            _mapper = mapper;
        }

        public async Task<ReniecResponseModel> ConsultarPersonaAsync(GetReniecRequest request)
        {
            // 1️⃣ Obtener credenciales locales
            var credencialResult = await _credencialReniecService.GetByNumdocAsync(request.nuDniUsuario);
            if (!credencialResult.Success || credencialResult.Data == null)
                throw new Exception("Credencial RENIEC no encontrada.");

            string password = credencialResult.Data.password;
            string ruc = credencialResult.Data.nuRucUsuario;

            // 2️⃣ Primera consulta
            var xdoc = await _reniecApiClient.ConsultarAsync(request.nuDniConsulta, request.nuDniUsuario, ruc, password);
            var (codigo, mensaje, datosPersona) = ParseResponse(xdoc);

            // 3️⃣ Si contraseña caducada (1002)
            if (codigo == "1002")
            {
                var nuevaPassword = Guid.NewGuid().ToString("N")[..10];

                var actualizado = await _reniecApiClient.ActualizarPasswordAsync(request.nuDniUsuario, ruc, password, nuevaPassword);
                if (!actualizado)
                    throw new Exception("No se pudo actualizar la contraseña en RENIEC.");

                // Guardar nueva contraseña en BD
                credencialResult.Data.password = nuevaPassword;

                await _credencialReniecService.UpdateAsync(credencialResult.Data.Id, _mapper.Map<AddCredencialReniecRequestDto>(credencialResult.Data));

                // Reintentar consulta con nueva contraseña
                xdoc = await _reniecApiClient.ConsultarAsync(request.nuDniConsulta, request.nuDniUsuario, ruc, nuevaPassword);
                (codigo, mensaje, datosPersona) = ParseResponse(xdoc);
            }

            // 4️⃣ Validar resultado final
            if (codigo != "0000")
                throw new Exception($"RENIEC devolvió error {codigo}: {mensaje}");

            return datosPersona;
        }

        private (string codigo, string mensaje, ReniecResponseModel datosPersona) ParseResponse(XDocument xdoc)
        {
            XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace reniec = "http://ws.reniec.gob.pe/";

            var resultElement = xdoc
                .Element(soap + "Envelope")?
                .Element(soap + "Body")?
                .Element(reniec + "consultarResponse")?
                .Element("return");

            if (resultElement == null)
                throw new Exception("Respuesta de RENIEC inválida.");

            var codigo = resultElement.Element("coResultado")?.Value;
            var mensaje = resultElement.Element("deResultado")?.Value;

            var datos = resultElement.Element("datosPersona");
            if (datos == null)
                return (codigo, mensaje, null);

            var persona = new ReniecResponseModel
            {
                Nombres = datos.Element("prenombres")?.Value,
                ApellidoPaterno = datos.Element("apPrimer")?.Value,
                ApellidoMaterno = datos.Element("apSegundo")?.Value,
                Direccion = datos.Element("direccion")?.Value,
                EstadoCivil = datos.Element("estadoCivil")?.Value,
                Restriccion = datos.Element("restriccion")?.Value,
                Ubigeo = datos.Element("ubigeo")?.Value,
                Foto = datos.Element("foto")?.Value
            };

            return (codigo, mensaje, persona);
        }
    }
}

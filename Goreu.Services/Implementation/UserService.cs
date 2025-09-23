using Goreu.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Goreu.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<Usuario> userManager;
        private readonly ILogger<UserService> logger;
        private readonly IConfiguration configuration;
        private readonly IOptions<AppSettings> options;
        private readonly IPersonaRepository personaRepository;
        private readonly IEntidadRepository entidadRepository;
        private readonly IUnidadOrganicaRepository unidadOrganicaRepository;
        private readonly SignInManager<Usuario> signInManager;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRolRepository rolRepository;
        private readonly IUserRepository userRepository;
        private readonly IAplicacionRepository aplicacionRepository;
        private readonly IUsuarioUnidadOrganicaRepository usuarioUnidadOrganicaRepository;
        private readonly IEntidadAplicacionRepository entidadAplicacionRepository;


        public UserService(
            UserManager<Usuario> userManager,
            ILogger<UserService> logger,
            IConfiguration configuration,
            IOptions<AppSettings> options,
            SignInManager<Usuario> signInManager,
            IMapper mapper,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            IRolRepository rolRepository,
            IPersonaRepository personaRepository,
            IEntidadRepository entidadRepository,
            IUnidadOrganicaRepository unidadOrganicaRepository,
            IUserRepository userRepository,
            IEmailService emailService,
            IAplicacionRepository aplicacionRepository,
            IUsuarioUnidadOrganicaRepository usuarioUnidadOrganicaRepository,
            IEntidadAplicacionRepository entidadAplicacionRepository
            )
        {
            this.userManager = userManager;
            this.logger = logger;
            this.configuration = configuration;
            this.options = options;
            this.personaRepository = personaRepository;
            this.entidadRepository = entidadRepository;
            this.unidadOrganicaRepository = unidadOrganicaRepository;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.emailService = emailService;
            this.context = context;
            this.roleManager = roleManager;
            this.rolRepository = rolRepository;
            this.userRepository = userRepository;
            this.aplicacionRepository = aplicacionRepository;
            this.usuarioUnidadOrganicaRepository = usuarioUnidadOrganicaRepository;
            this.entidadAplicacionRepository = entidadAplicacionRepository;
        }
        ////---------------------------------------------------------------------------------------------
        ////Registar Usuario

        public async Task<BaseResponseGeneric<string>> RegisterAsync(RegisterRequestDto request)
        {
            var response = new BaseResponseGeneric<string>();

            try
            {
                // 🔹 Validar si ya existe un usuario con esa persona
                var existingUser = await userRepository.GetByPersonaAsync(request.IdPersona);

                // 🔹 Obtener el rol solicitado
                var existingRol = await rolRepository.GetAsync(request.RolId);
                if (existingRol == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "El rol especificado no existe.";
                    return response;
                }

                if (existingUser == null)
                {
                    // 🔹 Crear un nuevo usuario
                    var newUser = new Usuario
                    {
                        UserName = request.UserName,
                        Email = request.Email,
                        IdPersona = request.IdPersona,
                        EmailConfirmed = true
                    };

                    var addUserResult = await userManager.CreateAsync(newUser, request.Password);

                    if (!addUserResult.Succeeded)
                    {
                        response.Success = false;
                        response.ErrorMessage = string.Join("; ", addUserResult.Errors.Select(e => e.Description));
                        return response;
                    }

                    var addUserRoleResult = await userManager.AddToRoleAsync(newUser, existingRol.Name);
                    if (!addUserRoleResult.Succeeded)
                    {
                        response.Success = false;
                        response.ErrorMessage = string.Join("; ", addUserRoleResult.Errors.Select(e => e.Description));
                        return response;
                    }

                    response.Success = true;
                    response.ErrorMessage = "Usuario registrado correctamente con el rol asignado.";
                }
                else
                {
                    // 🔹 Validar si ya tiene ese rol
                    bool alreadyInRole = await userManager.IsInRoleAsync(existingUser, existingRol.Name);

                    if (alreadyInRole)
                    {
                        response.Success = false;
                        response.ErrorMessage = "El usuario ya existe y tiene asignado este rol.";
                    }
                    else
                    {
                        var addUserRoleResult = await userManager.AddToRoleAsync(existingUser, existingRol.Name);

                        if (!addUserRoleResult.Succeeded)
                        {
                            response.Success = false;
                            response.ErrorMessage = string.Join("; ", addUserRoleResult.Errors.Select(e => e.Description));
                            return response;
                        }

                        response.Success = true;
                        response.ErrorMessage = "Rol adicional asignado al usuario existente.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = "Ocurrió un error inesperado al registrar el usuario.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }


        //public async Task<BaseResponseGeneric<string>> RegisterAsync(RegisterRequestDto request)
        //{
        //    var response = new BaseResponseGeneric<string>();
        //    try
        //    {
        //        var existingUsuario = await userRepository.GetByPersonaAsync(request.IdPersona);

        //        if (existingUsuario == null)
        //        {
        //            var user = new Usuario
        //            {
        //                UserName = request.UserName,
        //                Email = request.Email,
        //                IdPersona = request.IdPersona,
        //                EmailConfirmed = true
        //            };

        //            var addUser = await userManager.CreateAsync(user, request.ConfirmPassword);

        //            if (addUser.Succeeded)
        //            {
        //                var existingRol = await rolRepository.GetAsync(request.RolId);

        //                var addUserRol = await userManager.AddToRoleAsync(user, existingRol.Name);

        //                response.Success = true;
        //                response.ErrorMessage = "Se registró rol al usuario.";
        //            }
        //            else
        //            {
        //                response.Success = false;
        //                response.ErrorMessage = "No se registró el usuario.";
        //            }
        //        }
        //        else
        //        {
        //            var existingRol = await rolRepository.GetAsync(request.RolId);

        //            bool tieneRol = await userManager.IsInRoleAsync(existingUsuario, existingRol.Name);

        //            if (tieneRol)
        //            {
        //                response.Success = false;
        //                response.ErrorMessage = "El usuario ya existe con ese rol.";
        //            }
        //            else
        //            { 
        //                var addUserRol = await userManager.AddToRoleAsync(existingUsuario, existingRol.Name);

        //                response.Success = true;
        //                response.ErrorMessage = "Se adicionó un rol al usuario.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ErrorMessage = "Ocurrió un error al registrar el usuario.";
        //        logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        //    }
        //    return response;
        //}

        ////---------------------------------------------------------------------------------------------
        ////Login
        public async Task<BaseResponseGeneric<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var response = new BaseResponseGeneric<LoginResponseDto>();
            try
            {
                var resultado = await signInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);

                if (resultado.Succeeded)
                {
                    //var user = await userManager.FindByEmailAsync(request.UserName);
                    var user = await userManager.FindByNameAsync(request.UserName);
                    response.Success = true;
                    response.Data = await ConstruirToken(user);
                }
                else
                {
                    response.Success = false;
                    response.ErrorMessage = "Credenciales incorrectas.";
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
        ////---------------------------------------------------------------------------------------------
        ////Crear Token
        private async Task<LoginResponseDto> ConstruirToken(Usuario user)
        {

            var claims = new List<Claim>()
               {

                   ////new Claim(ClaimTypes.Email,user.Email ?? string.Empty), //Nunca enviar data sensible en un claim
                   new Claim(ClaimTypes.Name,user.UserName ?? string.Empty), //Nunca enviar data sensible en un claim
                   ////new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}")

               };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //Persona
            var persona = await personaRepository.GetAsync(user.IdPersona);
            Console.WriteLine("user =" + user.IdPersona);
            Console.WriteLine("Persona =" + persona.Id);
            var personaDto = new PersonaResponseDto
            {
                Id = persona.Id,
                Nombres = persona.Nombres,
                ApellidoPat = persona.ApellidoPat,
                ApellidoMat = persona.ApellidoMat,
                FechaNac = persona.FechaNac,
                Email = persona.Email,
                NroDoc = persona.NroDoc
            };

            ////UnidadOrganica
            var dataUnidad = await unidadOrganicaRepository.GetAsyncPerUser(user.Id);
            var unidadOrganicaDto = new List<UnidadOrganicaResponseSingleDto>();

            unidadOrganicaDto = mapper.Map<List<UnidadOrganicaResponseSingleDto>>(dataUnidad);

            ////Aplicaciones
            List<int> idAplicaciones = new List<int>();
            var aplicacionesDto = new List<AplicacionResponseDto>();

            var dataApp = await aplicacionRepository.GetAsyncPerUser(user.Id);
            aplicacionesDto = mapper.Map<List<AplicacionResponseDto>>(dataApp);


            ////Roles
            var dataroles=await rolRepository.GetAsyncPerUser(user.Id);
            var rolesResponseDto= new List<RolResponseSingleDto>();

            rolesResponseDto = mapper.Map<List<RolResponseSingleDto>>(dataroles);

            ////Entidad
            var dataEntidad = await entidadRepository.GetAsyncPerRol(dataroles.First().Id);
            var entidadDto = new EntidadResponseDto();

            entidadDto = mapper.Map<EntidadResponseDto>(dataEntidad);


            ////Agregar múltiples audiencias como claims dinámicamente
            var audiences = options.Value.Jwt.Audiences;
            foreach (var audience in audiences)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience)); //Necesario cuando la API será usada por otras APIs
            }

            ////JWT Signing
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:JWTKey"].ToString()));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddSeconds(Convert.ToDouble(configuration["JWT:LifetimeInSeconds"].ToString()));

            var securityToken = new JwtSecurityToken(
                issuer: options.Value.Jwt.Issuer,
                claims: claims,
                signingCredentials: credenciales,
                expires: expiracion
                );
            return new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                ExpirationDate = expiracion,
                IdUsuario = user.Id,
                Roles = rolesResponseDto,
                Entidad=entidadDto,
                Persona = personaDto,
                UnidadOrganicas= unidadOrganicaDto,
                Aplicaciones = aplicacionesDto
            };
        }

        ////---------------------------------------------------------------------------------------------
        ////Enviar Token a Correo
        public async Task<BaseResponse> RequestTokenToResetPasswordAsync(ResetPasswordRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var userIdentity = await userManager.FindByEmailAsync(request.Email);
                if (userIdentity is null)
                {
                    throw new SecurityException("Usuario no existe");
                }

                var token = await userManager.GeneratePasswordResetTokenAsync(userIdentity);

                var persona = new PersonaInfo();
                persona = mapper.Map<PersonaInfo>((await personaRepository.GetAsync(predicate: s => s.Email == request.Email)).FirstOrDefault());
              


                Console.WriteLine("TOKEN CREADO"+token);


                Console.WriteLine("ENVIANDO CORREO .........AL CORREO: "+request.Email+ " al señor: "+persona.nombres  );
                ////Enviar un email con el token para reestablecer la contraseña
                await emailService.SendEmailAsync(request.Email, "Reestablecer clave", "" +
                    @$"
                    <p> Estimado {persona.nombres} {persona.apellidoPat} {persona.apellidoMat} </p>
                    <p> Para reestablecer su clave, por favor copie el siguiente codigo</p>
                    <p> <strong> {token} </strong> </p>
                    <hr />
                    Atte. <br />
                    Goreu © 2025
                    ");

                Console.WriteLine("PASO EL METODO <SendEmailAsync> Y ENVIO EL CORREO");

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al solicitar el token para resetear la clave";
                logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
        ////--------------------------------------------------------------------------------------------
        ////Resetet password
        public async Task<BaseResponse> ResetPasswordAsync(NewPasswordRequestDto request)
        {
            var response = new BaseResponse();

            try
            {
                var userIdentity = await userManager.FindByEmailAsync(request.Email);

                if (userIdentity is null)
                {
                    throw new ApplicationException("Usuario no existe");
                }

                var result = await userManager.ResetPasswordAsync(userIdentity, request.Token, request.ConfirmNewPassword);
                response.Success = result.Succeeded;

                if (!result.Succeeded)
                {

                    response.ErrorMessage = string.Join(" ", result.Errors.Select(x => x.Description).ToArray());
                }
                else
                {
                    var persona = new PersonaInfo();
                    persona = mapper.Map<PersonaInfo>((await personaRepository.GetAsync(predicate: s => s.Email == request.Email)).FirstOrDefault());
                    //Enviar un email de confirmacion de clave cambiada
                    await emailService.SendEmailAsync(request.Email, "Confiracion de cambio de clave",
                    @$"
                    <P> Estimado {persona.nombres} {persona.apellidoPat} {persona.apellidoMat} </p>
                    <p> Se ha cambiado su clave correctamente</p>
                    <hr />
                    Atte. <br />
                    Tramite Goreu @ 2024");
                }
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al resetear el Password";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
        ////---------------------------------------------------------------------------------------------
        /////Cambiar password con email
        public async Task<BaseResponse> ChangePasswordAsyncEmail(string email, ChangePasswordRequestDto request)
        {
            var response = new BaseResponse();

            try
            {
                var userIdentity = await userManager.FindByEmailAsync(email);

                if (userIdentity is null)
                {
                    throw new ApplicationException("Usuario no existe");
                }

                var result = await userManager.ChangePasswordAsync(userIdentity, request.OldPassword, request.NewPassword);
                response.Success = result.Succeeded;
                if (!result.Succeeded)
                {
                    response.ErrorMessage = string.Join(" ", result.Errors.Select(x => x.Description).ToArray());
                }
                else
                {
                    var persona = new PersonaInfo();
                    persona = mapper.Map<PersonaInfo>((await personaRepository.GetAsync(predicate: s => s.Email == email)).FirstOrDefault());
                    logger.LogInformation("Se cambio la clave para {email}", userIdentity.Email);
                    //Enviar un email de confirmacion de clave cambiada
                    await emailService.SendEmailAsync(email, "Confiracion de cambio de clave",
                    @$"
                    <P> Estimado {persona.nombres} {persona.apellidoPat} {persona.apellidoMat}</p>
                    <p> Se ha cambiado su clave corecctaente</p>
                    <hr />
                    Atte. <br />
                    Tramite Goreu @ 2024");
                }
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al cambiar la clave";
                logger.LogError(ex, "Error al cambiar password {Message}", ex.Message);
            }

            return response;
        }
        ////---------------------------------------------------------------------------------------------
        ////Cambiar password con userName
        public async Task<BaseResponse> ChangePasswordAsyncUserName(string userName, ChangePasswordRequestDto request)
        {
            var response = new BaseResponse();
            Console.WriteLine("usuario 0 :" + userName);
            try
            {
                var userIdentity = await userManager.FindByNameAsync(userName);


                if (userIdentity is null)
                {
                    Console.WriteLine("usuario 2 :" + userIdentity.UserName);


                }

                var result = await userManager.ChangePasswordAsync(userIdentity, request.OldPassword, request.NewPassword);
                response.Success = result.Succeeded;
                if (!result.Succeeded)
                {

                    response.ErrorMessage = string.Join(" ", result.Errors.Select(x => x.Description).ToArray());
                }
                else
                {

                    var persona = new PersonaInfo();
                    persona = mapper.Map<PersonaInfo>((await personaRepository.GetAsync(predicate: s => s.Email == userIdentity.Email)).FirstOrDefault());

                    logger.LogInformation("Se cambio la clave para {email}", userIdentity.Email);
                    ////Enviar un email de confirmacion de clave cambiada
                    await emailService.SendEmailAsync(userIdentity.Email, "Confiracion de cambio de clave",
                    @$"
                    <P> Estimado {persona.nombres} {persona.apellidoPat} {persona.apellidoMat}</p>
                    <p> Se ha cambiado su clave corecctaente</p>
                    <hr />
                    Atte. <br />
                    Tramite Goreu @ 2024");
                }
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al cambiar la clave";
                logger.LogError(ex, "Error al cambiar password {Message}", ex.Message);
            }

            return response;
        }
        ////---------------------------------------------------------------------------------------------
        ////Traer todos los usuariocon ese rol
        public async Task<BaseResponseGeneric<List<UsuarioResponseDto>>> GetUsersByRole(string? role)
        {
            var response = new BaseResponseGeneric<List<UsuarioResponseDto>>();
            try
            {
                List<Usuario> resultado = new();
                if (role.Length > 0)
                {

                    resultado = (await userManager.GetUsersInRoleAsync(role)).ToList();//trae los user por role
                }
                else
                {
                    resultado = await context.Users.ToListAsync(); //trae a todos los user

                }

                var listResponse = new List<UsuarioResponseDto>();
                foreach (var user in resultado)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    listResponse.Add(new UsuarioResponseDto
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email ?? string.Empty,
                        Estado=user.Estado
                    });
                }
                if (resultado.Count > 0)
                {
                    response.Success = true;
                    response.Data = listResponse;
                }
                else
                {
                    response.ErrorMessage = "Ningun usuario encontrado.";
                    logger.LogWarning(response.ErrorMessage);
                }

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
        ////---------------------------------------------------------------------------------------------
        ////Buscar Usuario por email
        public async Task<BaseResponseGeneric<UsuarioResponseDto>> GetUserByEmail(string email)
        {
            var response = new BaseResponseGeneric<UsuarioResponseDto>();
            try
            {
                var user = await userManager.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
                if (user is not null)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    var userDto = new UsuarioResponseDto
                    {
                        Id = user.Id,
                        Email = user.Email ?? string.Empty,
                        UserName = user.UserName,
                        Estado = user.Estado
                    };
                    response.Success = true;
                    response.Data = userDto;

                }
                else
                {
                    response.ErrorMessage = "Ningun usuario encontrado.";
                    logger.LogWarning(response.ErrorMessage);

                }

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<UsuarioResponseDto>> GetUserByIdAsync(string userId)
        {
            var response = new BaseResponseGeneric<UsuarioResponseDto>();

            try
            {
                var user = await userManager.Users
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user is not null)
                {
                    response.Success = true;
                    response.Data = mapper.Map<UsuarioResponseDto>(user);
                }
                else
                {
                    response.ErrorMessage = "Ningún usuario encontrado.";
                    logger.LogWarning(response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        ////---------------------------------------------------------------------------------------------
        ////---------------------------------------------------------------------------------------------
        ////Asignar Role por IdUsuario
        public async Task<BaseResponse> GrantUserRole(string userId, string roleName)
        {
            var response = new BaseResponse();

            try
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    response.ErrorMessage = "usuario no enocntrado";
                    return response;
                }

                ////verifica si existe el rol
                var roleExistis = await roleManager.RoleExistsAsync(roleName);
                if (!roleExistis)
                {
                    response.ErrorMessage = "Rol no Existe";
                    return response;
                }
                ////verificar si el user tiene el rol
                var userRoleExists = await userManager.IsInRoleAsync(user, roleName);
                if (userRoleExists)
                {
                    response.ErrorMessage = "ya cuenta con este rol";
                    return response;
                }

                ////agregar el rol
                var resultado = await userManager.AddToRoleAsync(user, roleName);
                if (resultado.Succeeded)
                {
                    response.Success = true;

                }
                else
                {
                    response.ErrorMessage = string.Join(" ", resultado.Errors.Select(x => x.Description).ToArray());
                    logger.LogWarning(response.ErrorMessage);

                }


            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al agregar Rol al Usuario";
                logger.LogError(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
        ////---------------------------------------------------------------------------------------------
        ////Traer Asignar Role por email
        public async Task<BaseResponse> GrantUserRoleByEmail(string email, string roleName)
        {
            var response = new BaseResponse();

            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user is null)
                {
                    response.ErrorMessage = "usuario no enocntrado";
                    return response;
                }
                var roleExistis = await roleManager.RoleExistsAsync(roleName);
                if (!roleExistis)
                {
                    response.ErrorMessage = "Rol no Existe";
                    return response;
                }
                ////verificar si el user tiene el rol
                var userRoleExists = await userManager.IsInRoleAsync(user, roleName);
                if (userRoleExists)
                {
                    response.ErrorMessage = "ya cuenta con este rol";
                    return response;
                }

                ////agregar el rol
                var resultado = await userManager.AddToRoleAsync(user, roleName);
                if (resultado.Succeeded)
                {
                    response.Success = true;

                }
                else
                {
                    response.ErrorMessage = string.Join(" ", resultado.Errors.Select(x => x.Description).ToArray());
                    logger.LogWarning(response.ErrorMessage);

                }


            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al agregar Rol al Usuario";
                logger.LogError(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
        ////---------------------------------------------------------------------------------------------
        ////Quitar todos los roles por idUsuario
        public async Task<BaseResponse> RevokeUserRoles(string userId)
        {
            var response = new BaseResponse();

            try
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    response.ErrorMessage = "usuario no encontrado";
                    return response;
                }
                var roles = await userManager.GetRolesAsync(user);
                var resultado = await userManager.RemoveFromRolesAsync(user, roles);
                if (resultado.Succeeded)
                {
                    response.Success = true;

                }
                else
                {
                    response.ErrorMessage = string.Join(" ", resultado.Errors.Select(x => x.Description).ToArray());
                    logger.LogWarning(response.ErrorMessage);
                }


            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocuarrio un error al quitar roles";
                logger.LogError(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
        ////---------------------------------------------------------------------------------------------
        ////Quitar un Role especifico po IdUsruaio
        public async Task<BaseResponse> RevokeUserRole(string userId, string roleName)
        {
            var response = new BaseResponse();

            try
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    response.ErrorMessage = "usuario no encontrado";
                    return response;
                }

                var resultado = await userManager.RemoveFromRoleAsync(user, roleName);
                if (resultado.Succeeded)
                {
                    response.Success = true;

                }
                else
                {
                    response.ErrorMessage = string.Join(" ", resultado.Errors.Select(x => x.Description).ToArray());
                    logger.LogWarning(response.ErrorMessage);
                }


            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocuarrio un error al quitar rol";
                logger.LogError(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<UsuarioInfo>>> GetAsyncAll(string? userName, PaginationDto pagination)
        {
            var response=new BaseResponseGeneric<ICollection<UsuarioInfo>>();

            try
            {
                response.Data=await userRepository.GetAsyncAll(userName, pagination);
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obetner los datos";
                logger.LogError(ex,"{ErrorMessage} {Message}",response.ErrorMessage,ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<UsuarioResponseDto>>> GetAsync(int idEntidad, int idAplicacion, string? rolId, string? search, PaginationDto? pagination)
        {
            var response = new BaseResponseGeneric<ICollection<UsuarioResponseDto>>();

            try
            {
                search = string.IsNullOrWhiteSpace(search) ? "" : search;

                //var rol = await rolRepository.GetAsync(rolId);
                var entidadAplicacion = await entidadAplicacionRepository.GetAsync(idEntidad, idAplicacion);

                ICollection<UsuarioInfo> data = await userRepository.GetByRolAsync(entidadAplicacion!.IdAplicacion, rolId, search, pagination);

                response.Data = mapper.Map<ICollection<UsuarioResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener usuarios.";
                logger.LogError(ex,
                    "{ErrorMessage}. Parámetros -> rolId: {RolId}, search: {Search}",
                    response.ErrorMessage, rolId, search);
            }

            return response;
        }

        public async Task<BaseResponse> FinalizeAsync(string id)
        {
            var response = new BaseResponse();
            try
            {
                await userRepository.FinalizeAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al finalizar el usuario.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> InitializeAsync(string id)
        {
            var response = new BaseResponse();
            try
            {
                await userRepository.InitializeAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al inicializar el usuario.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

       
    }
}


using EShop.Application.Common;
using EShop.Application.Dtos;
using EShop.Application.Helpers;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Security;
using EShop.Application.Settings;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;

namespace EShop.Application.Features.Login.Commands
{
    public class LoginCredencialesHandler : IRequestHandler<LoginCredencialesCommand, Result<ResponseModelDto>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISesionRepository _sesionRepository;
        private readonly IPasswordHasherSecurity _passwordHasherSecurity;
        private readonly IJWTSecurity _jwtSecurity;

        private readonly JWTSettings _jwtSettings;
        public LoginCredencialesHandler(IUsuarioRepository usuarioRepository, ISesionRepository sesionRepository, IPasswordHasherSecurity passwordHasherSecurity, IJWTSecurity jwtSecurity,
            IOptions<JWTSettings> config)
        {
            _usuarioRepository = usuarioRepository;
            _sesionRepository = sesionRepository;
            _passwordHasherSecurity = passwordHasherSecurity;
            _jwtSecurity = jwtSecurity;
            _jwtSettings = config.Value;
        }
        public async Task<Result<ResponseModelDto>> Handle(LoginCredencialesCommand request, CancellationToken cancellationToken)
        {
            var usuarioEntity = await _usuarioRepository.BuscarPorNombreAsync(request.LoginCredencialesDto.Username);
            if (usuarioEntity is not null) {
                var credencialesValidas = _passwordHasherSecurity.Validar(usuarioEntity.Contrasenia, request.LoginCredencialesDto.Password);
                
                if (credencialesValidas)
                {
                    var sesionActual = await _sesionRepository.BuscarPorIdUsuarioAsync(usuarioEntity.IdUsuario);
                    if(sesionActual is null || sesionActual?.Activo == 0)
                    {
                        var expiration = FechaHelper.ActualUTC(_jwtSettings.VigenciaEnMinutos);
                        var jti = Guid.NewGuid().ToString();
                        var jwt = _jwtSecurity.Generar(usuarioEntity, jti, expiration);
                        
                        var sesionEntity = new SesionEntity()
                        {
                            Activo = 1,
                            IdUsuario = usuarioEntity.IdUsuario,
                            Jti = jti,
                            FechaExpiracion = expiration
                        };
                        await _sesionRepository.RegistrarAsync(sesionEntity);
                        return Result<ResponseModelDto>.Success(new ResponseModelDto(datos: jwt));
                    }
                    return Result<ResponseModelDto>.Failure(new ResponseModelDto("Se encuentra una sesion activa."), System.Net.HttpStatusCode.BadRequest);
                }
                return Result<ResponseModelDto>.Failure(null!, System.Net.HttpStatusCode.Forbidden);
            }
            return Result<ResponseModelDto>.Failure(null!, System.Net.HttpStatusCode.Forbidden);
        }
    }
}

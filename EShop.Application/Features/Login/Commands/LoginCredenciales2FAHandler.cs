using EShop.Application.Common;
using EShop.Application.Dtos;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Login.Commands
{
    public class LoginCredenciales2FAHandler : IRequestHandler<LoginCredenciales2FACommand, Result<ResponseModelDto>>
    {
        private readonly ISesionRepository _sesionRepository;
        private readonly ISegundoFARepository _segundoFARepository;
        public LoginCredenciales2FAHandler(ISesionRepository sesionRepository, ISegundoFARepository segundoFARepository)
        {
            _sesionRepository = sesionRepository;
            _segundoFARepository = segundoFARepository;
        }
        public async Task<Result<ResponseModelDto>> Handle(LoginCredenciales2FACommand request, CancellationToken cancellationToken)
        {
            SesionEntity sesionEntity = await _sesionRepository.BuscarPorJtiAsync(request.LoginCredenciales2FADto.Jti);
            SegundoFAEntity? segundoFAEntity = await _segundoFARepository.BuscarPorIdUsuario(sesionEntity.IdUsuario);

            if (sesionEntity is not null)
            {
                var secretBytes = OtpNet.Base32Encoding.ToBytes(segundoFAEntity!.Contrasenia);

                var totp = new OtpNet.Totp(secretBytes, step: 30, mode: OtpNet.OtpHashMode.Sha512);
                bool valid = totp.VerifyTotp(request.LoginCredenciales2FADto.Otp, out _, new OtpNet.VerificationWindow(0, 0));

                if (valid)
                {
                    sesionEntity.Estado = 1;
                    await _sesionRepository.ActualizarAsync(sesionEntity);
                    return Result<ResponseModelDto>.Success(new ResponseModelDto(datos: null!));
                }

                return Result<ResponseModelDto>.Failure(
                    null!,
                    System.Net.HttpStatusCode.Unauthorized
                );

            }

            return Result<ResponseModelDto>.Failure(
                new ResponseModelDto(mensaje: "Ocurrió un error con los datos proporcionados."),
                System.Net.HttpStatusCode.BadRequest
            );
        }
    } 
}

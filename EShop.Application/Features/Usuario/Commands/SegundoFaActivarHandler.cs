using EShop.Application.Common;
using EShop.Application.Dtos;
using EShop.Application.Interfaces.Repositories;
using MediatR;

namespace EShop.Application.Features.Usuario.Commands
{
    public class SegundoFaActivarHandler : IRequestHandler<SegundoFaActivarCommand, Result<ResponseModelDto>>
    {
        private readonly ISegundoFARepository _segundoFARepository;
        public SegundoFaActivarHandler(ISegundoFARepository segundoFARepository)
        {
            _segundoFARepository = segundoFARepository;
        }
        public async Task<Result<ResponseModelDto>> Handle(SegundoFaActivarCommand request, CancellationToken cancellationToken)
        {
            var segundoFAEntity = await _segundoFARepository.BuscarPorIdUsuario(request.ActivarSegundoFaDto.IdUsuario);

            if (segundoFAEntity is not null)
            {
                var secretBytes = OtpNet.Base32Encoding.ToBytes(segundoFAEntity.Contrasenia);

                var totp = new OtpNet.Totp(secretBytes, step: 30, mode: OtpNet.OtpHashMode.Sha512);
                bool valid = totp.VerifyTotp(request.ActivarSegundoFaDto.Otp, out _, new OtpNet.VerificationWindow(0, 0));

                if (valid)
                {
                    segundoFAEntity.Activo = 1;
                    await _segundoFARepository.ActualizarAsync(segundoFAEntity);
                    return Result<ResponseModelDto>.Success(new ResponseModelDto(datos: null!));
                }

                return Result<ResponseModelDto>.Failure(
                    new ResponseModelDto(mensaje: "Ocurrió un error al validar el OTP."),
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

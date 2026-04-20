using EShop.Application.Common;
using EShop.Application.Dtos;
using EShop.Application.Helpers;
using EShop.Application.Interfaces.Repositories;
using MediatR;

namespace EShop.Application.Features.Usuario.Commands
{
    public class ActivarUsuarioHandler : IRequestHandler<ActivarUsuarioCommand, Result<ResponseModelDto>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public ActivarUsuarioHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

        }
        public async Task<Result<ResponseModelDto>> Handle(ActivarUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioEntity = await _usuarioRepository.BuscarPorIdPersonaAsync(request.ActivarUsuarioDto.IdPersona);
                if (usuarioEntity is not null)
                {
                    usuarioEntity.Activo = 1;
                    await _usuarioRepository.ActualizarAsync(usuarioEntity);
                    return Result<ResponseModelDto>.Success(new ResponseModelDto(MensajesHelper.OK));
                }

                return Result<ResponseModelDto>.Failure(new ResponseModelDto("Usuario no encontrado"), System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return Result<ResponseModelDto>.Failure(new ResponseModelDto(ex.Message), System.Net.HttpStatusCode.InternalServerError);
            }            
        }
    }
}

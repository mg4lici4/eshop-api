using EShop.Application.Common;
using EShop.Application.Dtos;
using EShop.Application.Helpers;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Sesion.Commands
{
    public class CerrarSesionHandler : IRequestHandler<CerrarSesionCommand, Result<ResponseModelDto>>
    {
        private readonly ISesionRepository _sesionRepository;
        public CerrarSesionHandler(ISesionRepository sesionRepository)
        {
            _sesionRepository = sesionRepository;
        }
        public async Task<Result<ResponseModelDto>> Handle(CerrarSesionCommand request, CancellationToken cancellationToken)
        {
            SesionEntity? sesionEntity = await _sesionRepository.BuscarPorJtiAsync(request.CerrarSesionDto.JTI);
            if (sesionEntity is null)
                return Result<ResponseModelDto>.Failure(new ResponseModelDto(mensaje: MensajesHelper.ERROR_PETICION_INCORRECTA));

            if(sesionEntity.Estado == 1)
            {
                sesionEntity.Estado = 3;
                await _sesionRepository.ActualizarAsync(sesionEntity);
                return Result<ResponseModelDto>.Success(new ResponseModelDto(mensaje: MensajesHelper.OPERACION_CORRECTA));
            }

            return Result<ResponseModelDto>.Failure(new ResponseModelDto(mensaje: MensajesHelper.ERROR_SESION_CAMBIAR_ESTADO));
        }
    }
}

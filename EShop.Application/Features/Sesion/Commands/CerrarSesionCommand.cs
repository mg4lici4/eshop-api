using EShop.Application.Common;
using EShop.Application.Dtos;
using EShop.Application.Dtos.Sesion;
using MediatR;

namespace EShop.Application.Features.Sesion.Commands
{
    public class CerrarSesionCommand : IRequest<Result<ResponseModelDto>>
    {
        public required CerrarSesionDto CerrarSesionDto { get; set; }
    }
}

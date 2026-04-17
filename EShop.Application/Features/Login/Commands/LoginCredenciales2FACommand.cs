using EShop.Application.Common;
using EShop.Application.Dtos;
using EShop.Application.Dtos.Login;
using MediatR;

namespace EShop.Application.Features.Login.Commands
{
    public class LoginCredenciales2FACommand : IRequest<Result<ResponseModelDto>>
    {
        public required ValidarCredenciales2FADto LoginCredenciales2FADto { get; set; } = default!;
    }
}

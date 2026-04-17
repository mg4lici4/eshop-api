using EShop.Application.Dtos.Login;
using EShop.Application.Features.Login.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Api.Controllers
{
    [ApiController]
    [Route("api/v1/eshop/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("username", Name = "Validar credenciales")]
        public async Task<IActionResult> ValidarCredenciales(ValidarCredencialesDto validarCredencialesDto)
        {
            var command = new LoginCredencialesCommand() { LoginCredencialesDto = validarCredencialesDto };
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);

            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpPost("2fa", Name = "Validar credenciales 2FA")]
        public async Task<IActionResult> ValidarCredenciales2FA(ValidarCredenciales2FADto validarCredenciales2FADto)
        {
            var command = new LoginCredenciales2FACommand() { LoginCredenciales2FADto = validarCredenciales2FADto };
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);

            return StatusCode(result.StatusCode, result.Value);
        }
    }
}

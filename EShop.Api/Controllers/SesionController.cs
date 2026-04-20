using EShop.Application.Dtos.Login;
using EShop.Application.Dtos.Sesion;
using EShop.Application.Features.Login.Commands;
using EShop.Application.Features.Sesion.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Api.Controllers
{
    [ApiController]
    [Route("api/v1/eshop/[controller]")]
    public class SesionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SesionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("cerrar", Name = "Cerrar sesion vigente")]
        public async Task<IActionResult> CerrarSesionVigente(CerrarSesionDto cerrarSesionDto)
        {
            var command = new CerrarSesionCommand() { CerrarSesionDto = cerrarSesionDto };
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);

            return StatusCode(result.StatusCode, result.Value);
        }
    }
}

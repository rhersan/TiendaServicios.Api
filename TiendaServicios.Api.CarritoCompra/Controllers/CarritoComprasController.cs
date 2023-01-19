using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.CarritoCompra.Aplicacion;
using TiendaServicios.Api.CarritoCompra.Aplicacion.DTO;

namespace TiendaServicios.Api.CarritoCompra.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CarritoComprasController : Controller
    {
        private readonly IMediator _mediator;

        public CarritoComprasController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> CrearCarrito(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarritoDTO>> GetCarrito(int id)
        {
            return await _mediator.Send(new Consulta.Ejecuta { CarritoSesionId = id});
        }
    }
}

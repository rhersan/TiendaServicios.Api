using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Aplicacion.DTO;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDTO> { 
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
        {
            private readonly CarritoContexto _contexto;
            private readonly ILibrosService _librosService;
            public Manejador(CarritoContexto contexto, ILibrosService librosService)
            {
                _contexto= contexto;
                _librosService= librosService;
            }
            public async Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await _contexto.CarritoSesion.Where(x => x.CarritoSesionId == request.CarritoSesionId).FirstOrDefaultAsync();
                if(carritoSesion == null)
                {
                    throw new Exception("No se encontró el carrito");
                }
                var carritoSesionDetalle = await _contexto.CarritoSesionDetalle.Where(x => x.CarritoSesionId == carritoSesion.CarritoSesionId).ToListAsync();
                List<CarritoDetalleDTO> listaCarritoDto = new List<CarritoDetalleDTO>();
                foreach (var libro in carritoSesionDetalle)
                {
                    var resp = await _librosService.GetLibro(new Guid(libro.ProductoSeleccionado));
                
                    if(resp.resultado)
                    {
                        var objetoLibro = resp.Libro;
                        var carritoDetalle = new CarritoDetalleDTO
                        {
                          TituloLibro = objetoLibro.Titulo,
                          FechaPublicacion = objetoLibro.FechaPublicacion,
                          LibroId =  objetoLibro.AutorLibro
                        };
                        listaCarritoDto.Add(carritoDetalle);
                    }
                  
                     
                }
                var carritoSesionDto = new CarritoDTO
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos= listaCarritoDto
                };

                return carritoSesionDto;
            }
        }
    }
}

using MediatR;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest {
            //public DateTime? FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto _contexto;

            public Manejador(CarritoContexto contexto)
            {
                _contexto = contexto;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // Obtener IdSesión
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = DateTime.Now,
                };
                _contexto.CarritoSesion.Add(carritoSesion);
                var respo =await _contexto.SaveChangesAsync();
                if(respo ==0)
                {
                    throw new Exception("No se pudo obtener el id de la sesión");
                }
                int idSesion = carritoSesion.CarritoSesionId;
                // Agregar CarritoDetalle
                foreach (var idProducto in request.ProductoLista)
                {
                    var carritoDetalle = new CarritoSesionDetalle
                    {
                        CarritoSesionId = idSesion,
                        FechaCreacion = DateTime.Now,
                        ProductoSeleccionado = idProducto,
                    };
                    _contexto.CarritoSesionDetalle.Add(carritoDetalle);
                    var resp = await _contexto.SaveChangesAsync();
                    if(resp == 0)
                    {
                        throw new Exception("Error al guardar el carrito");
                    }

                }


                return Unit.Value;
            }
        }

    }
}

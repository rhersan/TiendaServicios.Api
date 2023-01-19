using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Aplicacion.DTO;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta: IRequest<List<LibroMaterialDTO>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDTO>>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoLibreria contextoLibreria, IMapper maper)
            {
                _contexto = contextoLibreria;
                _mapper = maper;
            }
            public async Task<List<LibroMaterialDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
               var libro = await _contexto.LibreriaMaterial.ToListAsync();
               var libroDto = _mapper.Map<List<LibreriaMaterial>,List<LibroMaterialDTO>>(libro);
                return libroDto;

            }
        }
    }
}

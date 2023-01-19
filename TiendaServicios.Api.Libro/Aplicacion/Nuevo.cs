﻿using FluentValidation;
using MediatR;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }

            public DateTime? FechaPublicacion { get; set; } 

            public Guid? AutorLibro { get; set; }

        }

        public class EjecutaValidacion: AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibreria _contexto;
            public Manejador(ContextoLibreria contexto)
            {
                _contexto = contexto;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var LibreriaMateria = new LibreriaMaterial
                {
                    Titulo = request.Titulo,
                    FechaPublicacion= request.FechaPublicacion,
                    AutorLibro = request.AutorLibro,
                };
                _contexto.LibreriaMaterial.Add(LibreriaMateria);
                var response = await _contexto.SaveChangesAsync();          
                if(response > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo guardar el Libro");
            }
        }
    }
}

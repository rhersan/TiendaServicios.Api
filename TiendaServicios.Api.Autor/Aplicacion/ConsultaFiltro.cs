﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Aplicacion.DTO;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDTO>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador: IRequestHandler<AutorUnico, AutorDTO>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                _contexto= contexto;
                _mapper= mapper;
            }

            public async Task<AutorDTO> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _contexto.AutorLibro.Where(x => x.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();
                if(autor == null)
                {
                    throw new Exception("No ase encontró el autor.");
                }
                var autorDto =  _mapper.Map<AutorLibro, AutorDTO>(autor);
                return autorDto;
            }
        }
    }
}

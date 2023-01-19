using AutoMapper;
using TiendaServicios.Api.Autor.Aplicacion.DTO;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<AutorLibro, AutorDTO>();
        }
    }
}

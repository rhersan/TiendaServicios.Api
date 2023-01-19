using AutoMapper;
using TiendaServicios.Api.Libro.Aplicacion.DTO;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDTO>();
        }
    }
}

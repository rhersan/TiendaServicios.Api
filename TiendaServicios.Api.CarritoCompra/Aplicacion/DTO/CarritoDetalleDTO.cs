﻿namespace TiendaServicios.Api.CarritoCompra.Aplicacion.DTO
{
    public class CarritoDetalleDTO
    {
        public Guid? LibroId { get; set; }
        public string TituloLibro { get; set; }
        public string AutorLibro { get; set; }
        public DateTime? FechaPublicacion { get; set; }
    }
}

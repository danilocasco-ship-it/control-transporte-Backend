using System;
using System.Collections.Generic;

namespace Application.PrestamosEquipos
{
    public interface IPrestamoEquipoService
    {
        List<PrestamoEquipoDto> ObtenerTodos(int codempresa);
        PrestamoEquipoDto ObtenerPorId(int codempresa, int codsucursal, int codcontratista, int codequipo);
        void Guardar(PrestamoEquipoDto prestamo);
        void Actualizar(int codempresa, int codsucursal, int codcontratista, int codequipo, PrestamoEquipoDto prestamo);
        void Eliminar(int codempresa, int codsucursal, int codcontratista, int codequipo);
    }

    public class PrestamoEquipoDto
    {
        public int Codempresa { get; set; }
        public int Codsucursal { get; set; }
        public int Codcontratista { get; set; }
        public int Codequipo { get; set; }
        public DateTime? Fecharegistro { get; set; }
        public DateTime? Fechaprestamo { get; set; }
        public string Estadoprestamo { get; set; }
        public DateTime? Fechadevolucion { get; set; }
        public string Estadodevolucion { get; set; }
        public string Observaciones { get; set; }
        public string Codusuario { get; set; }
    }
}

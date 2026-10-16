using System;
using System.Collections.Generic;

namespace Application.Equipos
{
    public interface IEquipoService

    {
        void GuardarEquipo(EquipoDto equipo);
        List<EquipoDto> ObtenerTodos();
        EquipoDto ObtenerPorId(int codempresa, int codequipo);
        void ActualizarEquipo(int codempresa, int codequipo, EquipoDto equipo);
        void EliminarEquipo(int codempresa, int codequipo);
    }

    public class EquipoDto
    {
        public int Codempresa { get; set; }
        public int Codequipo { get; set; }
        public string Nomequipo { get; set; }
        public int? Codtipomaquinaria { get; set; }
        public int? Codmarca { get; set; }
        public int? Codproveedor { get; set; }
        public string Nomproveedor { get; set; }
        public string Modelo { get; set; }
        public int? Anio { get; set; }
        public string Placa { get; set; }
        public string Capacidad { get; set; }
        public int Estado { get; set; }
        public DateTime? Fecharegistro { get; set; }
        public string Codusuario { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Application.SQL.Auth.DTO
{
    public class SesionUsuarioDTO
    {
        public int tipousuario { get; set; }
        public string codusuario { get; set; }
        public string nomusuario { get; set; }
        public DateTime expiracion { get; set; }
        public List<PermisoDTO> permisos { get; set; } = new List<PermisoDTO>();
    }

    public class PermisoDTO
    {
        public string nombre_permiso { get; set; }
    }
}

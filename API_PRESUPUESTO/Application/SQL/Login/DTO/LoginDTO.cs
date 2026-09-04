using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.Login.DTO
{
    public class LoginDTO
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string codigo_vendedor { get; set; }        
        public string mac_maquina { get; set; }
        public int cambio_contrasena { get; set; }
        public string nom_colaborador { get; set; }
        public string cargo { get; set; }
        public bool activo { get; set; }
        public int contador_fallidos { get; set; }
        public List<PermisosDTO> permisos { get; set; }
        public DateTime expiracion { get; set; }
    }
}

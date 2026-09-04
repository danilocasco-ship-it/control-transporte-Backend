using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// JULIO MOLINA 30/05/2025

namespace Application.SQL.Login_pre.DTO
{
    public class Login_DTO
    {
        public int tipousuario { get; set; }
        public string codusuario { get; set; }
        public string clave { get; set; }

        public string nomusuario { get; set; }
        public bool estado { get; set; }
        //public int contador_fallidos { get; set; }
        //public List<Permisos_DTO> permisos { get; set; }
        public DateTime expiracion { get; set; }
    }
}

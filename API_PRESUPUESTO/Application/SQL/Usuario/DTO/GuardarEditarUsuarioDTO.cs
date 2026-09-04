using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.Usuario.DTO
{
    public class GuardarEditarUsuarioDTO
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string nom_colaborador { get; set; }
        public int id_cargo { get; set; }
        public string codigo_vendedor { get; set; }
        public string mac_adrees { get; set; }
        public int id_perfil { get; set; }
    }
}

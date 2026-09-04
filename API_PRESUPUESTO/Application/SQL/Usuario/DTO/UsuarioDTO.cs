using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.Usuario.DTO
{
    public class UsuarioDTO
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string nom_colaborador { get; set; }
        public int id_cargo { get; set; }
        public string cargo { get; set; }
        public string codigo_vendedor { get; set; }
        public string mac_maquina { get; set; }
        public string fec_ultimo_inicio { get; set; }
        public string fecha { get; set; } //fecha de creacion o fecha de anulacion
        public string perfil { get; set; }
        public int id_perfil { get; set; }
    }
}

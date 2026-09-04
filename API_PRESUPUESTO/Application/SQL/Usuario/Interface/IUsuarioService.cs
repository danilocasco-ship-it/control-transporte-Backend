using Application.SQL.Usuario.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.Usuario.Interface
{
    public interface IUsuarioService
    {
        List<UsuarioDTO> ObtenerUsuariosActivos(string db);

        List<UsuarioDTO> ObtenerUsuariosInactivos(string db);

        UsuarioDTO ObtenerUsuarioId(string db, int? id);

        ResultUsuarioDTO GuardarUsuario(string db, GuardarEditarUsuarioDTO usuario);

        ResultUsuarioDTO ActualizarUsuario(string db, GuardarEditarUsuarioDTO usuario);

        ResultUsuarioDTO DesactivarUsuario(string db, int? id);

        ResultUsuarioDTO ActivarUsuario(string db, int? id);

        ResultUsuarioDTO ResetearClave(string db, int? id);
    }
}

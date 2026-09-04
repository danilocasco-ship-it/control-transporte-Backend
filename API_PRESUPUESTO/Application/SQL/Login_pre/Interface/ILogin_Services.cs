using Application.SQL.Login_pre.Parameter;
using Application.SQL.Login_pre.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.Login_pre.Interface
{
   public interface ILogin_Services
    {
        Login_DTO Login(Parameter_Login parameter, string db);

        Login_DTO ObtenerUsuarioHandoff(int codempresa, string codusuario, string db);

        List<Permisos_DTO> permisos(int Id, string db);

        Response_PasswordDTO PasswordChanger(Parameter_PasswordDTO parameter, string db);
    }
}

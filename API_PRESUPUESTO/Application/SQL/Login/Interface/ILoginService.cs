using Application.SQL.Login.DTO;
using Application.SQL.Login.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.Login.Interface
{
    public interface ILoginService
    {
        LoginDTO Login(ParameterLogin parameter, string db);

        List<PermisosDTO> Permisos(int Id, string db);

        ResponsePasswordChangeDTO PasswordChange(ParameterPasswordChangeDTO parameter, string db);
    }
}

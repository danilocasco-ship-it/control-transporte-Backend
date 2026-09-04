using Application.Utilidades.EnvioCorreo.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilidades.EnvioCorreo.Interface
{
    public interface ISendEmailsService
    {
        string SendEmails(ParameterSendEmail parameter, ConfigCredentialsEmail config);
    }
}

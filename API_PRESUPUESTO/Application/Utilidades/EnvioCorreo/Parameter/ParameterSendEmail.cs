using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilidades.EnvioCorreo.Parameter
{
    public class ParameterSendEmail
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Recipients { get; set; }

        public List<Adjuntos> ListadoAdjuntos { get; set; }
    }
}

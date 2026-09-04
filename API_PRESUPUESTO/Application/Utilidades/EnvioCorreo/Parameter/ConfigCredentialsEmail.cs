using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilidades.EnvioCorreo.Parameter
{
    public class ConfigCredentialsEmail
    {
        public string CorreoRemitente { get; set; }
        public string Password { get; set; }
        public string Dominio { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }



    }
}

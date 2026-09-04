using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WCF.InicioSesionUIF.DTO
{
    public class Result
    {
        public int codigoMensaje { get; set; }

        public string descripcionMensaje { get; set; }

        public string token { get; set; }
    }
}

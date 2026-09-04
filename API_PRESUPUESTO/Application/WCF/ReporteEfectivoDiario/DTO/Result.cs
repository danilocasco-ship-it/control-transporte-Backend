using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WCF.ReporteEfectivoDiario.DTO
{
    public class Result
    {
        public int codigoMensaje { get; set; }

        public string descripcionMensaje { get; set; }

        public string codigoTransaccion { get; set; }

        public string registrosProcesados { get; set; }
    }
}

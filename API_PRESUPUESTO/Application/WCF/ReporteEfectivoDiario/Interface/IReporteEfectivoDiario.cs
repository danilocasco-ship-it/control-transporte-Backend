using Application.WCF.ReporteEfectivoDiario.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WCF.ReporteEfectivoDiario.Interface
{
    public interface IReporteEfectivoDiario
    {
        Result reporteEfectivoDiario(string url, string xml);
    }
}

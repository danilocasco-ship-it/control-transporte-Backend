using Application.WCF.InicioSesionUIF.DTO;
using Application.WCF.InicioSesionUIF.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WCF.InicioSesionUIF.Interface
{
    public interface IIniciarSesionService
    {
        Result IniciarSesionUIF(ParameterInicioSesion parameter);

    }
}

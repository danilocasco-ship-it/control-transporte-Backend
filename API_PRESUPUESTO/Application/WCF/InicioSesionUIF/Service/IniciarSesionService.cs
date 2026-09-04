using Application.Configuration.SerializeToXml;
using Application.WCF.InicioSesionUIF.DTO;
using Application.WCF.InicioSesionUIF.Interface;
using Application.WCF.InicioSesionUIF.Parameter;
using Infrastructure.DataConnection.ConnectionWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WCF.InicioSesionUIF.Service
{
    public class IniciarSesionService : IIniciarSesionService
    {
        private readonly ISerializeToXml _serializeToXml;
        private readonly IConnectionWCF _connectionWCF;

        public IniciarSesionService(ISerializeToXml serializeToXml, IConnectionWCF connectionWCF)
        {
            _serializeToXml = serializeToXml;
            _connectionWCF = connectionWCF;
        }

        public Result IniciarSesionUIF(ParameterInicioSesion parameter)
        {
            InicioSesion inicioSesion = new InicioSesion();

            inicioSesion.request = new request();
            inicioSesion.request.cuenta = parameter.cuenta;
            inicioSesion.request.clave = parameter.clave;
            inicioSesion.request.mac = parameter.mac;

            string xml = _serializeToXml.SerializeToXmlContent(inicioSesion, "inicioSesion");

            string response = _connectionWCF.LlamarServicioWCF(parameter.url, xml).Result;

            //Dererializar response a Result

            Result result = _serializeToXml.DeserializeXml<Result>(response);

            return result;

        }
    }
}

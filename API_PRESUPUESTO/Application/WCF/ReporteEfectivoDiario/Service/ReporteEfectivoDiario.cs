using Application.Configuration.SerializeToXml;
using Application.WCF.ReporteEfectivoDiario.DTO;
using Application.WCF.ReporteEfectivoDiario.Interface;
using Infrastructure.DataConnection.ConnectionWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WCF.ReporteEfectivoDiario.Service
{
    public class ReporteEfectivoDiario : IReporteEfectivoDiario
    {
        private readonly ISerializeToXml _serializeToXml;
        private readonly IConnectionWCF _connectionWCF;

        public ReporteEfectivoDiario(ISerializeToXml serializeToXml, IConnectionWCF connectionWCF)
        {
            _serializeToXml = serializeToXml;
            _connectionWCF = connectionWCF;
        }

        public Result reporteEfectivoDiario(string url, string xml)
        {
            string response = _connectionWCF.LlamarServicioWCF(url, xml).Result;

            //Dererializar response a Result

            Result result = _serializeToXml.DeserializeXml<Result>(response);

            return result;

        }
    }
}

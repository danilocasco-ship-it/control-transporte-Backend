using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataConnection.ConnectionWCF
{
    public interface IConnectionWCF
    {
        Task<string> LlamarServicioWCF(string url, string xml);
    }
}

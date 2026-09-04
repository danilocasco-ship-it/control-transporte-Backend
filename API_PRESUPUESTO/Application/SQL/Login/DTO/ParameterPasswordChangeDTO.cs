using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.Login.DTO
{
    public class ParameterPasswordChangeDTO
    {
        public int? id { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string clave_nueva { get; set; }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.Login_pre.Parameter
{
    public class Parameter_Login
    {
        public int codempresa { get; set; }


        [JsonProperty("codusuario")]
        public string codusuario { get; set; }

        [JsonProperty("clave")]
        public string clave { get; set; }


    }
}

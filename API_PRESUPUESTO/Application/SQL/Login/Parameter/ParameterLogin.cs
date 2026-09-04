using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.Login.Parameter
{
    public class ParameterLogin
    {
        [JsonProperty("usuario")]
        public string usuario { get; set;}

        [JsonProperty("clave")]
        public string clave { get; set;}

        //[JsonProperty("mac_addres")]
        //public string mac_addres { get; set;}

    }
}

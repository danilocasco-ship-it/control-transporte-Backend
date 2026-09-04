using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.Asignaciones.ListaAsignaciones.DTO
{
    public sealed class ListaAsignacionesDTO
    {
        public int numpre { get; set; }
        public string nompre { get; set; } = "";
        public string poligono { get; set; } = "";
        public string lote { get; set; } = "";
        public int codfase { get; set; }
        public string nomfase { get; set; } = "";
        public int codproceso { get; set; }
        public string nomproceso { get; set; } = "";
        public int codetapa { get; set; }
        public string nometapa { get; set; } = "";
        public int codrubro { get; set; }
        public string nomrubro { get; set; } = "";
        public int contratistaanterior { get; set; }
        public string Contratista_Anterior { get; set; } = "";
        public int contratistanuevo { get; set; }
        public string Contratista_Nuevo { get; set; } = "";
        public string usuarioactualiza { get; set; } = "";
        public DateTime fechaactualiza { get; set; }
    }
}


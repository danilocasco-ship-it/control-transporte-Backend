namespace Application.SQL.Asignaciones.ListaAsignaciones.Parameter
{
    public sealed class ListaAsignacionesParameter
    {
        public int CodEmpresa { get; set; }
        public int? CodSucursal { get; set; }
        public int? CodProyecto { get; set; }
        public string poligono { get; set; }

        public string lote { get; set; }

        public int? fase { get; set; }
        public int? proceso { get; set; }


        public string DbConnection { get; set; } = string.Empty;
    }
}

namespace Application.Presupuestos.DetallePresupuesto.DTO
{
    public class ListaProcesosDTO
    {
        public int numpre { get; set; }
        public string correlativo { get; set; }
        public int codetapa { get; set; }
        public string nometapa { get; set; }
        public int codrubro { get; set; }
        public string nomrubro { get; set; }
        public int codfase { get; set; }
        public string nomfase { get; set; }
        public int codproceso { get; set; }
        public string nomproceso { get; set; }
        public string descripcion { get; set; }
        public string responsable { get; set; }
        public string Contratista { get; set; }
    }
}
using Application.Presupuestos.Asignacion.AsignaContratista.Interface;
using Application.Presupuestos.Asignacion.AsignaContratista.Parametros;
using Infrastructure.DataConnection.ConnectionSQL;

namespace Application.Presupuestos.Asignacion.AsignaContratista.Services
{
    public class AsignaContratistaService : IAsignaContratista
    {
        private readonly IDbExecutor _dbExecutor;

        public AsignaContratistaService(IDbExecutor dbExecutor)
        {
            _dbExecutor = dbExecutor;
        }

        public bool Asignar(string db, AsignaContratistaParametros parametros)
        {
            string query = @"
                UPDATE pre_presupuesto_enc
                SET responsable = @responsable
                WHERE numpre = @numpre";

            _dbExecutor.ExecuteQuery(query, parametros, false, db);

            return true;
        }
    }
}
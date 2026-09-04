using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataConnection.ConnectionOracle
{
    public interface IDbOracleExecutor : IDisposable
    {
        DataTable ExecuteStoredProcedure(string storedProcedure, object parameters, bool returnsData, string connectionString, bool useTransaction = false);

        DataTable ExecuteQuery(string query, object parameters, bool returnsData, string connectionString, bool useTransaction = false);
    }
}

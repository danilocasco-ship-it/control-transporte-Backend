using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataConnection.ConnectionSQL
{
    public interface IDbExecutor : IDisposable
    {
        DataTable ExecuteStoredProcedure(string storedProcedure, object parameters, bool returnsData, string connectionString, bool useTransaction = false);

        DataSet ExecuteStoreMultiple(string storedProcedure, object parameters, bool returnsData, string connectionString, bool useTransaction = false);

        DataTable ExecuteQuery(string query, object parameters = null, bool returnsData = true, string connectionString = null, bool useTransaction = false);

        (DataTable DataTable, Dictionary<string, object> OutputParameters) ExecuteStoredProcedureOut(string storedProcedure, object parameters, bool returnsData, string connectionString, bool useTransaction = false);

    }
}

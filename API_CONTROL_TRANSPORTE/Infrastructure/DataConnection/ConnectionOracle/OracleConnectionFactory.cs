using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataConnection.ConnectionOracle
{
    public class OracleConnectionFactory : IOracleConnectionFactory
    { 
        public IDbConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}

using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;


namespace Infrastructure.DataConnection.ConnectionOracle
{
    public class OracleDbExecutor : IDbOracleExecutor
    {
        private readonly IOracleConnectionFactory _connectionFactory;
        private IDbConnection _dbConnection;

        public OracleDbExecutor(IOracleConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public DataTable ExecuteStoredProcedure(string storedProcedure, object parameters, bool returnsData, string connectionString, bool useTransaction = false)
        {
            try
            {
                using (_dbConnection = _connectionFactory.CreateConnection(connectionString))
                {
                    _dbConnection.Open();

                    using (var command = _dbConnection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = storedProcedure;

                        if (parameters != null)
                        {
                            AddParametersToCommand(command, parameters);
                        }
                        var dataTable = new DataTable();

                        if (useTransaction)
                        {
                            // No es necesario iniciar una transacción para Oracle.ManagedDataAccess.Client
                        }

                        if (returnsData)
                        {
                            using (var dataAdapter = new OracleDataAdapter((OracleCommand)command))
                            {
                                dataAdapter.Fill(dataTable);
                            }
                        }
                        else
                        {
                            command.ExecuteNonQuery();
                        }
                        
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return null;
            }
        }

        public DataTable ExecuteQuery(string query, object parameters = null, bool returnsData = true, string connectionString = null, bool useTransaction = false)
        {
            try
            {
                using (_dbConnection = new OracleConnection(connectionString))
                {
                    _dbConnection.Open();

                    using (var command = _dbConnection.CreateCommand())
                    {
                        command.CommandText = query;

                        if (parameters != null)
                        {
                            AddParametersToCommand(command, parameters);
                        }

                        var dataTable = new DataTable();

                        if (useTransaction)
                        {
                            // No es necesario iniciar una transacción para Oracle.ManagedDataAccess.Client
                        }

                        if (returnsData)
                        {
                            using (var dataAdapter = new OracleDataAdapter((OracleCommand)command))
                            {
                                dataAdapter.Fill(dataTable);
                            }
                        }
                        else
                        {
                            command.ExecuteNonQuery();
                        }

                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return null;
            }
        }

        private void AddParametersToCommand(IDbCommand command, object parameters)
        {
            var properties = parameters.GetType().GetProperties();
            foreach (var property in properties)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = property.Name;
                parameter.Value = property.GetValue(parameters);
                command.Parameters.Add(parameter);
            }
        }

        public void Dispose()
        {
            // Implement IDisposable pattern as needed
        }
    }
}

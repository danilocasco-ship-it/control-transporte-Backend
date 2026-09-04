using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.DataConnection.ConnectionSQL
{
    public class SqlDbExecutor : IDbExecutor
    {
        private readonly IConnectionFactory _connectionFactory;
        private IDbConnection _dbConnection;

        public SqlDbExecutor(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public DataTable ExecuteStoredProcedure(string storedProcedure, object parameters, bool returnsData, string connectionString, bool useTransaction = false)
        {
            try
            {
                if (!useTransaction)
                {
                    _dbConnection = _connectionFactory.CreateConnection(connectionString);
                    _dbConnection.Open();
                }

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
                        command.Transaction = _dbConnection.BeginTransaction();
                    }

                    if (returnsData)
                    {
                        using (var dataAdapter = new SqlDataAdapter((SqlCommand)command))
                        {
                            dataAdapter.Fill(dataTable);
                        }
                    }
                    else
                    {
                        command.ExecuteNonQuery();
                    }

                    if (!useTransaction)
                    {
                        Disconnect();
                    }

                    return dataTable;
                }
            }
            catch (Exception ex )
            {
                // Log the exception or handle it appropriately
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
            finally
            {
                Disconnect();
            }
        }

        public DataTable ExecuteQuery(string query, object parameters = null, bool returnsData = true, string connectionString = null, bool useTransaction = false)
        {
            try
            {
                using (_dbConnection = new SqlConnection(connectionString))
                {
                    _dbConnection.Open();

                    using (var command = _dbConnection.CreateCommand())
                    {
                        command.CommandText = query;
                        command.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            AddParametersToCommand(command, parameters);
                        }

                        var dataTable = new DataTable();

                        if (useTransaction)
                        {
                            command.Transaction = _dbConnection.BeginTransaction();
                        }

                        if (returnsData)
                        {
                            using (var dataAdapter = new SqlDataAdapter((SqlCommand)command))
                            {
                                dataAdapter.Fill(dataTable);
                            }
                        }
                        else
                        {
                            command.ExecuteNonQuery();
                        }

                        if (useTransaction)
                        {
                            command.Transaction.Commit();
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

        public (DataTable DataTable, Dictionary<string, object> OutputParameters) ExecuteStoredProcedureOut(string storedProcedure, object parameters, bool returnsData, string connectionString, bool useTransaction = false)
        {
            try
            {
                if (!useTransaction)
                {
                    _dbConnection = _connectionFactory.CreateConnection(connectionString);
                    _dbConnection.Open();
                }

                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = storedProcedure;

                    Dictionary<string, object> outputParameters = new Dictionary<string, object>();

                    if (parameters != null)
                    {
                        AddParametersToCommandOut(command, parameters, outputParameters);
                    }

                    var dataTable = new DataTable();

                    if (useTransaction)
                    {
                        command.Transaction = _dbConnection.BeginTransaction();
                    }

                    if (returnsData)
                    {
                        using (var dataAdapter = new SqlDataAdapter((SqlCommand)command))
                        {
                            dataAdapter.Fill(dataTable);
                        }
                    }
                    else
                    {
                        command.ExecuteNonQuery();
                    }

                    // Leer los valores de los parámetros de salida
                    foreach (var param in command.Parameters.Cast<SqlParameter>().Where(p => p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.InputOutput))
                    {
                        outputParameters[param.ParameterName] = param.Value;
                    }

                    if (!useTransaction)
                    {
                        Disconnect();
                    }

                    return (dataTable, outputParameters);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine("Error: " + ex.Message);
                return (null, null);
            }
            finally
            {
                Disconnect();
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

        private void AddParametersToCommandOut(IDbCommand command, object parameters, Dictionary<string, object> outputParameters)
        {
            foreach (var prop in parameters.GetType().GetProperties())
            {
                var paramName = $"@{prop.Name}";
                var value = prop.GetValue(parameters);

                if (value is string && string.IsNullOrEmpty((string)value))
                {
                    var sqlParameter = new SqlParameter(paramName, SqlDbType.NVarChar)
                    {
                        Size = 800, // Tamaño predeterminado
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(sqlParameter);
                    outputParameters[paramName] = null; // Inicializamos en null
                }
                else
                {
                    command.Parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
                }
            }
        }

        public DataSet ExecuteStoreMultiple(string storedProcedure, object parameters, bool returnsData, string connectionString, bool useTransaction = false)
        {
            try
            {
                if (!useTransaction)
                {
                    _dbConnection = _connectionFactory.CreateConnection(connectionString);
                    _dbConnection.Open();
                }

                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = storedProcedure;

                    if (parameters != null)
                    {
                        AddParametersToCommand(command, parameters);
                    }

                    var dataSet = new DataSet();

                    if (useTransaction)
                    {
                        command.Transaction = _dbConnection.BeginTransaction();
                    }

                    if (returnsData)
                    {
                        using (var dataAdapter = new SqlDataAdapter((SqlCommand)command))
                        {
                            dataAdapter.Fill(dataSet);
                        }
                    }
                    else
                    {
                        command.ExecuteNonQuery();
                    }

                    if (!useTransaction)
                    {
                        Disconnect();
                    }

                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return null;
            }
            finally
            {
                Disconnect();
            }
        }

        private void Disconnect()
        {
            if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }

        public void Dispose()
        {
            // Implement IDisposable pattern as needed
        }
    }
}

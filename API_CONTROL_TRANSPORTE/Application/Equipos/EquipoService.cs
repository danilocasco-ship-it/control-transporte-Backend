using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Application.Equipos
{
    public class EquipoService : IEquipoService
    {
        // Se mantiene la misma cadena usada por el módulo Proveedores existente.
        private const string ConnectionString = "Data Source=172.16.0.75;Initial Catalog=negocios_g;Persist Security Info=True;User ID=sr2008;Password=2008;";

        public void GuardarEquipo(EquipoDto equipo)
        {
            const string query = @"
                INSERT INTO dbo.gen_equipos
                    (codempresa, codequipo, nomequipo, codtipomaquinaria, codmarca,
                     codproveedor, modelo, anio, placa, capacidad, estado, codusuario)
                VALUES
                    (@codempresa, @codequipo, @nomequipo, @codtipomaquinaria, @codmarca,
                     @codproveedor, @modelo, @anio, @placa, @capacidad, @estado, @codusuario);";

            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@codempresa", equipo.Codempresa);
                command.Parameters.AddWithValue("@codequipo", ObtenerSiguienteCodigo(connection, equipo.Codempresa));
                AgregarParametrosEquipo(command, equipo);
                command.ExecuteNonQuery();
            }
        }

        public List<EquipoDto> ObtenerTodos()
        {
            const string query = @"
                SELECT e.codempresa, e.codequipo, e.nomequipo, e.codtipomaquinaria, e.codmarca,
                       e.codproveedor, e.modelo, e.anio, e.placa, e.capacidad, e.estado, e.fecharegistro, e.codusuario,
                       ISNULL(p.nomproveedor, '') + ' ' + ISNULL(p.apellidoproveedor, '') AS nomproveedor
                FROM dbo.gen_equipos e
                LEFT JOIN dbo.gen_proveedor p ON p.codempresa = e.codempresa AND p.codproveedor = e.codproveedor
                WHERE e.estado = 1
                ORDER BY e.nomequipo;";

            var equipos = new List<EquipoDto>();
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) equipos.Add(MapearEquipo(reader));
                }
            }
            return equipos;
        }

        public EquipoDto ObtenerPorId(int codempresa, int codequipo)
        {
            const string query = @"
                SELECT e.codempresa, e.codequipo, e.nomequipo, e.codtipomaquinaria, e.codmarca,
                       e.codproveedor, e.modelo, e.anio, e.placa, e.capacidad, e.estado, e.fecharegistro, e.codusuario,
                       ISNULL(p.nomproveedor, '') + ' ' + ISNULL(p.apellidoproveedor, '') AS nomproveedor
                FROM dbo.gen_equipos e
                LEFT JOIN dbo.gen_proveedor p ON p.codempresa = e.codempresa AND p.codproveedor = e.codproveedor
                WHERE e.codempresa = @codempresa AND e.codequipo = @codequipo;";

            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@codempresa", codempresa);
                command.Parameters.AddWithValue("@codequipo", codequipo);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    return reader.Read() ? MapearEquipo(reader) : null;
                }
            }
        }

        public void ActualizarEquipo(int codempresa, int codequipo, EquipoDto equipo)
        {
            const string query = @"
                UPDATE dbo.gen_equipos
                SET nomequipo = @nomequipo,
                    codtipomaquinaria = @codtipomaquinaria,
                    codmarca = @codmarca,
                    codproveedor = @codproveedor,
                    modelo = @modelo,
                    anio = @anio,
                    placa = @placa,
                    capacidad = @capacidad,
                    estado = @estado,
                    codusuario = @codusuario
                WHERE codempresa = @codempresa AND codequipo = @codequipo;";

            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@codempresa", codempresa);
                command.Parameters.AddWithValue("@codequipo", codequipo);
                AgregarParametrosEquipo(command, equipo);
                connection.Open();
                if (command.ExecuteNonQuery() == 0) throw new InvalidOperationException("Equipo no encontrado.");
            }
        }

        public void EliminarEquipo(int codempresa, int codequipo)
        {
            const string query = @"UPDATE dbo.gen_equipos SET estado = 0 WHERE codempresa = @codempresa AND codequipo = @codequipo;";
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@codempresa", codempresa);
                command.Parameters.AddWithValue("@codequipo", codequipo);
                connection.Open();
                if (command.ExecuteNonQuery() == 0) throw new InvalidOperationException("Equipo no encontrado.");
            }
        }

        private static int ObtenerSiguienteCodigo(SqlConnection connection, int codempresa)
        {
            const string query = "SELECT ISNULL(MAX(codequipo), 0) + 1 FROM dbo.gen_equipos WHERE codempresa = @codempresa;";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@codempresa", codempresa);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private static void AgregarParametrosEquipo(SqlCommand command, EquipoDto equipo)
        {
            command.Parameters.AddWithValue("@nomequipo", equipo.Nomequipo);
            command.Parameters.AddWithValue("@codtipomaquinaria", (object)equipo.Codtipomaquinaria ?? DBNull.Value);
            command.Parameters.AddWithValue("@codmarca", (object)equipo.Codmarca ?? DBNull.Value);
            command.Parameters.AddWithValue("@codproveedor", (object)equipo.Codproveedor ?? DBNull.Value);
            command.Parameters.AddWithValue("@modelo", (object)equipo.Modelo ?? DBNull.Value);
            command.Parameters.AddWithValue("@anio", (object)equipo.Anio ?? DBNull.Value);
            command.Parameters.AddWithValue("@placa", (object)equipo.Placa ?? DBNull.Value);
            command.Parameters.AddWithValue("@capacidad", (object)equipo.Capacidad ?? DBNull.Value);
            command.Parameters.AddWithValue("@estado", equipo.Estado);
            command.Parameters.AddWithValue("@codusuario", (object)equipo.Codusuario ?? DBNull.Value);
        }

        private static EquipoDto MapearEquipo(SqlDataReader reader)
        {
            return new EquipoDto
            {
                Codempresa = Convert.ToInt32(reader["codempresa"]),
                Codequipo = Convert.ToInt32(reader["codequipo"]),
                Nomequipo = reader["nomequipo"].ToString(),
                Codtipomaquinaria = reader["codtipomaquinaria"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["codtipomaquinaria"]),
                Codmarca = reader["codmarca"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["codmarca"]),
                Codproveedor = reader["codproveedor"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["codproveedor"]),
                Nomproveedor = reader["nomproveedor"] == DBNull.Value ? null : reader["nomproveedor"].ToString().Trim(),
                Modelo = reader["modelo"] == DBNull.Value ? null : reader["modelo"].ToString(),
                Anio = reader["anio"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["anio"]),
                Placa = reader["placa"] == DBNull.Value ? null : reader["placa"].ToString(),
                Capacidad = reader["capacidad"] == DBNull.Value ? null : reader["capacidad"].ToString(),
                Estado = Convert.ToInt32(reader["estado"]),
                Fecharegistro = reader["fecharegistro"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["fecharegistro"]),
                Codusuario = reader["codusuario"] == DBNull.Value ? null : reader["codusuario"].ToString()
            };
        }
    }
}

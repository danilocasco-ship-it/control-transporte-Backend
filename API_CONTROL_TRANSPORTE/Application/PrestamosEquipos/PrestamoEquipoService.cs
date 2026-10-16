using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Application.PrestamosEquipos
{
    public class PrestamoEquipoService : IPrestamoEquipoService
    {
        // Misma conexión utilizada por Proveedores y Equipos.
        private const string ConnectionString = "Data Source=172.16.0.75;Initial Catalog=negocios_g;Persist Security Info=True;User ID=sr2008;Password=2008;";

        public List<PrestamoEquipoDto> ObtenerTodos(int codempresa)
        {
            const string query = @"SELECT codempresa, codsucursal, codcontratista, codequipo, fecharegistro, fechaprestamo, estadoprestamo, fechadevolucion, estadodevolucion, observaciones, codusuario FROM dbo.tra_prestamo_equipo WHERE codempresa = @codempresa ORDER BY fechaprestamo DESC;";
            var prestamos = new List<PrestamoEquipoDto>();
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@codempresa", codempresa);
                connection.Open();
                using (var reader = command.ExecuteReader())
                    while (reader.Read()) prestamos.Add(Mapear(reader));
            }
            return prestamos;
        }

        public PrestamoEquipoDto ObtenerPorId(int codempresa, int codsucursal, int codcontratista, int codequipo)
        {
            const string query = @"SELECT codempresa, codsucursal, codcontratista, codequipo, fecharegistro, fechaprestamo, estadoprestamo, fechadevolucion, estadodevolucion, observaciones, codusuario FROM dbo.tra_prestamo_equipo WHERE codempresa = @codempresa AND codsucursal = @codsucursal AND codcontratista = @codcontratista AND codequipo = @codequipo;";
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = CrearComandoPk(connection, query, codempresa, codsucursal, codcontratista, codequipo))
            {
                connection.Open();
                using (var reader = command.ExecuteReader()) return reader.Read() ? Mapear(reader) : null;
            }
        }

        public void Guardar(PrestamoEquipoDto prestamo)
        {
            const string query = @"INSERT INTO dbo.tra_prestamo_equipo (codempresa, codsucursal, codcontratista, codequipo, fecharegistro, fechaprestamo, estadoprestamo, fechadevolucion, estadodevolucion, observaciones, codusuario) VALUES (@codempresa, @codsucursal, @codcontratista, @codequipo, GETDATE(), @fechaprestamo, @estadoprestamo, @fechadevolucion, @estadodevolucion, @observaciones, @codusuario);";
            Ejecutar(query, prestamo);
        }

        public void Actualizar(int codempresa, int codsucursal, int codcontratista, int codequipo, PrestamoEquipoDto prestamo)
        {
            const string query = @"UPDATE dbo.tra_prestamo_equipo SET fechaprestamo = @fechaprestamo, estadoprestamo = @estadoprestamo, fechadevolucion = @fechadevolucion, estadodevolucion = @estadodevolucion, observaciones = @observaciones, codusuario = @codusuario WHERE codempresa = @codempresa AND codsucursal = @codsucursal AND codcontratista = @codcontratista AND codequipo = @codequipo;";
            Ejecutar(query, prestamo, codempresa, codsucursal, codcontratista, codequipo);
        }

        public void Eliminar(int codempresa, int codsucursal, int codcontratista, int codequipo)
        {
            const string query = @"DELETE FROM dbo.tra_prestamo_equipo WHERE codempresa = @codempresa AND codsucursal = @codsucursal AND codcontratista = @codcontratista AND codequipo = @codequipo;";
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = CrearComandoPk(connection, query, codempresa, codsucursal, codcontratista, codequipo))
            {
                connection.Open();
                if (command.ExecuteNonQuery() == 0) throw new InvalidOperationException("Préstamo no encontrado.");
            }
        }

        private static void Ejecutar(string query, PrestamoEquipoDto prestamo, int? codempresa = null, int? codsucursal = null, int? codcontratista = null, int? codequipo = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@codempresa", codempresa ?? prestamo.Codempresa);
                command.Parameters.AddWithValue("@codsucursal", codsucursal ?? prestamo.Codsucursal);
                command.Parameters.AddWithValue("@codcontratista", codcontratista ?? prestamo.Codcontratista);
                command.Parameters.AddWithValue("@codequipo", codequipo ?? prestamo.Codequipo);
                command.Parameters.AddWithValue("@fechaprestamo", (object)prestamo.Fechaprestamo ?? DBNull.Value);
                command.Parameters.AddWithValue("@estadoprestamo", (object)prestamo.Estadoprestamo ?? DBNull.Value);
                command.Parameters.AddWithValue("@fechadevolucion", (object)prestamo.Fechadevolucion ?? DBNull.Value);
                command.Parameters.AddWithValue("@estadodevolucion", (object)prestamo.Estadodevolucion ?? DBNull.Value);
                command.Parameters.AddWithValue("@observaciones", (object)prestamo.Observaciones ?? DBNull.Value);
                command.Parameters.AddWithValue("@codusuario", (object)prestamo.Codusuario ?? DBNull.Value);
                connection.Open();
                if (command.ExecuteNonQuery() == 0 && codempresa.HasValue) throw new InvalidOperationException("Préstamo no encontrado.");
            }
        }

        private static SqlCommand CrearComandoPk(SqlConnection connection, string query, int codempresa, int codsucursal, int codcontratista, int codequipo)
        {
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@codempresa", codempresa);
            command.Parameters.AddWithValue("@codsucursal", codsucursal);
            command.Parameters.AddWithValue("@codcontratista", codcontratista);
            command.Parameters.AddWithValue("@codequipo", codequipo);
            return command;
        }

        private static PrestamoEquipoDto Mapear(SqlDataReader reader)
        {
            return new PrestamoEquipoDto
            {
                Codempresa = Convert.ToInt32(reader["codempresa"]), Codsucursal = Convert.ToInt32(reader["codsucursal"]), Codcontratista = Convert.ToInt32(reader["codcontratista"]), Codequipo = Convert.ToInt32(reader["codequipo"]),
                Fecharegistro = Fecha(reader, "fecharegistro"), Fechaprestamo = Fecha(reader, "fechaprestamo"), Estadoprestamo = Texto(reader, "estadoprestamo"), Fechadevolucion = Fecha(reader, "fechadevolucion"), Estadodevolucion = Texto(reader, "estadodevolucion"), Observaciones = Texto(reader, "observaciones"), Codusuario = Texto(reader, "codusuario")
            };
        }

        private static DateTime? Fecha(SqlDataReader reader, string columna) => reader[columna] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader[columna]);
        private static string Texto(SqlDataReader reader, string columna) => reader[columna] == DBNull.Value ? null : reader[columna].ToString();
    }
}

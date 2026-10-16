using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Application.Catalogos
{
    public class CatalogoEquipoService : ICatalogoEquipoService
    {
        // Misma conexión del módulo Proveedores y Equipos.
        private const string ConnectionString = "Data Source=172.16.0.75;Initial Catalog=negocios_g;Persist Security Info=True;User ID=sr2008;Password=2008;";

        public List<TipoMaquinariaDto> ObtenerTiposMaquinaria(int codempresa)
        {
            const string query = @"
                SELECT codempresa, codtipomaquinaria, nomtipomaquinaria, codtipomaquinariapadre
                FROM dbo.tra_tipo_maquinaria
                WHERE codempresa = @codempresa
                ORDER BY nomtipomaquinaria;";

            var tipos = new List<TipoMaquinariaDto>();
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@codempresa", codempresa);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tipos.Add(new TipoMaquinariaDto
                        {
                            Codempresa = Convert.ToInt32(reader["codempresa"]),
                            Codtipomaquinaria = Convert.ToInt32(reader["codtipomaquinaria"]),
                            Nomtipomaquinaria = reader["nomtipomaquinaria"].ToString(),
                            Codtipomaquinariapadre = reader["codtipomaquinariapadre"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["codtipomaquinariapadre"])
                        });
                    }
                }
            }
            return tipos;
        }

        public List<MarcaDto> ObtenerMarcas(int codempresa)
        {
            const string query = @"
                SELECT codempresa, codmarca, nommarca, descripcion
                FROM dbo.tra_marca
                WHERE codempresa = @codempresa
                ORDER BY nommarca;";

            var marcas = new List<MarcaDto>();
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@codempresa", codempresa);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        marcas.Add(new MarcaDto
                        {
                            Codempresa = Convert.ToInt32(reader["codempresa"]),
                            Codmarca = Convert.ToInt32(reader["codmarca"]),
                            Nommarca = reader["nommarca"] == DBNull.Value ? string.Empty : reader["nommarca"].ToString(),
                            Descripcion = reader["descripcion"] == DBNull.Value ? null : reader["descripcion"].ToString()
                        });
                    }
                }
            }
            return marcas;
        }

        public List<SucursalDto> ObtenerSucursales(int codempresa)
        {
            const string query = "SELECT codempresa, codsucursal, nomsucursal FROM dbo.gen_sucursal WHERE codempresa = @codempresa ORDER BY nomsucursal;";
            var sucursales = new List<SucursalDto>();
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@codempresa", codempresa); connection.Open();
                using (var reader = command.ExecuteReader()) while (reader.Read()) sucursales.Add(new SucursalDto { Codempresa = Convert.ToInt32(reader["codempresa"]), Codsucursal = Convert.ToInt32(reader["codsucursal"]), Nomsucursal = reader["nomsucursal"].ToString() });
            }
            return sucursales;
        }

        public List<ContratistaDto> ObtenerContratistas(int codempresa)
        {
            const string query = "SELECT codempresa, codempleado, nomempleado, codestado FROM dbo.V_Contratista WHERE codempresa = @codempresa ORDER BY nomempleado;";
            var contratistas = new List<ContratistaDto>();
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@codempresa", codempresa); connection.Open();
                using (var reader = command.ExecuteReader()) while (reader.Read()) contratistas.Add(new ContratistaDto { Codempresa = Convert.ToInt32(reader["codempresa"]), Codempleado = reader["codempleado"].ToString(), Nomempleado = reader["nomempleado"].ToString(), Codestado = Convert.ToInt32(reader["codestado"]) });
            }
            return contratistas;
        }

        public List<EstadoDto> ObtenerEstados(int codempresa)
        {
            const string query = "SELECT codempresa, codestado, nomestado FROM dbo.tra_estado WHERE codempresa = @codempresa ORDER BY nomestado;";
            var estados = new List<EstadoDto>();
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@codempresa", codempresa); connection.Open();
                using (var reader = command.ExecuteReader()) while (reader.Read()) estados.Add(new EstadoDto { Codempresa = Convert.ToInt32(reader["codempresa"]), Codestado = Convert.ToInt32(reader["codestado"]), Nomestado = reader["nomestado"].ToString() });
            }
            return estados;
        }
    }
}

using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace Application.Proveedores
{
    public class ProveedorService : IProveedorService
    {
        private readonly IConnectionFactory _connectionFactory;



        private readonly string _connectionString = "Data Source=172.16.0.75;Initial Catalog=negocios_g;Persist Security Info=True;User ID=sr2008;Password=2008;";
        public ProveedorService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // ✅ CREATE - Guardar proveedor
        public void GuardarProveedor(ProveedorDto proveedor)
        {
        

            string connectionString = "Data Source=172.16.0.75;Initial Catalog=negocios_g;Persist Security Info=True;User ID=sr2008;Password=2008;";

            int nuevoCodProveedor = 1;
            string queryMax = File.ReadAllText("Query/Proveedor/BuscarCorrelativo.sql");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdMax = new SqlCommand(queryMax, conn))
                {
                    cmdMax.Parameters.AddWithValue("@codempresa", proveedor.Codempresa);
                    conn.Open();
                    nuevoCodProveedor = Convert.ToInt32(cmdMax.ExecuteScalar());
                }
            }

            string query = File.ReadAllText("Query/Proveedor/InsertarProveedor.sql");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codempresa", proveedor.Codempresa);
                    cmd.Parameters.AddWithValue("@codproveedor", nuevoCodProveedor); 
                    cmd.Parameters.AddWithValue("@nomproveedor", proveedor.Nomproveedor);
                    cmd.Parameters.AddWithValue("@apellidoproveedor", proveedor.Apellidoproveedor);
                    cmd.Parameters.AddWithValue("@duiproveedor", proveedor.Duiproveedor);
                    cmd.Parameters.AddWithValue("@nitproveedor", proveedor.Nitproveedor);
                    cmd.Parameters.AddWithValue("@nrfproveedor", proveedor.Nrfproveedor);
                    cmd.Parameters.AddWithValue("@estado", proveedor.Estado);
                    cmd.Parameters.AddWithValue("@telefonoproveedor", proveedor.Telefonoproveedor);
                    cmd.Parameters.AddWithValue("@direccionproveedor", proveedor.Direccionproveedor);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ✅ READ - Obtener todos los proveedores
        public List<ProveedorDto> ObtenerTodos()
        {
            var lista = new List<ProveedorDto>();
            string query = @"
                SELECT codempresa, codproveedor, nomproveedor, apellidoproveedor, 
                       duiproveedor, nitproveedor, nrfproveedor, estado, 
                       telefonoproveedor, direccionproveedor
                FROM dbo.gen_proveedor
                WHERE estado = 1";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ProveedorDto
                            {
                                Codempresa = Convert.ToInt32(reader["codempresa"]),
                                Codproveedor = Convert.ToInt32(reader["codproveedor"]),
                                Nomproveedor = reader["nomproveedor"].ToString(),
                                Apellidoproveedor = reader["apellidoproveedor"].ToString(),
                                Duiproveedor = reader["duiproveedor"].ToString(),
                                Nitproveedor = reader["nitproveedor"].ToString(),
                                Nrfproveedor = reader["nrfproveedor"].ToString(),
                                Estado = Convert.ToInt32(reader["estado"]),
                                Telefonoproveedor = reader["telefonoproveedor"].ToString(),
                                Direccionproveedor = reader["direccionproveedor"].ToString()
                            });
                        }
                    }
                }
            }

            return lista;
        }

        // ✅ READ - Obtener un proveedor por ID
        public ProveedorDto ObtenerPorId(int codempresa, int codproveedor)
        {
            ProveedorDto proveedor = null;
            string query = @"
                SELECT codempresa, codproveedor, nomproveedor, apellidoproveedor, 
                       duiproveedor, nitproveedor, nrfproveedor, estado, 
                       telefonoproveedor, direccionproveedor
                FROM dbo.gen_proveedor
                WHERE codempresa = @codempresa AND codproveedor = @codproveedor";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codempresa", codempresa);
                    cmd.Parameters.AddWithValue("@codproveedor", codproveedor);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            proveedor = new ProveedorDto
                            {
                                Codempresa = Convert.ToInt32(reader["codempresa"]),
                                Codproveedor = Convert.ToInt32(reader["codproveedor"]),
                                Nomproveedor = reader["nomproveedor"].ToString(),
                                Apellidoproveedor = reader["apellidoproveedor"].ToString(),
                                Duiproveedor = reader["duiproveedor"].ToString(),
                                Nitproveedor = reader["nitproveedor"].ToString(),
                                Nrfproveedor = reader["nrfproveedor"].ToString(),
                                Estado = Convert.ToInt32(reader["estado"]),
                                Telefonoproveedor = reader["telefonoproveedor"].ToString(),
                                Direccionproveedor = reader["direccionproveedor"].ToString()
                            };
                        }
                    }
                }
            }

            return proveedor;
        }

        // ✅ UPDATE - Actualizar proveedor
        public void ActualizarProveedor(int codempresa, int codproveedor, ProveedorDto proveedor)
        {
            string query = @"
                UPDATE dbo.gen_proveedor
                SET nomproveedor = @nomproveedor,
                    apellidoproveedor = @apellidoproveedor,
                    duiproveedor = @duiproveedor,
                    nitproveedor = @nitproveedor,
                    nrfproveedor = @nrfproveedor,
                    estado = @estado,
                    telefonoproveedor = @telefonoproveedor,
                    direccionproveedor = @direccionproveedor
                WHERE codempresa = @codempresa AND codproveedor = @codproveedor";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codempresa", codempresa);
                    cmd.Parameters.AddWithValue("@codproveedor", codproveedor);
                    cmd.Parameters.AddWithValue("@nomproveedor", proveedor.Nomproveedor);
                    cmd.Parameters.AddWithValue("@apellidoproveedor", proveedor.Apellidoproveedor);
                    cmd.Parameters.AddWithValue("@duiproveedor", proveedor.Duiproveedor);
                    cmd.Parameters.AddWithValue("@nitproveedor", proveedor.Nitproveedor);
                    cmd.Parameters.AddWithValue("@nrfproveedor", proveedor.Nrfproveedor);
                    cmd.Parameters.AddWithValue("@estado", proveedor.Estado);
                    cmd.Parameters.AddWithValue("@telefonoproveedor", proveedor.Telefonoproveedor);
                    cmd.Parameters.AddWithValue("@direccionproveedor", proveedor.Direccionproveedor);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ✅ DELETE - Eliminar proveedor (soft delete: cambia estado a 0)
        public void EliminarProveedor(int codempresa, int codproveedor)
        {
            string query = @"
                UPDATE dbo.gen_proveedor
                SET estado = 0
                WHERE codempresa = @codempresa AND codproveedor = @codproveedor";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codempresa", codempresa);
                    cmd.Parameters.AddWithValue("@codproveedor", codproveedor);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
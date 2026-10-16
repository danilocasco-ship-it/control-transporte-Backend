

using System.Collections.Generic;

namespace Application.Proveedores
{
    public interface IProveedorService
    {
        // guardar un nuevo proveedor  DANILO 17/09/2026
        // CREATE
        void GuardarProveedor(ProveedorDto proveedor);

        // READ - Obtener todos
        List<ProveedorDto> ObtenerTodos();

        // READ - Obtener por ID
        ProveedorDto ObtenerPorId(int codempresa, int codproveedor);

        // UPDATE
        void ActualizarProveedor(int codempresa, int codproveedor, ProveedorDto proveedor);

        // DELETE
        void EliminarProveedor(int codempresa, int codproveedor);



    }

    public class ProveedorDto
    {
        public int Codempresa { get; set; }
        public int Codproveedor { get; set; }
        public string Nomproveedor { get; set; }
        public string Apellidoproveedor { get; set; }
        public string Duiproveedor { get; set; }
        public string Nitproveedor { get; set; }
        public string Nrfproveedor { get; set; }
        public int Estado { get; set; }
        public string Telefonoproveedor { get; set; }
        public string Direccionproveedor { get; set; }
    }
}


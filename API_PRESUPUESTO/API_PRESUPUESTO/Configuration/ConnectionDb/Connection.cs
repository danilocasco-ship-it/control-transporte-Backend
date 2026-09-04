using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace API_PRESUPUESTO.Configuration.ConnectionDb
{
    public class Connection : IConnection
    {
        private readonly string _connectionString;
        private readonly string _connectionStringOracle;
        private readonly IConfiguration _configuration;
        private readonly string _ConfigEmail;


        public Connection(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("ConnectionGlobal").ToString();

        }
        public string GetConnectionString()
        {
            return _connectionString;
        }

        public string GetConnectionStringDesarrolladoras(int codempresa)
        {
            string _connectionString = string.Empty;

            switch (codempresa)
            {
                case 1:
                    //
                    _connectionString = _configuration.GetConnectionString("ConnectionSalazar").ToString();
                    break;
                case 5:
                    //
                    _connectionString = _configuration.GetConnectionString("ConnectionFenix").ToString();
                    break;
                case 9:
                    _connectionString = _configuration.GetConnectionString("ConnectionGlobal").ToString();
                    break;
            }

            return _connectionString;
        }

    }
}

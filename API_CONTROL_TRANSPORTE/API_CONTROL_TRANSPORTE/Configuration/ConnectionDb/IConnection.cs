namespace API_CONTROL_TRANSPORTE.Configuration.ConnectionDb
{
    public interface IConnection
    {
        string GetConnectionString();

        string GetConnectionStringDesarrolladoras(int codempresa);


    }
}

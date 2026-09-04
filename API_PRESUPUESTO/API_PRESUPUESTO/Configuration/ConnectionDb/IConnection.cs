namespace API_PRESUPUESTO.Configuration.ConnectionDb
{
    public interface IConnection
    {
        string GetConnectionString();

        string GetConnectionStringDesarrolladoras(int codempresa);


    }
}

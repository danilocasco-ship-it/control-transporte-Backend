using System.Collections.Generic;
using System.Data;

namespace Application.Configuration
{
    public interface IDataMapper
    {
        List<T> MapearDataTable<T>(DataTable dataTable) where T : new();

        T MapDataTableToObject<T>(DataTable dataTable) where T : new();
        T MapDictionaryToObject<T>(Dictionary<string, object> dictionary) where T : new();

    }
}
